// document.addEventListener('DOMContentLoaded', () =>
//     document.querySelector('[data-action="submit"]').addEventListener('click', async () => await sendRemindData())
// )

document.addEventListener('DOMContentLoaded', async () => {
    try {
        await liff.init({ liffId: '2007424668-B8rkZdQN' });

        if (!liff.isLoggedIn()) {
            liff.login({ redirectUri: window.location.href });
            return;
        }

        console.log("LIFF 初始化成功");

        const idToken = liff.getIDToken();
        console.log("取得 idToken:", idToken);

        //驗證 Token 是否有效
        const verifyResponse = await fetch('/api/lineuserverify', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                idToken: idToken,
                clientId: '2007424668'
            })
        });

        if (verifyResponse.status === 400 || verifyResponse.status === 401) {
            console.warn("Token 無效或過期，重新登入中...");
            liff.logout();
            liff.login({ redirectUri: window.location.href });
            return;
        }

        console.log("Token 驗證成功");

        //驗證完才綁定點擊事件
        document.querySelector('[data-action="submit"]').addEventListener('click', async () => {
            await sendRemindData();
        });

    } catch (err) {
        console.error("LIFF 初始化或驗證錯誤:", err);
        alert("LIFF 初始化失敗，請重新打開頁面");
    }
});

async function sendRemindData() {

    let data = {
        userIDToken:getUserIDToken(),
        userClientId:getAppClientId(),
        userRouteName:getRouteName(),
        userSelectedRemindTime:getMinuteTimeRemind(),
        userStopName:getStopName(),
        userRepeatWeekTime:getRepeatWeekTime(),
        userBusShowUpTime:getBusShowUpTime()
    }

    try {
        const response = await fetch('/api/Remind', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            const result = await response.json();
            console.log('提醒設定成功', result);
            alert('提醒已成功加入！');
            // 可導向其他頁面或重整
        } else {
            const errorText = await response.text();
            console.error('提醒設定失敗', errorText);
            alert('提醒設定失敗，請稍後再試');
        }
    } catch (error) {
        console.error('網路或伺服器錯誤', error);
        alert('發生錯誤，請檢查網路或稍後再試');
    }
}

function getUserIDToken () {
    // let userIDToken = localStorage.getItem('LIFF_STORE:2007424668-B8rkZdQN:IDToken');
    // return userIDToken;
    return liff.getIDToken();
}

function getAppClientId () {
    return '2007424668';
}

function getRouteName () {
    let routeName = localStorage.getItem('busRouteInputValue');
    return routeName;
}

function getMinuteTimeRemind () {
    let selectedInput = document.querySelector('[data-id="setCountTime"] input[name="remind-time"]:checked');
    let remindMinutes = selectedInput?.value;

    if (remindMinutes === 'custom') {
        const customValue = document.querySelector('.custom-time-input')?.dataset.value;
        if (customValue) {
            remindMinutes = customValue;
        } else {
            remindMinutes = '';
        }
    }

    return remindMinutes;
}

function getStopName () {
    let stopName = sessionStorage.getItem('busRemindstopName');
    return stopName;
}

function getRepeatWeekTime () {
    const checkedWeekdays = Array.from(
        document.querySelectorAll('[data-id="setRepeatWeekTime"] input[type="checkbox"]:checked')
        ).map(checkbox => checkbox.value);

    return checkedWeekdays;
}

function getBusShowUpTime () {
    const timeInputs = document.querySelectorAll('[data-id="setBusShowUpTime"] input[type="time"]');
    const busShowUpTime = {
        start: timeInputs[0]?.value || '',
        end: timeInputs[1]?.value || ''
    };

    return busShowUpTime;
}
