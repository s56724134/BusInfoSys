// This init the liff
document.addEventListener('DOMContentLoaded', function () {
	liff.init({
		liffId: '2007424668-B8rkZdQN' // 請替換成你的 LIFF ID
	}).then(() => {
		// 初始化成功
		console.log('LIFF 初始化成功');
	}).catch((err) => {
		// 初始化失敗
		console.error('LIFF 初始化失敗:', err);
	});
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
	document.querySelector('.Search-Icon').addEventListener('click', loadData);

});

async function loadData (){
    // get input value
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

