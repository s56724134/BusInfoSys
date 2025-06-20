document.addEventListener('DOMContentLoaded', async function () {
	try
	{
		await liff.init({ liffId: '2007424668-B8rkZdQN' });

			//尚未登入：進行登入（不論 LINE App 或瀏覽器）
			if (!liff.isLoggedIn())
			{
				liff.login({ redirectUri: window.location.href });
				return; //login() 後會自動跳轉，所以後面不跑
			}

		// 登入成功，取得 id_token
		const idToken = liff.getIDToken();

		// 傳到後端做驗證
		let response = await fetch('/api/lineuserverify', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({
				idToken: idToken,
				clientId: '2007424668'
			})
		});

			//如果驗證失敗（如 token 過期）
			if (response.status === 401 || response.status === 400)
			{
				console.warn('Token 無效或過期，重新登入以取得新 token');
				liff.logout(); // 清除快取 token
				liff.login({ redirectUri: window.location.href }); // 強制重新登入
				return;
			}
		//驗證成功，取得 userId 或其他資料
		const data = await response.json();
		console.log("驗證成功，LINE 使用者資訊：", data);
	}
	catch (error)
	{
		console.error('LIFF 初始化或驗證過程錯誤:', error);
	}
});


//The keyboard behavior
document.addEventListener('DOMContentLoaded', function () {
    const input = document.getElementById('route');
    //
    document.querySelectorAll('.num-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            input.value += btn.dataset.num;
            input.focus();
        });
    });
    document.querySelector('.keyboard-btn').addEventListener('click', function () {
        input.blur(); // 收起鍵盤
    });
    document.querySelector('.del-btn').addEventListener('click', function () {
        input.value = input.value.slice(0, -1);
        input.focus();
    });
	// click Search-Icon trigger the event that put data to api
	document.querySelector('.Search-Icon').addEventListener('click', async function() {
		await loadData();
	});
});

async function loadData (){
    // Get input value
	let input = document.getElementById('route');
	let value = input.value.trim();
	if (!value) {
		alert('請輸入內容');
		return;
	}
	// save value to localStorage
	let busRouteUrl = `/api/businfo/BusRoute/${encodeURIComponent(value)}`;
	let busStopOfRouteUrl = `/api/businfo/BusStopOfRoute/${encodeURIComponent(value)}`;
	let busRealTimeNearStopUrl = `/api/businfo/RealTimeNearStop/${encodeURIComponent(value)}`;
	let busEstimatedTimeOfArrivalUrl = `/api/businfo/EstimatedTimeOfArrival/${encodeURIComponent(value)}`
	try
	{
		const responses = await Promise.all([
			fetch(busRouteUrl),
			fetch(busStopOfRouteUrl),
			fetch(busRealTimeNearStopUrl),
			fetch(busEstimatedTimeOfArrivalUrl)
		]);

		if (responses.every(res => res.ok))
		{
			const [busRouteJson, busStopOfRouteJson, busRealTimeNearStopJson, busEstimatedTimeOfArrival] = await Promise.all(responses.map(r => r.json()));
			console.log(responses);
			localStorage.setItem('busRouteInputValue', value);
			localStorage.setItem('busRouteResult', JSON.stringify(busRouteJson));
			localStorage.setItem('busStopOfRouteResult', JSON.stringify(busStopOfRouteJson));
			localStorage.setItem('busRealTimeNearStop', JSON.stringify(busRealTimeNearStopJson));
			localStorage.setItem('busEstimatedTimeOfArrival', JSON.stringify(busEstimatedTimeOfArrival));
			window.location.href = '/BusStopOfRoute.html';
		}
		else
		{
			alert('查詢失敗');
		}
	}
	catch (err)
	{
		alert('查詢失敗');
	}

}

