#javascript数据类型

>javascript学习已经有一段时间了，现在总结一下javascript的基础内容，作为一个工具书来用

###变量
javascript是一种弱类型语言，与强类型语言不同，它的变量是泛型的，即可以保存任何类型数据。在使用过程中，变量是没有类型标识的，所以在js中下面代码是正确并可以运行的。
	
	var message = "hello";
	message = 125;//将number赋值给message
	message = true;//将boolean类型赋值给message
	message = null;//将null类型赋值给message
	message = ['h','l','l','l','o'];//将数组类型赋值给message
	message = undefined;//将undefined类型赋值给message


以上代码在Js中并没有错误，但在实际运用中，不推荐这么做。

###数据类型
javascript中有5种基本数据类型：undefined、null、number、boolean、string，以及一种引用类型：object。js中不支持创建自定义类型的机制，所有数据最终都是上述6种数据类型之一。

####数据类型检测
js中提供一个typeof操作符可以对变量内存储的数据的类型进行检测，对一个变量使用typeof会有下面结果：

- "undefined"---如果一个值未定义或已定义未赋值
- "boolean"---如果一个值是布尔值
- "string"---如果变量的值是字符串类型
- "number"---如果变量的值是数值
- "object"---如果变量的值是对象或null
- "function"----如果这个变量是一个对函数的引用

下面是使用typeof的例子

	var Str = "somthing";
	var Bool = true;
	var Nul = null;
	var UnDef = undefined;
	var Num = 35;
	var Fun = function(){ console.log("hello")};

	console.log(typeof Str);
	console.log(typeof Bool);
	console.log(typeof Nul);
	console.log(typeof UnDef);
	console.log(typeof Num);
	console.log(Fun);

####undefined类型
undefined类型是js区别于其他语言的一种特殊的基本类型，以下三种情况变量的值会是undefined:
	
	var message;
	var test = undefined;

	console.log(message);//定义未赋值
	console.log(test);//定义赋值为test
	console.log(typeof assent);//未定义变量，不加typeof会在代码执行是报错

在实际使用中，该类性用第一种情况和第三种情况来判断错误位置。

####null类型
在逻辑角度上看，null是一个空对象指针，所以使用typeof来检测的结果为object类型，该类型通常用来初始化对象。

####Boolean类型
Boolean类型有两种字面值：true和false，javascript中的所有值都可以通过Boolean函数来转换成这两种字面量中的一种。如

	var bool = Boolean("test");
	console.log(bool);//输出true

其他类型通过Boolean()转换成布尔类型的规则如下

<table>
	<tr>
		<td>数据类型</td>
		<td>转换为true值</td>
		<td>转换为false值</td>
	</tr>
	<tr>
		<td>Boolean</td>
		<td>true</td>
		<td>false</td>
	</tr>
	<tr>
		<td>String</td>
		<td>非空字符串</td>
		<td>""</td>
	</tr>
	<tr>
		<td>Number</td>
		<td>任何非0字符串</td>
		<td>0和NaN</td>
	</tr>
	<tr>
		<td>Object</td>
		<td>任何对象</td>
		<td>null</td>
	</tr>
	<tr>
		<td>Undefined</td>
		<td>无</td>
		<td>undefined</td>
	</tr>
</table>

###Number类型
js中的Number类型使用IEEE754格式来表示整形和浮点数值。