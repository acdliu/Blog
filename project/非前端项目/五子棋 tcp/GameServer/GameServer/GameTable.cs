//-------------------GameTable.cs-----------------//
using System;
using System.Timers;
using System.Windows.Forms;
namespace GameServer
{
    class GameTable
    {
        public int isAllStart = 0;

        private const int None = -1;       //������
        private const int Black = 0;       //��ɫ����
        private const int White = 1;       //��ɫ����
        public Player[] gamePlayer;        //����ͬһ���������Ϣ
        private int[,] grid = new int[15, 15];       //15*15�ķ���
        private System.Timers.Timer timer;       //���ڶ�ʱ��������
        private int NextdotColor = -1;            //Ӧ�ò��������ӻ��ǰ�����
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
        /// <summary>��������</summary>
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
        /// <summary>����Timer</summary>
        public void StartTimer()
        {
            timer.Start();
            isAllStart = 1;
        }
        /// <summary>ֹͣTimer</summary>
        public void StopTimer()
        {
            timer.Stop();
            isAllStart = 0;
        }
        /// <summary>����ʱ����</summary>
        /// <param name="interval">ʱ����</param>
        public void SetTimerLevel(int interval)
        {
            timer.Interval = interval;
        }
        /// <summary>�ﵽʱ����ʱ�����¼�</summary>
        private void timer_Elapsed(object sender, EventArgs e)
        {
            //int x, y;
            ////�������һ������û�����ӵĵ�Ԫ��λ��
            //do
            //{
            //    x = rnd.Next(15);  //����һ��С��15�ķǸ�����
            //    y = rnd.Next(15);
            //} while (grid[x, y] != None);
            ////��������:x����,y����,��ɫ
            //SetDot(x, y, NextdotColor);
            ////�趨�´ηַ���������ɫ
            //NextdotColor = (NextdotColor + 1) % 2;
        }
        /// <summary>���Ͳ�����������Ϣ</summary>
        /// <param name="i">ָ�����̵ĵڼ���</param>
        /// <param name="j">ָ�����̵ĵڼ���</param>
        /// <param name="dotColor">������ɫ</param>
        public void SetDot(int i, int j, int dotColor)
        {
            /*
             * ��������ʱ�����Ƿ����ֵ����£�����ֵ����£����з�Ӧ����������ֵ����£�������Ӧ
             */
            if (NextdotColor == -1)
            {
                NextdotColor = dotColor;
            }

            if (NextdotColor == dotColor)
            {
                //�������û����Ͳ�����������Ϣ�����ж��Ƿ�����������
                //���͸�ʽ��SetDot,��,��,��ɫ
                grid[i, j] = dotColor;
                service.SendToBoth(this, string.Format("SetDot,{0},{1},{2}", i, j, dotColor));

                int nl = 0, nr = 0, nt = 0, nb = 0, lt = 0, lb = 0, rt = 0, rb = 0;
                /*----------�����жϵ�ǰ���Ƿ񹹳���������----------*/
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
                /*----------�����жϵ�ǰ���Ƿ񹹳���������----------*/
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
                    /*----------�����ж�б���Ƿ񹹳���������----------*/
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
        /// <summary>�������ڵ����ɫΪdotColor</summary>
        /// <param name="dotColor">���ڵ����ɫ</param>
        private void ShowWin(int dotColor)
        {
            timer.Enabled = false;
            gamePlayer[0].started = false;
            gamePlayer[1].started = false;
            this.ResetGrid();
            //���͸�ʽ��Win,���ڵ����ɫ,�ڷ��ɼ�,�׷��ɼ�
            service.SendToBoth(this, string.Format("Win,{0},{1},{2}",
                dotColor, gamePlayer[0].grade, gamePlayer[1].grade));
        }
        /// <summary>��ȥ���ӵ���Ϣ</summary>
        /// <param name="i">ָ�����̵ĵڼ���</param>
        /// <param name="j">ָ�����̵ĵڼ���</param>
        /// <param name="color">ָ��������ɫ</param>
        public void UnsetDot(int i, int j, int color)
        {
            //�������û�������ȥ���ӵ���Ϣ
            //��ʽ��UnsetDot,��,��,�ڷ��ɼ�,�׷��ɼ�
            grid[i, j] = None;
            gamePlayer[color].grade++;
            string str = string.Format("UnsetDot,{0},{1},{2},{3}",
                i, j, gamePlayer[0].grade, gamePlayer[1].grade);
            service.SendToBoth(this, str);
        }
    }
}
