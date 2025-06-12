document.addEventListener('StopItemRendered', function(){
    // Add a click event listener to the "Direction-Zero" and "Direction-One" container.
    // When any child element is clicked, openDirectionZeroBottomSheet(e) will be triggered.
    document.getElementById('Direction-Zero').addEventListener('click', (e) => openDirectionZeroBottomSheet(e));
    document.getElementById('Direction-One').addEventListener('click', (e) => openDirectionOneBottomSheet(e));
    // Close BottomSheet
    document.getElementById('CancelBtn').addEventListener('click', () => closeBottomSheet());
});

function openDirectionZeroBottomSheet(e) {
    // Find the closest parent element with class 'StopItem'
    const stopItem = e.target.closest('.StopItem');
    if(stopItem)
    {
        // Get the element with class 'StopName' inside the stopItem
        let stopName = stopItem.querySelector('.StopName');
        // Find the element with id 'SelectedStopTitle'
        let selectStopTitle = document.querySelector('#SelectedStopTitle');
        // Set the text content of selectStopTitle to the stop name
        selectStopTitle.textContent = stopName.textContent;
        // Show the bottom sheet by adding the 'Show' class
        document.getElementById('BottomSheet').classList.add('Show');
        document.getElementById('Overlay').classList.add('Show');
        // Forbidden screen move
        document.body.classList.add('No-Scroll');

    }
}



function openDirectionOneBottomSheet(e) {
    // Find the closest parent element with class 'StopItem'
    const stopItem = e.target.closest('.StopItem');
    if(stopItem)
    {
        // Get the element with class 'StopName' inside the stopItem
        let stopName = stopItem.querySelector('.StopName');
        // Find the element with id 'SelectedStopTitle'
        let selectStopTitle = document.querySelector('#SelectedStopTitle');
        // Set the text content of selectStopTitle to the stop name
        selectStopTitle.textContent = stopName.textContent;
        // Show the bottom sheet by adding the 'Show' class
        document.getElementById('BottomSheet').classList.add('Show');
        document.getElementById('Overlay').classList.add('Show');
        document.body.classList.add('No-Scroll');
    }
}

function closeBottomSheet() {
    document.getElementById('BottomSheet').classList.remove('Show');
    document.getElementById('Overlay').classList.remove('Show');
    document.body.classList.remove('No-Scroll');
}

