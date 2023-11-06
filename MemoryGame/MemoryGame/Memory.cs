using MemorySystem;
using System;
using System.Data;

namespace MemoryGame
{
    public partial class Memory : Form
    {
        Game game = new();
 
        List<Button> lstbuttons;

        public Memory()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnSwitch.Click += BtnSwitch_Click;
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
            //btnSwitch.DataBindings.Add("Enabled", game, "EnableBtnSwitch");
            
        }

        private void StartGame() 
        {
            game.StartGame();
            btnStart.Enabled = false;
        }

        private void DoTurn(Button btn)
        {
            int index = lstbuttons.IndexOf(btn);
            game.TakeTurn(index);
            btnSwitch.Enabled = game.EnableBtnSwitch;
        }

        private void SwitchTurn()
        {
            btnSwitch.Enabled = false;
            game.SwitchTurn();
            if (game.Winner != Game.TurnEnum.None)
            {
                btnStart.Enabled = true;
            }
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
            SwitchTurn();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

    }
}
