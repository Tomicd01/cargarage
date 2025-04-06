setTimeout(function () {
	const alert = document.getElementById('alert');
	if (alert) {
		alert.style.opacity = '0';
		setTimeout(() => {
			alert.style.display = 'none';
		}, 1000); // sačekaj dok se fade out završi (1s)
	}
}, 5000); // 5 sekundi pre nego što krene fade out