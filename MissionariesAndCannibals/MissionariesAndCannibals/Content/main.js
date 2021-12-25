
function start(element) {

    element.style.display = 'none';

    var DELAY = 3000;

    var url = "/api/getSolution";

    var xhr = new XMLHttpRequest();

    xhr.open('get', url);

    xhr.onload = function () {
        var response = JSON.parse(xhr.response);

        // console.log(response);

        var i = 0;

        var iteration = setInterval(function () {
            // make current state on each side
            makeCurrent(response[i]);

            // Load items onto the boat
            load(response[i], response[i + 1]);

            // Move the boat to the other bank
            moveBoat(response[i].boat, i);

            if (i == 11) {
                clearInterval(iteration);
                setTimeout(() => { element.style.display = 'block'; }, 2000);
            }
            else {
                i++;
            }
        }, DELAY);
    }

    xhr.send();
}

function makeCurrent(state) {
    console.log("make current");
    // left bank
    var leftMissionaries = document.getElementById('dvMissionariesLeft');
    var leftCannibals = document.getElementById('dvCannibalsLeft');

    // right bank
    var rightMissionaries = document.getElementById('dvMissionariesRight');
    var rightCannibals = document.getElementById('dvCannibalsRight');

    // boat
    var boatItems = document.getElementById('dvBoatItems');

    leftMissionaries.innerHTML = leftCannibals.innerHTML = '';
    rightMissionaries.innerHTML = rightCannibals.innerHTML = '';
    boatItems.innerHTML = '';

    // left bank
    addMissionary('dvMissionariesLeft', state.missionaryLeft);
    addCannibal('dvCannibalsLeft', state.cannibalLeft);

    // right bank
    addMissionary('dvMissionariesRight', state.missionaryRight);
    addCannibal('dvCannibalsRight', state.cannibalRight);
}

function load(currentState, nextState) {
    if (nextState != null) {
        var boatDiv = document.getElementById('dvBoatItems');

        boatDiv.innerHTML = '';

        var mCount = 0;
        var cCount = 0;

        if (currentState.boat == 'left') {
            // check right
            mCount = nextState.missionaryRight - currentState.missionaryRight;
            cCount = nextState.cannibalRight - currentState.cannibalRight;

            // adjust the quantity being shifted
            var mLeft = currentState.missionaryLeft - mCount;
            var cLeft = currentState.cannibalLeft - cCount;

            addMissionary('dvMissionariesLeft', mLeft);
            addCannibal('dvCannibalsLeft', cLeft);
        }
        else {
            // check left
            mCount = nextState.missionaryLeft - currentState.missionaryLeft;
            cCount = nextState.cannibalLeft - currentState.cannibalLeft;

            // adjust the quantity being shifted
            var mLeft = currentState.missionaryRight - mCount;
            var cLeft = currentState.cannibalRight - cCount;

            addMissionary('dvMissionariesRight', mLeft);
            addCannibal('dvCannibalsRight', cLeft);
        }

        addMissionary('dvBoatItems', mCount);
        addCannibal('dvBoatItems', cCount);
    }
}

function moveBoat(boatPosition, iterationCount) {
    if (iterationCount != 11) {
        var boatDiv = document.getElementById('dvBoat');

        if (boatPosition == 'left') {
            // move boat right
            var padLeft = 0;
            var moveLeft = setInterval(function () {
                padLeft++;
                boatDiv.style.paddingLeft = padLeft + "px";
                if (padLeft == 300) {
                    clearInterval(moveLeft);
                }
            }, 3);
        }
        else {
            // move boat left
            var padLeft = 300;
            var moveRight = setInterval(function () {
                padLeft--;
                boatDiv.style.paddingLeft = padLeft + "px";
                if (padLeft == 0) {
                    clearInterval(moveRight);
                }
            }, 3);
        }
    }
}

function addMissionary(divId, count) {
    var div = document.getElementById(divId);

    if (divId != 'dvBoatItems') {
        div.innerHTML = '';
    }

    for (var i = 0; i < count; i++) {
        div.innerHTML += '<img class="imgMissionary" src="App_Images/missionary.png" alt="missionary" />';
    }
}

function addCannibal(divId, count) {
    var div = document.getElementById(divId);

    if (divId != 'dvBoatItems') {
        div.innerHTML = '';
    }

    for (var i = 0; i < count; i++) {
        div.innerHTML += '<img class="imgCannibal" src="App_Images/cannibal.png" alt="cannibal" />';
    }
}




/**
 *
 * Sample response:
 * boat: "left"
 * cannibalLeft: 3
 * cannibalRight: 0
 * missionaryLeft: 3
 * missionaryRight: 0
 * parent: null
 *
 * */