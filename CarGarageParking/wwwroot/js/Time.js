function Time() {
	var now = new Date();

	document.getElementById("currentTime").innerText = now.toLocaleTimeString();
}
setInterval(Time, 1000);
Time();