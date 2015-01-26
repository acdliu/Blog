#WEB开发之前端数据提交

>浏览器和服务器之间的通信是基于http协议的，所以浏览器可以通过http的请求方式来向服务器请求资源和服务，其中最常
>用的请求方式是get请求和post请求，本文将介绍前端代码向服务器提交请求。

###get方法
使用get方法时，请求的内容会以明文形式附加在url地址后面一起发送到服务器。get请求具有以下特点：
- get请求能够被浏览器缓存，能保存在浏览器的历史记录中。
- get请求在http协议中并没有限制，但在浏览器中的实现不同，而在ie实现中长度最小，所以一般以ie的限制2kb为get请求的长度限制。
- get请求实际上只有一个头文件，所以效率上会比post高。
- get请求的安全性差，附加在url上的内容可以被别人知悉，所以一般用来请求数据，而不用与私密数据的提交。

###post方法
使用post方法时，请求数据会被附加到http请求体中发送到服务器。post请求具有以下特点：
- post请求不能被浏览器缓存，也不能保存在历史记录中。
- post请求在http协议中大小也没有限制，但通常在服务器端有post最大值限制，具体多大要看服务器配置。
- post请求内容是附加在http协议请求体中，多数浏览器对于post请求采用两阶段发送数据，先发送请求头，再发送请求体，所以效率方面比get请求低。
- post请求是不可见的，安全性比get请求高一点

###通过form表单提交请求
向服务器提交http请求的最简单的方式是通过html的表单元素来提交，表单元素的基本结构如下面所示:

	<form id="form" method="get" action="http://localhost/index.html">
		<fieldset>
		<legend></legend>
        <p>
            <label for="username">用户名：</label>
            <input type="text" id="username" name="username" >
        </p>
        <p>
            <label for="username">用户名：</label>
            <input type="text" id="username" name="username" >
        </p>
        <button>submit</button>
        </fieldset>
    </form>

form表单的重要元素和属性介绍
- 1、form元素是form表单的基本，它有两个属性要注意：method和action。method属性是指明表单通过get/post方法提交到服务器，该属性的默认值是get。action是指明表单提交到服务器的那个处理逻辑，如果不填这个数据就提交不到服务器。
- 2、input元素的name属性，该值指定浏览器要提交的数据以及在服务器通过什么变量来访问浏览器提交的数据。
- 3、button元素，表单的提交必须通过一个按钮来触发提交操作。

###通过ajax提交请求
通过form表单提交/请求数据必须刷新页面才能看到新的内容，但是有一种技术它可以通过浏览器后台与服务器进行数据交互而不刷新页面或阻塞用户操作，它叫ajax。如果不想网页请求数据时阻塞用户操作而导致恶劣的用户体验，可以使用这种方式来提交/请求数据。

	<script>
     	//创建表单对象，这里可以传递一个DOM对象来初始化表单数据对象。
     	var oForm = new FormData();
     	//添加一个username数据
     	oForm.username="username";
     	/*添加一个password数据*/
     	oForm.password="password";
     	//通过ajax来提交表单
		function ajax(method,url,data,callback){

                var req = null;
                try{
                     req = new XMLHttpRequest();
                }catch(){
                     req = new ActiveXObject("Microsoft.XMLHTTP");
                }

                req.onreadystatechange = function(){

                    if(req.readyState == 4)
                    {
                        if(req.status == 200)
                        {
                            console.log("success");
                        }
                        else
                        {
                            console.log("error");
                        }
                    }
                }
                req.open(method,url,false);
                req.send(data);

        }

		ajax("post","http://localhost/index",oForm);
	</script>


###通过jquery提交请求
ajax在ie和其他浏览器中有兼容性问题，并且像上面写的代码无法解决请求超时的情况，我们可以使用jquery提供的ajax、get、post函数来提交数据，只要配置相应的信息即可。

	$.ajax({
		url:"http://localhost/index",//请求处理路径
		type:'get',//请求方式
		data:null,//请求数据，get方式为null，post方式附加数据
		timeout:3000,//设置请求超时时间
		success:function(){console.log("success")},//请求成功的处理函数
		error:function(){console.log("error")}//请求失败的处理函数
		});