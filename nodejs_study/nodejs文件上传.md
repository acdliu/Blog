#nodejs之文件上传

###formidable中间件
formidable是一个提供处理表单文件上传的中间件，可以通过npm来安装.
	
	npm install formidable


###form表单文件上传
form表单上传文件在前端要注意在form标签中加入enctype属性，

	<form enctype="multipart/form-data"></form>

###formidable的简单使用

	var formidable = require('formidable');

	app.post("/upload",function(res,req,next){
	
		var form = new formidable.IncomingForm();
		//设置文件存储目录
		form.uploadDir = "./pic";
		form.keepExtensions = true;//设置保留后缀
		form.maxFieldsSize = 10*1024*1024;//设置数据大小上限
		form.parse(req,function(err,fields,files){
			if(err)
				throw err;

			res.send("you has post jpg success");
		});
	});


###formidable的简单的api