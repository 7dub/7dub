function hello() {
	var obj ={
		"name":"1111",	
		"desc":"this is a string from 7dub"
	};
	return obj;
// 	return JSON.stringify(obj);
}

function analyse(data) {
	var obj ={};
	var bs = data.split("&");
	if(bs.length > 0){
		for(var i=0;i<bs.length;i++){
			var item = bs[i].split("=");
			obj.item[0] = item[1];
		}
	}
// 	return JSON.stringify(obj);
	return obj;
}
