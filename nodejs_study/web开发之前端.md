#WEB开发之前端数据提交

>浏览器前端数据与后台数据是如何进行交互的呢？web前端技术提供了几种方法:1、form表单，2、ajax，3、websocket，4、跨文档消息传输。本文重点介绍前两种方法，其他方法的详细处理将在以后补充完整。
>

###http请求介绍
浏览器和服务器之间是采用http协议进行通信的，所以在了解前端数据提交前我们要先了解http请求方式。http协议中提供了很多中请求方式，例如：head、get、post、put、options、delete等，其中最常用的是get和post请求方式，现在说http请求方式基本上都是指这两种方式。以下介绍一下这两种方式
1)、get请求的使用方式是将请求信息附加在URL上，这种请求方式有以下特点

- get请求会在浏览器地址栏中显示，能够被浏览器缓存，并且可以保存在浏览器的历史记录中
- http协议中对get请求的长度并没有限制，但多数(或者说全部浏览器)对get的请求做了长度限制，其中ie的最短为2kb，为了考虑兼容方面问题，开发上将get限制为2kb的长度。
- 多数浏览器在发送form表单请求报文时，有这样的一个行为：先发送http报文的header部分，然后再发送http报文的body部分，get请求的数据是在header部分发送的，所以效率比较高
- get请求的数据是明文附加在URL上的(也有通过escape函数进行编码的，unescape就可以解码了，跟明文没什么差别)，所以安全性差
- get请求通常用于幂等请求(请求任何次返回同样的结果)和向服务器拉取数据

get的方法很常见，就比如说我用百度来搜索"寄生兽next to you",就是一个get请求，地址栏URL?号后面的内容就是请求内容。

	http://www.baidu.com/s?wd=%E5%AF%84%E7%94%9F%E5%85%BDnext%20to%20you&rsv_spt=1&issp=1&f=8&rsv_bp=0&rsv_idx=2&ie=utf-8&tn=baiduhome_pg&rsv_enter=1&rsv_pq=8948c55e000158a8&rsv_t=0a95hrC%2B5SCTCUkyGtPonxVH8q2FwvnW9tsgO7EJgjnS%2FNRNCmJUpxFTKF4QeZnD%2F5CC&inputT=7805&rsv_sug3=308&rsv_sug6=6&rsv_sug1=160&rsv_sug2=0&rsv_sug4=9096

2)、post请求的内容是编码后附加到http报文的body部分，这种请求的方式的特性是：

- post请求的信息不会显示在地址栏上，不能被浏览器缓存，也不能保存在历史记录中
- http协议对post请求也没有大小限制，浏览器通常也不限制post请求的大小，但服务器通常对post请求有限制，php中是3-8M。
- 由于post请求方式的数据是附加在http报文的body部分，根据form表单发送http请求的特性，它的效率会比ge请求低
- post请求方式的数据是附加在http报文的body部分，安全性方面比get请求好，但也是相对而已，如果要好的安全性要采用https协议
- post请求方式通常用户向服务器传递大数据和私密信息


###form表单
介绍完http请求，我们来写一个form表单向服务器提交用户名和密码。
1、form标签是form表单的最基本元素。form标签有两个重要属性：method和action，method属性是指明表单将采取get/post方式提交到服务器，如果不填写该值，表单将以get方式提交数据；action属性是指明表单提交到服务器中的哪个处理逻辑，如果不填该值，表单将无法提交。html中表单的写法如下所示：
	
	<form method="get" action="http://localhost/index.php"></form>

2、form表单建好了，但打开网页看是一片空白，也无法向服务器提交数据。现在我们想表单里面添加两个输入框和一个按钮，输入框要注意一个name属性，该属性是表单提交数据的标识，也是服务器接收数据的标识。添加完成后代码如下所示：

	<form method="get" action="http://localhost/index.php">
		<p><label>用户名：</label><input type="text" name="username"></p>
		<p><label>密码：</label><input type="password" name="password"></p>
		<p><button>submit</button></p>
	</form>

3、好了，到这里表单就基本完成了，下面是一个登录界面的完整代码：

	<!DOCTYPE html>
	<html>
		<head>
			<meta charset="utf-8" >
			<title>提交登录数据</title>
		</head>
		<body>
			<form method="get" action="http://localhost/index.php">
				<fieldset>
					<legend>用户登录</legend>
					<p><label>用户名：</label><input type="text" name="username"></p>
					<p><label>密码：</label><input type="password" name="password"></p>
					<p><button>submit</button></p>
				</fieldset>
			</form>
		</body>
	</html>

4、表单弄好了，我们来写PHP服务器index.php代码：

	<?php
		//将浏览器传递过来的数据取出
		$username = $_REQUEST["username"];
		$password = $_REQUEST["password"];
		//输出这两个数据看是否正确
		echo "hello ${username} , you password is ${password}!";
	?>


###ajax
form表单提交数据需要跳转页面或会阻塞页面的其他操作，为了提供更好的交互，我们可以ajax来提交表单，下面演示一段通过jquery封装的ajax代码来提交数据。代码如下：
	
	<div class="clickBtn">click show message</div>
	<script type="text/javascript" src="javascripts/jquery-2.1.3.min.js"></script>
	<script type="text/javascript">
	$.ajax({
		url:"http://localhost/index.php?username=acdliu&password=hey",
		type:"get",
		data:"null",
		timeout:2000,
		success:function(data){
			$(".clickBtn").html(data);
			},
		error:function(){
			alert('something is error!');
		}
		});
	</script>




###本文结语
写本文时，表达、用户和说明方式都有很大的问题，主要注重方面是实践，理论方面解释的不够清晰，以后会对本文进行改进。
本文参考：http://blog.chinaunix.net/uid-21778123-id-1815443.html