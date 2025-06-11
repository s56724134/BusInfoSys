// document.addEventListener('StopItemRendered', function () {
//     setBusEstimatedTimeAtDirectionZero();
//     setBusEstimatedTimeAtDirectionOne();
// })

// function setBusEstimatedTimeAtDirectionZero () {
//     // check the direction and stopSequence
//     let data = localStorage.getItem('busEstimatedTimeOfArrival');
//     let busEstimatedTimeOfArrivals = JSON.parse(data);
//     for (let i = 0; i < busEstimatedTimeOfArrivals.length; i++)
//     {
//         let busEstimatedTimeOfArrival = busEstimatedTimeOfArrivals[i];
//         if (busEstimatedTimeOfArrival.direction == 0 && busEstimatedTimeOfArrival.estimateTime == 0)
//         {
//             let stopSequence = busEstimatedTimeOfArrival.stopSequence;

//             let stopContainer = document.getElementById('Direction-Zero');
//             let stopItem = stopContainer.querySelector(`#StopSequence-${stopSequence}`);
//             let timeBox = document.createElement('div');
//             let time = formatNextBusTime(busEstimatedTimeOfArrival);
//             timeBox.className = 'TimeBox';
//             timeBox.textContent = time;

//             stopItem.prepend(timeBox);
//         }
//         if (busEstimatedTimeOfArrival.direction == 0 && busEstimatedTimeOfArrival.estimateTime != 0)
//         {
//             let stopSequence = busEstimatedTimeOfArrival.stopSequence;

//             let stopContainer = document.getElementById('Direction-Zero');
//             let stopItem = stopContainer.querySelector(`#StopSequence-${stopSequence}`);
//             let timeBox = document.createElement('div');
//             let time = changeEstimatedTimeToMinute(busEstimatedTimeOfArrival);
//             timeBox.className = 'TimeBox';
//             timeBox.textContent = time + "分";

//             stopItem.prepend(timeBox);
//         }
//     }
// }

// function setBusEstimatedTimeAtDirectionOne () {
//     busEstimatedTimeOfArrival

// }

// function changeEstimatedTimeToMinute(busEstimatedTimeOfArrival) {
//     let unAdjustTime = busEstimatedTimeOfArrival.estimateTime;
//     let time = unAdjustTime / 60;
//     return time;
// }

// // This function is format time
// function formatNextBusTime(busEstimatedTimeOfArrival) {
//     let unAdjustTime = busEstimatedTimeOfArrival.nextBusTime;
//     let date = new Date(unAdjustTime);

//     const time = date.toLocaleTimeString("zh-TW", {
//         hour: "2-digit",
//         minute: "2-digit",
//         // 24hour sys
//         hour12: false
//     });

//     return time;
// }
