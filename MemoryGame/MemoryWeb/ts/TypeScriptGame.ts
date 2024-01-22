enum Turn { "A" = "A", 'B' = 'B' }
let currentTurn = Turn.A;
let aScoreVal: number = 0;
let bScoreVal: number = 0;
let pair = false;
let lstLetters = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
let lstbuttons: HTMLButtonElement[] = [];
let btnStart;
let btnSwitch: HTMLButtonElement;
let aScore: HTMLElement;
let bScore: HTMLInputElement;
let purplecolor = 'rgb(153, 50, 204)';
let whitecolor = 'rgb(255, 255, 255)';


$(document).ready(function () {
    const cards = document.querySelectorAll('.card');
    cards.forEach(c => lstbuttons.push(c as unknown as HTMLButtonElement));
    
    //cards.forEach(btn => btn as unknown as HTMLButtonElement);
    //let ll = cards.length;
    
    btnStart = document.querySelector("#btnStart");
    btnSwitch = document.querySelector("#btnSwitch");
    aScore = document.querySelector("#aScore");
    bScore = document.querySelector("#bScore");
    btnStart.addEventListener('click', startGame);
    btnSwitch.addEventListener('click', switchTurn);
    //$("#table").find("#card").each(function () {
    //    lstbuttons.push(this as HTMLButtonElement);
    //})
    //lstbuttons.push(document.querySelectorAll('.card') as unknown as HTMLButtonElement);
    lstbuttons.forEach(btn => { btn.disabled = true; addEventListener('click', cardClick) });
    
    //lstbuttons.forEach(btn => addEventListener('click', cardClick));
    setupGame();
});

function enableButtons() {
    $(lstbuttons).prop('disabled', false);
}

function giveCardLetter() {
    let lstb = lstbuttons.slice(0);
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
        aScore.style.backgroundColor = "red";
    }
    else if (bScoreVal > aScoreVal) {
        bScore.style.backgroundColor = "red";
        winner = "B";
    }
    return winner;
}

function winnerMode() {
    btnSwitch.disabled = true;
    $(lstbuttons).css('background-color', purplecolor);
}

function messageText() {
    let message = "Current turn: Player " + currentTurn;
    let finalmsg = "";
    if (lstbuttons.filter(b => b.disabled == false).length == 0) {
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
                aScoreVal = aScoreVal++;
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
    btnSwitch.disabled = true;
}

function setScore() {
    aScore.innerHTML = aScoreVal.toString();
    bScore.innerHTML = bScoreVal.toString();
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
    const btn = e.target as HTMLButtonElement;
    if (buttonsClicked() < 2) {
        btn.style.backgroundColor = whitecolor;
    }
    if (buttonsClicked() == 2) {
        $("#btnSwitch").prop('disabled', false);
    }
}
