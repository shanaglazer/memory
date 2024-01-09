enum Turn{ "A" = "A", 'B' = 'B' }
let currentTurn = Turn.A;
let aScoreVal: Number = 0;
let bScoreVal: Number = 0;
let pair = false;
let lstLetters = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
let lstbuttons: Element[] = [];
let btnStart;
let btnSwitch: HTMLButtonElement;
let aScore: HTMLElement;
let bScore: HTMLElement;
let purplecolor = 'rgb(153, 50, 204)';
let whitecolor = 'rgb(255, 255, 255)';


$(document).ready(function () {
    btnStart = $("#btnStart");
    btnSwitch = $("#btnSwitch").prop("disabled");
    aScore = $("#aScore");
    bScore = $("#bScore");
    btnStart.click(startGame);
    btnSwitch.click(switchTurn);
    lstbuttons = [...document.querySelectorAll("#card")];
    $(lstbuttons).prop('disabled', true);
    $(lstbuttons).click(cardClick);
    setupGame();
});

function enableButtons() {
    $(lstbuttons).prop('disabled', false);
}

function giveCardLetter() {
    let lstb = lstbuttons.slice();
    let lstl = lstLetters.slice();
    for (let i = 0; i < 15; i++) {
        let c = Math.floor(Math.random() * (lstl.length));
        let a = Math.floor(Math.random() * (lstb.length));
        lstb[a].innerHTML = lstl[c].toString();
        lstb.splice(a, 1);
        let b = Math.floor(Math.random() * (lstb.length));
        lstb[b].innerHTML = lstl[c].toString().toUpperCase();
        lstb.splice(b, 1);
        lstl.splice(c, 1);
    }
}

function setCurrentTurn() {
    currentTurn = currentTurn == Turn.A ? Turn.B : Turn.A;
}

function detectWinner() {
    let winner = "";
    if (aScoreVal > bScoreVal) {
        winner = "A";
        aScore.('background-color', 'red');
    }
    else if (bScoreVal > aScoreVal) {
        bScore.css('background-color', 'red');
        winner = "B";
    }
    return winner;
}

function winnerMode() {
    btnSwitch.prop('disabled', true);
    $(lstbuttons).css('background-color', purplecolor);
}

function messageText() {
    let message = "Current turn: Player " + currentTurn;
    let finalmsg = "";
    if (lstbuttons.filter(b => b. disabled == false).length == 0) {
        finalmsg = "Winner is " + detectWinner() + ". Click Start Game";
        winnerMode();
    }
    else if (pair) {
        finalmsg = "You got it! " + message;
    }
    else if (pair == false) {
        finalmsg = message;
    }
    $("#msg").text(finalmsg);
}

function doSwitchTurn() {
    pair = false;
    let lstcheckb = lstbuttons.filter(b => b.style.backgroundColor == whitecolor);
    let a = ""; a = lstcheckb[0].innerHTML.toLowerCase();
    let b = ""; b = lstcheckb[1].innerHTML.toLowerCase();
    if (a == b) {
        pair = true;
        switch (currentTurn) {
            case "A":
                aScoreVal = aScoreVal + 1;
                break;
            case "B":
                bScoreVal = bScoreVal + 1;
                break;
        }
        setScore();
        $(lstcheckb).prop("disabled", true);
        $(lstcheckb).css('color', 'rgb(0, 0, 0)');
    }
    $(lstbuttons).css('background-color', purplecolor);
    btnSwitch.prop('disabled', true);
}

function setScore() {
    aScore.text(aScoreVal);
    bScore.text(bScoreVal);
}

function buttonsClicked() {
    const array = lstbuttons.filter(btn => btn.style.backgroundColor == whitecolor);
    let i = array.length;
    return i;
}

function setupGame() {
    pair = false;
    aScoreVal = 0;
    bScoreVal = 0;
    setScore();
    currentTurn = Turn.A;
    $(lstbuttons).css({ 'background-color': purplecolor, 'color': purplecolor });
    giveCardLetter();
    let lstlbl = $('[id*="Score"]');
    lstlbl.text("0");
    lstlbl.css('background-color', 'white');
}

function startGame() {
    enableButtons();
    setupGame();
    messageText();
}

function switchTurn() {
    doSwitchTurn();
    setCurrentTurn();
    messageText();
}

function cardClick(e: Event) {
    const btn = e.target;
    if (buttonsClicked() < 2) {
        btn.style.backgroundColor = whitecolor;
    }
    if (buttonsClicked() == 2) {
        $("#btnSwitch").prop('disabled', false);
    }
}
