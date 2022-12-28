using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Memory : Form
    {
        // menatzeah
        // thilat mishak hadash - enable et hakaftor
        // lblMessage - torot, nekuda nosfa le..., menazeah, lilhots lehthil mehadash
        enum TurnEnum { A, B }
        TurnEnum currentturn = TurnEnum.A;
        Random rnd = new();
        int AScore = 0;
        int BScore = 0;
        List<string> lstletters = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        List<Button> lstbuttons;

        //tzricha limzo derech lhatim randomaly et haklafim laot

        public Memory()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnSwitch.Click += BtnSwitch_Click;

            lstbuttons = new() { btn1, btn2, btn3, btn4, btn5, btn6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25, button26, button27, button28, button29, button30};
            lstbuttons.ForEach(b => b.Click += B_Click);
        }


        private void EnableButtons()
        {
            foreach(Button b in tblCards.Controls)
            {
                b.Enabled = true;
            }
        }

        private void GiveCardLetter()
        {
            List<Button> lstb = lstbuttons.ToList();
            List<string> lstl = lstletters.ToList();
            for(int i = 0; i < 15; i++)
            {
                int c = rnd.Next(lstl.Count());
                int a = rnd.Next(lstb.Count());
                lstb[a].Text = lstl[c].ToString();
                lstb.RemoveAt(a);
                int b = rnd.Next(lstb.Count());
                lstb[b].Text = lstl[c].ToUpper().ToString();
                lstb.RemoveAt(b);
                lstl.RemoveAt(c);
            }
        }

        private void SetCurrentTurn()
        {
                if(currentturn == TurnEnum.A)
                {
                    currentturn = TurnEnum.B;
                }
                else if(currentturn == TurnEnum.B)
                {
                    currentturn = TurnEnum.A;
                }
        }

        private void LblMessage()
        {
            SetCurrentTurn();
            lblMessage.Text = "Current turn: Player " + currentturn.ToString();
            //if ()
            //{

            //}
        }
        private void BtnSwitch_Click(object? sender, EventArgs e)
        {
            List<Button> lstcheckb = lstbuttons.Where(b => b.BackColor == Color.White).ToList();
            if (lstcheckb[0].Text.ToLower() == lstcheckb[1].Text.ToLower())
            {
                switch (currentturn)
                {
                    case TurnEnum.A:
                        AScore = AScore + 1;
                        break;
                    case TurnEnum.B:
                        BScore = BScore + 1;
                        break;
                }
                lblAScore.Text = AScore.ToString();
                lblBScore.Text = BScore.ToString();
                lstcheckb.ForEach(b => b.Enabled = false);
            }
            SetCurrentTurn();
            lstbuttons.ForEach(b => b.BackColor = b.ForeColor);
            btnSwitch.Enabled = false;
        }

        private void B_Click(object? sender, EventArgs e)
        {
            if(sender is Button)
            {
                if (lstbuttons.Where(btn => btn.BackColor == Color.White).Count() < 2)
                {

                    ((Button)sender).BackColor = Color.White;
                }
            }
            if (lstbuttons.Where(b => b.BackColor == Color.White).Count() == 2)
            {
                btnSwitch.Enabled = true;
            }
            LblMessage();
        }


        private void BtnStart_Click(object? sender, EventArgs e)
        {
            EnableButtons();
            LblMessage();
            btnStart.Enabled = false;
            GiveCardLetter();
        }
    }
}
