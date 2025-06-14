// Waits for a specific event to occur on a given target and resolves a Promise when the event is triggered.
// Usage: await waitForEvent('click', someElement);
// function waitForEvent(eventName, target = document) {
//   return new Promise(resolve => {
//     target.addEventListener(eventName, resolve, { once: true });
//   });
// }

// function sendDataToRemindPage() {
//     document.addEventListener
// }


// Wait Tow Event Completed and then trigger sendDataToRemindPage
// Promise.all([
//   waitForEvent('DOMContentLoaded'),
//   waitForEvent('StopItemRendered')
// ]).then(sendDataToRemindPage);


