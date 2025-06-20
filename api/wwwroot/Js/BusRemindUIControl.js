// =========================== ===========================
document.addEventListener('DOMContentLoaded', () => setHeaderName());

function setHeaderName() {
    let header = document.querySelector('.Header');
    let stopName = sessionStorage.getItem('busRemindstopName');
    header.textContent = stopName;
}

// =========================== KeyBoard Behavior Event =========================== //
let currentValue = '';
let isKeyboardShown = false;

const customLabel = document.querySelector('.custom-time-label');
const customInput = document.querySelector('.custom-time-input');
const customRadio = customLabel.querySelector('input[type="radio"]');
const cursor = document.querySelector('.cursor');
const keyboard = document.querySelector('.numeric-keyboard');
const overlay = document.querySelector('.keyboard-overlay');
const allLabels = document.querySelectorAll('.Checkbox-Group label');
const allRadios = document.querySelectorAll('input[name="remind-time"]');

// 處理 radio 選擇
allRadios.forEach(radio => {
    radio.addEventListener('change', function() {
        // 移除所有選中狀態
        allLabels.forEach(label => label.classList.remove('selected'));

        // 添加當前選中狀態
        this.closest('label').classList.add('selected');

        // 如果不是自訂時間，隱藏鍵盤
        if (this.value !== 'custom') {
            hideKeyboard();
        }
    });
});

// 點擊自訂時間輸入框
customInput.addEventListener('click', function(e) {
    e.preventDefault();
    e.stopPropagation();

    // 選中自訂時間的 radio
    customRadio.checked = true;
    customRadio.dispatchEvent(new Event('change'));

    // 顯示鍵盤和光標
    showKeyboard();
});

// 點擊自訂時間標籤
customLabel.addEventListener('click', function(e) {
    if (e.target === customInput) return;

    // 選中 radio
    customRadio.checked = true;
    customRadio.dispatchEvent(new Event('change'));
});

// 顯示鍵盤
function showKeyboard() {
    isKeyboardShown = true;
    keyboard.classList.add('show');
    overlay.classList.add('show');
    cursor.style.display = 'inline-block';

    // 防止頁面滾動
    document.body.style.overflow = 'hidden';
}

// 隱藏鍵盤
function hideKeyboard() {
    isKeyboardShown = false;
    keyboard.classList.remove('show');
    overlay.classList.remove('show');
    cursor.style.display = 'none';

    // 恢復頁面滾動
    document.body.style.overflow = '';
}

// 鍵盤按鍵事件
document.querySelectorAll('.keyboard-key').forEach(key => {
    key.addEventListener('click', function() {
        if (this.classList.contains('disabled')) return;

        if (this.dataset.number) {
            // 數字按鍵
            let newValue = currentValue + this.dataset.number;
            // 限制最多120分鐘
            if (parseInt(newValue) <= 120 && newValue.length <= 3) {
                currentValue = newValue;
            }
        } else if (this.dataset.action === 'delete') {
            // 刪除按鍵
            currentValue = currentValue.slice(0, -1);
        }

        updateDisplay();
    });
});

// 完成按鈕
document.querySelector('.keyboard-done').addEventListener('click', function() {
    hideKeyboard();
});

// 點擊遮罩隱藏鍵盤
overlay.addEventListener('click', function() {
    hideKeyboard();
});

// 更新顯示
function updateDisplay() {
    if (currentValue === '') {
        customInput.textContent = '請輸入';
        customInput.dataset.value = '';
    } else {
        let numValue = parseInt(currentValue);
        if (numValue > 120) {
            currentValue = '120';
            numValue = 120;
        }
        customInput.textContent = currentValue;
        customInput.dataset.value = currentValue;
    }
}

// 初始化第一個選項為選中狀態
document.querySelector('input[name="remind-time"]:checked').closest('label').classList.add('selected');

// 阻止點選輸入框時的預設行為
customInput.addEventListener('mousedown', function(e) {
    e.preventDefault();
});

customInput.addEventListener('focus', function(e) {
    e.preventDefault();
    this.blur();
});

// =========================== Control Week List Color =========================== //
document.querySelectorAll('.Week-Select label').forEach(label => {
    const checkbox = label.querySelector('input[type="checkbox"]');
    const span = label.querySelector('span');

    label.addEventListener('click', () => {
        checkbox.checked = !checkbox.checked;
        if (checkbox.checked) {
            label.style.background = '#14274E';
            label.style.color = 'white';
            label.style.borderColor = '#14274E';
        } else {
            label.style.background = 'white';
            label.style.color = '#333';
            label.style.borderColor = '#e9ecef';
        }
    });
});



