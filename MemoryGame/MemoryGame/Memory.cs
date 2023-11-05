﻿using MemorySystem;
using System;
using System.Data;

namespace MemoryGame
{
    public partial class Memory : Form
    {
        //enum TurnEnum { A, B }
        //TurnEnum currentturn = TurnEnum.A;
        //bool newgame = true;
        //Random rnd = new();
        //int AScore = 0;
        //int BScore = 0;
        //bool pair = false;
        //List<string> lstletters = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        Game game = new();
 
        List<Button> lstbuttons;

        public Memory()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnSwitch.Click += BtnSwitch_Click;
            //SetCards();
            lstbuttons = new() { btn1, btn2, btn3, btn4, btn5, btn6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25, button26, button27, button28, button29, button30};
            lstbuttons.ForEach(b =>
            {
                Card card = game.lstCards[lstbuttons.IndexOf(b)];
                b.Click += B_Click;
                b.DataBindings.Add("Text", card, "CardText");
                b.DataBindings.Add("BackColor", card, "BackColor");
                b.DataBindings.Add("Enabled", card, "Enabled");
                b.DataBindings.Add("ForeColor", card, "FontColor");
            });
            lblMessage.DataBindings.Add("Text", game, "GameStatusDesc");
            lblAScore.DataBindings.Add("Text", game, "ScoreA");
            lblBScore.DataBindings.Add("Text", game, "ScoreB");
            // btnSwitch.DataBindings.Add("Enabled", game, "EnableBtnSwitch");
            
        }

        //try calling SetCards from InitializeComponent - didn't work
        //private void SetCards()
        //{
        //    game.SetCards();
        //}


        //private void EnableButtons()
        //{
        //    foreach(Button b in tblCards.Controls)
        //    {
        //        b.Enabled = true;
        //    }
        //}

        //private void GiveCardLetter()
        //{
        //    List<Button> lstb = lstbuttons.ToList();
        //    List<string> lstl = lstletters.ToList();
        //    for(int i = 0; i < 15; i++)
        //    {
        //        int c = rnd.Next(lstl.Count());
        //        int a = rnd.Next(lstb.Count());
        //        lstb[a].Text = lstl[c].ToString();
        //        lstb.RemoveAt(a);
        //        int b = rnd.Next(lstb.Count());
        //        lstb[b].Text = lstl[c].ToUpper().ToString();
        //        lstb.RemoveAt(b);
        //        lstl.RemoveAt(c);
        //    }
        //}

        //private void SetCurrentTurn()
        //{
        //    currentturn = currentturn == TurnEnum.A ? TurnEnum.B : TurnEnum.A;
        //}

        //private string detectwinner()
        //{
        //    string winner = "";
        //    if (AScore > BScore)
        //    {
        //        winner = "A";
        //        lblAScore.BackColor = Color.BlueViolet;
        //    }
        //    else if (BScore > AScore)
        //    {
        //        lblBScore.BackColor = Color.BlueViolet;
        //        winner = "B";
        //    }
        //    return winner;
        //}

        //private void winnermode()
        //{
        //    btnStart.Enabled = true;
        //    btnSwitch.Enabled = false;
        //    lstbuttons.ForEach(b => b.BackColor = b.ForeColor);
        //}

        //private void LblMessage()
        //{
        //    string message = "Current turn: Player " + currentturn.ToString();
        //    lblMessage.Text = pair == false? message: "You got it! " + message;
        //    //lesader po sheim thilat mishak az bli u got it
        //    if(newgame == true)
        //    {
        //        lblMessage.Text = message;
        //    }
        //    else if (lstbuttons.Where(b => b.Enabled == true).ToList().Count() == 0) 
        //    {
        //        lblMessage.Text = "Winner is " + detectwinner() + " Click Start Game";
        //        winnermode();
        //    }
        //}

        //private void switchturn()
        //{
        //    pair = false;
        //    List<Button> lstcheckb = lstbuttons.Where(b => b.BackColor == Color.White).ToList();
        //    if (lstcheckb[0].Text.ToLower() == lstcheckb[1].Text.ToLower())
        //    {
        //        pair = true;
        //        switch (currentturn)
        //        {
        //            case TurnEnum.A:
        //                AScore = AScore + 1;
        //                break;
        //            case TurnEnum.B:
        //                BScore = BScore + 1;
        //                break;
        //        }
        //        lblAScore.Text = AScore.ToString();
        //        lblBScore.Text = BScore.ToString();
        //        lstcheckb.ForEach(b => b.Enabled = false);
        //    }
        //    lstbuttons.ForEach(b => b.BackColor = b.ForeColor);
        //    btnSwitch.Enabled = false;
        //}
        //private int buttonsclicked()
        //{
        //    int i = lstbuttons.Where(btn => btn.BackColor == Color.White).Count();
        //    return i;
        //}

        //private void startgame()
        //{
        //    btnStart.Enabled = false;
        //    GiveCardLetter();
        //    List<Label> lstlbl = new() { lblAScore, lblBScore };
        //    lstlbl.ForEach(l => { l.Text = "0"; l.BackColor = btn1.BackColor; });
        //    currentturn = TurnEnum.A;
        //}


        //private void B_Click(object? sender, EventArgs e)
        //{
        //    //if (sender is Button)
        //    //{
        //    //    if (buttonsclicked() < 2)
        //    //    {
        //    //        ((Button)sender).BackColor = Color.White;
        //    //    }
        //    //}
        //    //if (buttonsclicked() == 2)
        //    //{
        //    //    btnSwitch.Enabled = true;
        //    //}
        //    //newgame = false;
        //}

        private void StartGame() 
        {
            game.StartGame();
            //btnStart.Enabled = false;
        }

        private void DoTurn(Button btn)
        {
            int index = lstbuttons.IndexOf(btn);
            game.TakeTurn(index);
            btnSwitch.Enabled = game.EnableBtnSwitch;
        }

        private void B_Click(object? sender, EventArgs e)
        {
            if (sender is Button)
            {
                DoTurn((Button)sender);
            }
                
        }

        private void BtnSwitch_Click(object? sender, EventArgs e)
        {
            btnSwitch.Enabled = false;
            game.SwitchTurn();
            if (game.Winner != Game.TurnEnum.None)
            {
                btnStart.Enabled = true;
            }
            //switchturn();
            //SetCurrentTurn();
            //LblMessage();

        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

    }
}
