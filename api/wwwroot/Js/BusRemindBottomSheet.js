document.addEventListener('StopItemRendered', function(){
    // Add a click event listener to the "Direction-Zero" and "Direction-One" container.
    // When any child element is clicked, openDirectionZeroBottomSheet(e) will be triggered.
    document.getElementById('Direction-Zero').addEventListener('click', (e) => openDirectionZeroBottomSheet(e));
    document.getElementById('Direction-One').addEventListener('click', (e) => openDirectionOneBottomSheet(e));
    // Close BottomSheet
    document.getElementById('CancelBtn').addEventListener('click', () => closeBottomSheet());
    document.getElementById('Overlay').addEventListener('click', () => closeBottomSheet());
    // Open RemindPage
    document.getElementById('RemindBtn').addEventListener('click', () => openRemindPage());
});

// Set StopName and StopId
function openDirectionZeroBottomSheet(e) {
    // Find the closest parent element with class 'StopItem'
    const stopItem = e.target.closest('.StopItem');
    if(stopItem)
    {

        // Get the element with class 'StopName' inside the stopItem
        let stopNameClass = stopItem.querySelector('.StopName');
        let stopName = stopNameClass.textContent;
        // Find the element with id 'SelectedStopTitle'
        let selectStopTitle = document.querySelector('#SelectedStopTitle');
        // Set the text content of selectStopTitle to the stop name
        selectStopTitle.textContent = stopName;
        // Show the bottom sheet by adding the 'Show' class
        document.getElementById('BottomSheet').classList.add('Show');
        document.getElementById('Overlay').classList.add('Show');
        // Forbidden screen move
        document.body.classList.add('No-Scroll');
        // Set stop name at sessionStorage
        setStopNameToSessionStorage(stopName);
        setStopIdToSessionStorage(stopItem);
    }
}


function openDirectionOneBottomSheet(e) {
    // Find the closest parent element with class 'StopItem'
    const stopItem = e.target.closest('.StopItem');
    if(stopItem)
    {

        // Get the element with class 'StopName' inside the stopItem
        let stopNameClass = stopItem.querySelector('.StopName');
        let stopName = stopNameClass.textContent;
        // Find the element with id 'SelectedStopTitle'
        let selectStopTitle = document.querySelector('#SelectedStopTitle');
        // Set the text content of selectStopTitle to the stop name
        selectStopTitle.textContent = stopName;
        // Show the bottom sheet by adding the 'Show' class
        document.getElementById('BottomSheet').classList.add('Show');
        document.getElementById('Overlay').classList.add('Show');
        document.body.classList.add('No-Scroll');
        // Set stop name at sessionStorage
        setStopNameToSessionStorage(stopName);
        setStopIdToSessionStorage(stopItem);
    }
}


//Set StopId to session storage
function setStopIdToSessionStorage(stopItem) {
    //
    let stopId = stopItem.getAttribute('data-stopid');
    sessionStorage.setItem('busRemindStopItem', stopId);
}

//Set StopName to session storage
function setStopNameToSessionStorage(stopName) {
    sessionStorage.setItem('busRemindstopName', stopName);
}



function closeBottomSheet() {
    document.getElementById('BottomSheet').classList.remove('Show');
    document.getElementById('Overlay').classList.remove('Show');
    document.body.classList.remove('No-Scroll');
}

function openRemindPage() {
    console.log('that be active');
    window.location.href = '/BusRemind.html';
}


