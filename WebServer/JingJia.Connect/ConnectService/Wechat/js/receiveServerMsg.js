function WebSocketTest() {
	if ("WebSocket" in window) {
		console.log("您的浏览器支持 WebSocket!");

		// 打开一个 web socket
		var ws = new WebSocket(WebSocketUrl);

		ws.onopen = function() {
			console.log("WebSocket已打开!");
		};

		ws.onmessage = function(evt) {
			console.log(evt);
			mui.alert(evt.msg, evt.title,'确定',function(){
				console.log("确定之后的动作!");
			});
		};

		ws.onclose = function() {
			console.log("WebSocket已关闭!");
		};
	} else {
		// 浏览器不支持 WebSocket
		console.log("您的浏览器不支持 WebSocket!");
	}
}
