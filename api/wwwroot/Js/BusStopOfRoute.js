// Add EnentListnter
document.addEventListener('DOMContentLoaded', async function(){
    await loadNavbar();
    startProgressBar();
    getRouteInfo();
    getBusStopOfRoute();
    displayStopFromDeparture();
    changeDirectionTabs();
});


// fetch navbar.html
async function loadNavbar() {
    const res = await fetch('../common/navbar.html');
    // console.log("fetch 狀態", res.status);
    const html = await res.text();
    document.getElementsByClassName('NavBar-Holder')[0].innerHTML = html;
}

// This is Progress Bar function
function startProgressBar() {
    const bar = document.querySelector('.ProgressBar');
    if (!bar) return;
    let duration = 15000; // 15秒
    function animate() {
        bar.style.transition = 'none';
        bar.style.width = '0%';
        // 強制重繪，確保 transition 生效
        void bar.offsetWidth;
        bar.style.transition = `width ${duration}ms linear`;
        bar.style.width = '100%';
    }
    bar.addEventListener('transitionend', animate);
    animate();
}

// get route from localStorage
function getRouteInfo() {
    let busRouteStr = localStorage.getItem('busRouteResult');
    let inputValue = localStorage.getItem('busRouteInputValue');
    if (busRouteStr && inputValue) {
        let busRoute = JSON.parse(busRouteStr);
        for (let i = 0; i < busRoute.length; i++)
        {
            if (busRoute[i].routeID == inputValue)
            {
                let tabs = document.querySelectorAll('.Direction-Tab');
                tabs[0].textContent = '往 ' + busRoute[i].destinationStopNameZh;
                tabs[1].textContent = '往 ' + busRoute[i].departureStopNameZh;
            }
        }
    }
}

// get busStopofRoute
function getBusStopOfRoute(direction) {
    // First Parsse string data to json
    let busStopOfRouteStr = localStorage.getItem("busStopOfRouteResult");
    let busStopOfRoutes = JSON.parse(busStopOfRouteStr);
    let inputValue = localStorage.getItem('busRouteInputValue');
    for (let i = 0; i < busStopOfRoutes.length; i++)
    {
        if (inputValue == busStopOfRoutes[i].routeID && busStopOfRoutes[i].direction == direction)
        {
            return busStopOfRoutes[i];
        }

        if (inputValue == busStopOfRoutes[i].routeID && busStopOfRoutes[i].direction == direction)
        {
            return busStopOfRoutes[i];
        }
    }
}

// Adjust StopContainer margintop because that "NavBar-Holder" class and "Direction-Tabs" class have position:fixed
function adjustStopContainerMarginTop() {
    let navBar = document.querySelector('.NavBar-Holder');
    let directionTabs = document.querySelector('.Direction-Tabs');
    let stopContainer = document.querySelectorAll('.StopContainer');

    if (navBar && directionTabs && stopContainer)
    {
        let navBarHeight = navBar.offsetHeight;
        let directionTabsHeight = directionTabs.offsetHeight;
        let marginTop = (navBarHeight + directionTabsHeight) + 'px';
        stopContainer.forEach(container => {container.style.marginTop = marginTop});
    }
}

function changeDirectionTabs() {
    // first set a eventListener that listening button click
    let tabs = document.querySelectorAll(".Direction-Tab");
    let tabsDeparture = tabs[0];
    let tabsDestination = tabs[1];
    tabsDeparture.addEventListener('click', () => displayStopFromDeparture());
    tabsDestination.addEventListener('click', () => displayStopFromDestination());
}

function displayStopFromDeparture() {
    // get all Direction-Tab
    let stopContainerWithZero = document.getElementById('Direction-Zero');
    let stopContainerWithOne = document.getElementById('Direction-One');
    let direction = 0;
    let busStopRouteJson = getBusStopOfRoute(direction);
    // check the getIsDirectionZero value
    stopContainerWithZero.style.display = 'block';
    stopContainerWithOne.style.display = 'none';
    adjustStopContainerMarginTop();
    stopContainerWithZero.innerHTML = '';

    for(let i=0; i<busStopRouteJson.stops.length; i++)
    {
        stopContainerWithZero.innerHTML +=
            `<div class="StopItem" id="StopSequence-${busStopRouteJson.stops[i].stopSequence}">
                <div class="TimeBox"></div>
                <div class="Stop-Sequence">${busStopRouteJson.stops[i].stopSequence}</div>
                <div class="StopContent">
                    <div class="StopName">${busStopRouteJson.stops[i].stopName.zh_tw}</div>
                </div>
            </div>`;
    }
    document.dispatchEvent(new Event('StopItemRendered'));
}

function displayStopFromDestination()
{
    let stopContainerWithZero = document.getElementById('Direction-Zero');
    let stopContainerWithOne = document.getElementById('Direction-One');
    let direction = 1;
    let busStopRouteJson = getBusStopOfRoute(direction);
    // check the getIsDirectionZero value
    stopContainerWithOne.style.display = 'block';
    stopContainerWithZero.style.display = 'none';
    adjustStopContainerMarginTop();
    stopContainerWithOne.innerHTML = '';
    for(let i=0; i<busStopRouteJson.stops.length; i++)
    {
        stopContainerWithOne.innerHTML +=
            `<div class="StopItem" id="StopSequence-${busStopRouteJson.stops[i].stopSequence}">
                <div class="TimeBox">
                </div>
                <div class="Stop-Sequence">${busStopRouteJson.stops[i].stopSequence}</div>
                <div class="StopContent">
                    <div class="StopName">${busStopRouteJson.stops[i].stopName.zh_tw}</div>
                </div>
            </div>`;
    }
    document.dispatchEvent(new Event('StopItemRendered'));
}

