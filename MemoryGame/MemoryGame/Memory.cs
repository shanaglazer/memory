﻿using System;
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
        enum TurnEnum { A, B }
        TurnEnum currentturn = TurnEnum.A;
        Random rnd = new();
        List<string> lstletters = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        List<Button> lstbuttons;

        //tzricha limzo derech lhatim randomaly et haklafim laot

        public Memory()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;

            lstbuttons = new() { btn1, btn2, btn3, btn4, btn5, btn6, button7, button8, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20, button21, button22, button23, button24, button25, button26, button27, button28, button29, button30};
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
            for(int i = 0; i < 15; i++)
            {
                int c = rnd.Next(lstletters.Count());
                int a = rnd.Next(lstbuttons.Count());
                lstbuttons[a].Text = lstletters[c].ToString();
                lstbuttons.RemoveAt(a);
                int b = rnd.Next(lstbuttons.Count());
                lstbuttons[b].Text = lstletters[c].ToUpper().ToString();
                lstbuttons.RemoveAt(b);
                lstletters.RemoveAt(c);
            }
        }

        private void LblMessage()
        {
            lblMessage.Text = "Current turn: Player " + currentturn.ToString();
        }

        private void B_Click(object? sender, EventArgs e)
        {
            if(sender is Button)
            {
                if (lstbuttons.Where(btn => btn.BackColor == Color.White).ToList().Count() < 2)
                {

                    ((Button)sender).BackColor = Color.White;
                }
            }
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