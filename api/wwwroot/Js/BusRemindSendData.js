// Waits for a specific event to occur on a given target and resolves a Promise when the event is triggered.
// Usage: await waitForEvent('click', someElement);
document.addEventListener('DOMContentLoaded', function (){
    //
    document.querySelector('.Confirm-Btn').addEventListener('click', sendRemindDataToServer);
})

function sendRemindDataToServer() {
    let userIDToken = getUserIDToken();
    let userRouteName = getUserRouteName();
    let userStopName = getUserStopName();
    let userStopId = getUserStopId();
    let userSelectedRemindTime = getSelectedRemindTime();
    let userRepeatWeekTime = getRepeatWeekTime();
    let [showUpStart, showUpEnd] = getBusShowUpTime();

    //Create Json Object
    const data = {
        userIDToken,
        userRouteName,
        userStopName,
        userStopId,
        userSelectedRemindTime,
        userRepeatWeekTime,
        userBusShowUpTime: {
            start: showUpStart,
            end: showUpEnd
        }
    }
    console.log(data);
    // const jsonData = JSON.stringify(data);
    // // 用 fetch 傳送到後端
    // fetch('/api/bus-remind', {
    //     method: 'POST',
    //     headers: {
    //         'Content-Type': 'application/json'
    //     },
    //     body: jsonData
    // })
    // .then(response => response.json())
    // .then(result => {
    //     console.log('後端回應：', result);
    //     })
    // .catch(error => {
    //     console.error('傳送失敗：', error);
    //     });
}

function getUserIDToken() {
    return localStorage.getItem('LIFF_STORE:2007424668-B8rkZdQN:IDToken');
}

function getUserRouteName() {
    return localStorage.getItem('busRouteInputValue');
}

function getUserStopName() {
    return sessionStorage.getItem('busRemindstopName');
}

function getUserStopId() {
    return sessionStorage.getItem('busRemindStopItem');
}

// Firstion section's input[name="remind-time"] and it's has checked status
function getSelectedRemindTime() {
    const section = document.querySelector('[data-id="setCountTime"]');
    let selectedRadio = section.querySelector('input[name="remind-time"]:checked');
    let selectedValue;

    if (selectedRadio.value === 'custom') {
        // if the value is custom time, read data-value from '.custom-time-input'
        const customInput = document.querySelector('.custom-time-input');
        selectedValue = customInput.dataset.value;
    } else {
        // else 5、10、15
        selectedValue = selectedRadio.value;
    }
    // console.log('使用者選擇的提醒時間：', selectedValue, '分鐘前');
    return selectedValue;
}

function getRepeatWeekTime() {
    let checkedBoxes = document.querySelectorAll('[data-id="setRepeatWeekTime"] input[type="checkbox"]:checked');
    let selectedWeekDays = Array.from(checkedBoxes).map(checkbox => checkbox.value);

    // Ex: ["Mon", "Wed", "Fri"]
    // console.log(selectedWeekDays);

    return selectedWeekDays;
}

function getBusShowUpTime() {
    // get Bus startTime and  endTime
    let [startInput, endInput] = document.querySelectorAll('[data-id="setBusShowUpTime"] input[type="time"]');
    let startTime = startInput.value;
    let endTime = endInput.value;

    return [startTime, endTime];
}

// function get



