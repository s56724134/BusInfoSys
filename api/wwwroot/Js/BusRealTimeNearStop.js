// Wait the "displayStopFromDeparture() from "BusStopOfRoute.JS" completed and then trigger function
document.addEventListener('StopItemRendered', function(){
    // setDirectionAtElem();
    // setBusPositionWithDirectionZero();
    // setBusPositionWithDirectionOne();
    updateStopItemsWithBusAndTime(0);
    updateStopItemsWithBusAndTime(1);

});

async function fetchAndUpdateBusRealTime() {
    let inputValue = localStorage.getItem('busRouteInputValue');
    if (!inputValue) return;
    let busRealTimeNearStopUrl = `/api/businfo/RealTimeNearStop/${encodeURIComponent(inputValue)}`;
    let busEstimatedTimeOfArrival = `/api/businfo/EstimatedTimeOfArrival/${encodeURIComponent(inputValue)}`;
    try
    {
        const responses = await Promise.all([
			fetch(busRealTimeNearStopUrl),
			fetch(busEstimatedTimeOfArrival)
		]);
        if (responses.every(res => res.ok))
        {
            const [realTimeData, estimatedData] = await Promise.all(responses.map(res => res.json()));
            localStorage.setItem('busRealTimeNearStop', JSON.stringify(realTimeData));
            localStorage.setItem('busEstimatedTimeOfArrival', JSON.stringify(estimatedData));
            updateStopItemsWithBusAndTime(0);
            updateStopItemsWithBusAndTime(1);
        }
        else
        {
            console.error('有 API 回傳失敗', responses);
        }
    }
    catch (error)
    {
        console.error('發生錯誤:', error);
    }
}

// 每15秒自動更新
setInterval(fetchAndUpdateBusRealTime, 15000);

// 頁面載入時先執行一次
fetchAndUpdateBusRealTime();



//All of "clearAllBusIcons","resetAllTimeBox","renderEstimatedTime","renderBusAndInStop",insert point.
function updateStopItemsWithBusAndTime(direction) {
    let stopContainer = document.getElementById(direction === 0 ? 'Direction-Zero' : 'Direction-One');
    let estimatedData = JSON.parse(localStorage.getItem('busEstimatedTimeOfArrival') || '[]');
    let realTimeData = JSON.parse(localStorage.getItem('busRealTimeNearStop') || '[]');

    clearAllBusIcons(stopContainer);
    resetAllTimeBox(stopContainer);

    renderEstimatedTime(stopContainer, estimatedData, direction);
    renderBusAndInStop(stopContainer, realTimeData, direction);
}

// Clear busIcon when update page
function clearAllBusIcons(stopContainer) {
    stopContainer.querySelectorAll('.BusContainer').forEach(el => el.remove());
}

// Reset time when update page
function resetAllTimeBox(stopContainer) {
    stopContainer.querySelectorAll('.TimeBox').forEach(tb => tb.textContent = '');
}


function renderEstimatedTime(stopContainer, estimatedData, direction) {
    for (let est of estimatedData) {
        if (est.direction !== direction) continue;
        let stopItem = stopContainer.querySelector(`#StopSequence-${est.stopSequence}`);
        if (!stopItem) continue;
        let timeBox = stopItem.querySelector('.TimeBox');
        if (!timeBox) continue;

        timeBox.textContent = getEstimatedTimeText(est);
    }
}

function getEstimatedTimeText(est) {
    if (est.estimateTime === 0) {
        return formatNextBusTime(est);
    }
    if (est.estimateTime > 120) {
        return changeEstimatedTimeToMinute(est) + '分';
    }
    return "即將進站中"
}

let busRouteInputValue = localStorage.getItem('busRouteInputValue');
function renderBusAndInStop(stopContainer, realTimeData, direction) {
    for (let bus of realTimeData) {
        if (bus.direction == direction && bus.routeID == busRouteInputValue)
        {
            let stopItem = stopContainer.querySelector(`#StopSequence-${bus.stopSequence}`);
            if (!stopItem) continue;
            let timeBox = stopItem.querySelector('.TimeBox');
            if (timeBox) timeBox.textContent = "進站中";
            createBusContainerClass(bus, stopItem);
        }
    }
}


function changeEstimatedTimeToMinute(busEstimatedTimeOfArrival) {
    let unAdjustTime = busEstimatedTimeOfArrival.estimateTime;
    let time = unAdjustTime / 60;
    return time;
}

// This function is format time
function formatNextBusTime(busEstimatedTimeOfArrival) {
    let unAdjustTime = busEstimatedTimeOfArrival.nextBusTime;
    let date = new Date(unAdjustTime);

    const time = date.toLocaleTimeString("zh-TW", {
        hour: "2-digit",
        minute: "2-digit",
        // 24hour sys
        hour12: false
    });

    return time;
}
function createBusContainerClass(bus, stopItem)
{
    let busContainer = document.createElement('div');
    let busIcon = document.createElement('span');
    let busPlateNumb = document.createElement('span');
    // busContainer
    busContainer.className = 'BusContainer';
    // busIcon
    busIcon.className = 'BusIcon';
    busIcon.textContent = '🚌';
    // busPlateNumb
    busPlateNumb.className = 'BusPlateNumb';
    busPlateNumb.textContent = `${bus.plateNumb}`;

    busContainer.appendChild(busIcon);
    busContainer.appendChild(busPlateNumb);
    stopItem.appendChild(busContainer);
}

