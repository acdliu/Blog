#关于promise/A模式的学习及使用

> 很久之前就接触过promise模式，它是一个包含三个状态的对象，用于解决异步嵌套的问题，但是在实际使用中很少用到这个东西。最近玩了下nodejs，里面的方法基本上都是异步的，用回调的方法感觉有点别扭，所以想用promise模式来改善一下。
>

###promise的相关概念
以下promise的相关概念是借用该[博客](http://jishu.zol.com.cn/210138.html)的介绍
####什么是promise
按照官方的说明，promise是一个异步操作的最终结，它具有三个状态：pending(等待)，fulfilled(完成),rejected(拒绝)。它可以使异步代码像同步代码那样书写和阅读，下面以一个ajax为例子。

	//普通的异步方式
	$.get('/js/script.js',function(){
		//这里可以使用一个回调来完成后续操作
	});

	//使用promise
	var promise = $.get('/js/script.js');
	/*promise.resolve(data);触发事件完成
	promise.rejected(data);触发事件失败*/
	promise.done(function(){});
	promise.fail(function(){});

可以看出，它实际上也是异步的，但这个写起来像同步的，而且代码执行起来也是同步的，可维护性也比普通的异步方式高。

####jquery中的常用函数

- 1、$.Deferred(),生成promise对象
	在jquery中生成promise对象可以通过$.Defferred()方法来生成一个promise对象，也可以通过一些$.get()、$.post()之类返回promise对象的方法来生成promise对象。
- 2、Promise.then(fullfilled,rejected),绑定promise对象处理函数
	then函数接收两个函数作为参数，分别绑定fulfilled和rejected状态的处理函数。
- 3、Promise.done(function(){})，相当于then绑定的第一个参数，用于处理fulfilled状态
- 4、Promise.fail(function(){})，相当于then绑定的第二个参数，用于处理rejected状态

###在nodejs中使用promise对象控制流
在nodejs项目中，我是采用node-mysql来处理数据库连接的，但node-mysql中的所有的方法都是异步的，需要通过回调来处理。现在借助bluebird框架来让后台代码看起来像同步代码。

- 1、首先要将node-mysql的代码同步化，通过下面的代码可以生成一个同步版的node-mysql，它的同步方法为node-mysql原方法加Async后缀。
   
	var promise = require("bluebird");
	var mysql = require("mysql");
	promise.promisifyAll(mysql);
	promise.promisifyAll(require("mysql/lib/Connection").prototype);
	promise.promisifyAll(require("mysql/lib/Pool").prototype);

- 2、然后node-mysql的查询方法可以按以下方式写
	
	con.queryAsync("select * from user")
	.then(function(){
		console.log("success!");
	},
	function(){
		console.log("error!");
	});

promise的使用讲解完了，如果需要深入研究，可以看下bluebird的官方文档，还有很多方法。在实际中用bluebird会使代码更好看些，实际使用时跟同步方法很类似，但终究不是同步方法，是用起来没同步方法舒服。
