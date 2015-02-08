var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');

var routes = require('./routes/index');
var users = require('./routes/users');

var app = express();
//添加两个依赖模块
var ejs = require("ejs");
var mysql = require("mysql");
//创建mysql连接
var conn = mysql.createConnection({
    host:'localhost',
    user:'root',
    password:'',
    database:'test'
});


// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.engine('.html',ejs.__express);
app.set('view engine', 'html');

// uncomment after placing your favicon in /public
//app.use(favicon(__dirname + '/public/favicon.ico'));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));


//拦截并处理/login路径的请求
app.get('/login',function(req,res,next){

    return res.render('login',{title:"用户登录界面"});
});
//拦截并处理/loginLogic路径的请求
app.post('/loginLogic',function(req,res,next){

    console.log(req.url);
    //先获取用户传递过来的请求信息
    var usernameB = req.body.username;
    console.log(usernameB);
    var passwordB = req.body.password;
    console.log(passwordB);
    
    //拼接sql语句
    var sql = 'select * from user where username="'+usernameB+'" and password="'+passwordB+'"';
    console.log(sql);
    //因为数据库连接是异步的，所以要一个回调来处理异步的结果
    var selectCallBack = function(result){

        if(result.length > 0)
        {
            return res.render('index',{username:usernameB,password:passwordB});
        }else
        {
            return res.redirect('/login');
        }
    }
    //执行数据库查询语句
    conn.query(sql,function(err,res,fields){
        //执行语句时遇到异常，抛出err
        if(err)
        {
            throw err;
        }

        console.log("sql query success");
        selectCallBack(res);
    });

});

// catch 404 and forward to error handler
app.use(function(req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function(err, req, res, next) {
        res.status(err.status || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}

// production error handler
// no stacktraces leaked to user
app.use(function(err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
        message: err.message,
        error: {}
    });
});


module.exports = app;
