/*
*date:2014.11.12
*function: 查看用户用什么设备上网。
*/
function isPc(){

	var userAgentInfo = navigator.userAgent;
	var keywordsOfMobile = ["Android","iPhone","SymbianOS","Windows Phone","iPad","iPod"];

	for(var i=0;i<keywordsOfMobile.length;i++)
	{
		if(userAgentInfo.indexOf(keywordsOfMobile[i])>0)
		{
			return false;
		}
	}

	return true;
}

/*
*date:2014.11.12
*function:根据传入对象生成link标签
*目前规定obj有三个属性
*/
function lazyLink(obj){

	if(!obj.type && !obj.rel && ! obj.href)
	{
		return ;
	}

	var oLink = document.createElement("link");
	oLink.setAttribute("type",obj.type);
	oLink.setAttribute("rel",obj.rel);
	oLink.setAttribute("href",obj.href);
	document.getElementsByTagName("head")[0].appendChild(oLink);

}