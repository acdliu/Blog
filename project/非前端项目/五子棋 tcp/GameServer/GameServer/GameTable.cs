//-------------------GameTable.cs-----------------//
using System;
using System.Timers;
using System.Windows.Forms;
namespace GameServer
{
    class GameTable
    {
        public int isAllStart = 0;

        private const int None = -1;       //无棋子
        private const int Black = 0;       //黑色棋子
        private const int White = 1;       //白色棋子
        public Player[] gamePlayer;        //保存同一桌的玩家信息
        private int[,] grid = new int[15, 15];       //15*15的方格
        private System.Timers.Timer timer;       //用于定时产生棋子
        private int NextdotColor = -1;            //应该产生黑棋子还是白棋子
        private ListBox listbox;
        Random rnd = new Random();
        Service service;
        public GameTable(ListBox listbox)
        {
            gamePlayer = new Player[2];
            gamePlayer[0] = new Player();
            gamePlayer[1] = new Player();
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = false;
            this.listbox = listbox;
            service = new Service(listbox);
            ResetGrid();
        }
        /// <summary>重置棋盘</summary>
        public void ResetGrid()
        {
            for (int i = 0; i <= grid.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= grid.GetUpperBound(1); j++)
                {
                    grid[i, j] = None;
                }
            }
            gamePlayer[0].grade = 0;
            gamePlayer[1].grade = 0;
        }
        /// <summary>启动Timer</summary>
        public void StartTimer()
        {
            timer.Start();
            isAllStart = 1;
        }
        /// <summary>停止Timer</summary>
        public void StopTimer()
        {
            timer.Stop();
            isAllStart = 0;
        }
        /// <summary>设置时间间隔</summary>
        /// <param name="interval">时间间隔</param>
        public void SetTimerLevel(int interval)
        {
            timer.Interval = interval;
        }
        /// <summary>达到时间间隔时处理事件</summary>
        private void timer_Elapsed(object sender, EventArgs e)
        {
            //int x, y;
            ////随机产生一个格内没有棋子的单元格位置
            //do
            //{
            //    x = rnd.Next(15);  //产生一个小于15的非负整数
            //    y = rnd.Next(15);
            //} while (grid[x, y] != None);
            ////放置棋子:x坐标,y坐标,颜色
            //SetDot(x, y, NextdotColor);
            ////设定下次分发的旗子颜色
            //NextdotColor = (NextdotColor + 1) % 2;
        }
        /// <summary>发送产生的棋子信息</summary>
        /// <param name="i">指定棋盘的第几行</param>
        /// <param name="j">指定棋盘的第几列</param>
        /// <param name="dotColor">棋子颜色</param>
        public void SetDot(int i, int j, int dotColor)
        {
            /*
             * 产生棋子时，看是否是轮到它下，如果轮到它下，才有反应，如果不是轮到它下，不做反应
             */
            if (NextdotColor == -1)
            {
                NextdotColor = dotColor;
            }

            if (NextdotColor == dotColor)
            {
                //向两个用户发送产生的棋子信息，并判断是否有相邻棋子
                //发送格式：SetDot,行,列,颜色
                grid[i, j] = dotColor;
                service.SendToBoth(this, string.Format("SetDot,{0},{1},{2}", i, j, dotColor));

                int nl = 0, nr = 0, nt = 0, nb = 0, lt = 0, lb = 0, rt = 0, rb = 0;
                /*----------以下判断当前行是否构成五子相连----------*/
                int k = i - 1;
                for (; k > -1; k--)
                {
                    if (grid[k, j] == dotColor)
                    {
                        nl++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (nl < 4)
                {
                    k = i + 1;
                    for (; k < 15; k++)
                    {
                        if (grid[k, j] == dotColor)
                        {
                            nr++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (nl + nr >= 4)
                {
                    ShowWin(dotColor);
                }
                /*----------以下判断当前列是否构成五子相连----------*/
                else
                {
                    k = j - 1;
                    for (; k > -1; k--)
                    {
                        if (grid[i, k] == dotColor)
                        {
                            nt++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (nt < 4)
                    {
                        k = j + 1;
                        for (; k < 15; k++)
                        {
                            if (grid[i, k] == dotColor)
                            {
                                nb++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    if (nt + nb >= 4)
                    {
                        ShowWin(dotColor);
                    }
                    /*----------以下判断斜线是否构成五子相连----------*/
                    else
                    {
                        int k1 = i - 1, k2 = j - 1;
                        for (; k1 > -1 && k2 > -1; k1--, k2--)
                        {
                            if (grid[k1, k2] == dotColor)
                            {
                                lt++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (lt < 4)
                        {
                            k1 = i + 1;
                            k2 = j + 1;
                            for (; k1 < 15 && k2 < 15; k1++, k2++)
                            {
                                if (grid[k1, k2] == dotColor)
                                {
                                    rb++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if (lt + rb >= 4)
                        {
                            ShowWin(dotColor);
                        }
                        else
                        {
                            k1 = i + 1;
                            k2 = j - 1;
                            for (; k1 < 15 && k2 > -1; k1++, k2--)
                            {
                                if (grid[k1, k2] == dotColor)
                                {
                                    rt++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (rt < 4)
                            {
                                k1 = i - 1;
                                k2 = j + 1;
                                for (; k1 > -1 && k2 < 15; k1--, k2++)
                                {
                                    if (grid[k1, k2] == dotColor)
                                    {
                                        lb++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            if (rt + lb >= 4)
                            {
                                ShowWin(dotColor);
                            }
                        }
                    }
                }

                if (NextdotColor == 0)
                {
                    NextdotColor = 1;
                }
                else if (NextdotColor == 1)
                {
                    NextdotColor = 0;
                }
            }
        }
        /// <summary>出现相邻点的颜色为dotColor</summary>
        /// <param name="dotColor">相邻点的颜色</param>
        private void ShowWin(int dotColor)
        {
            timer.Enabled = false;
            gamePlayer[0].started = false;
            gamePlayer[1].started = false;
            this.ResetGrid();
            //发送格式：Win,相邻点的颜色,黑方成绩,白方成绩
            service.SendToBoth(this, string.Format("Win,{0},{1},{2}",
                dotColor, gamePlayer[0].grade, gamePlayer[1].grade));
        }
        /// <summary>消去棋子的信息</summary>
        /// <param name="i">指定棋盘的第几行</param>
        /// <param name="j">指定棋盘的第几列</param>
        /// <param name="color">指定棋子颜色</param>
        public void UnsetDot(int i, int j, int color)
        {
            //向两个用户发送消去棋子的信息
            //格式：UnsetDot,行,列,黑方成绩,白方成绩
            grid[i, j] = None;
            gamePlayer[color].grade++;
            string str = string.Format("UnsetDot,{0},{1},{2},{3}",
                i, j, gamePlayer[0].grade, gamePlayer[1].grade);
            service.SendToBoth(this, str);
        }
    }
}
