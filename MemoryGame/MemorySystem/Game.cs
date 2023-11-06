using Microsoft.Maui.Graphics;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MemorySystem
{
    public class Game : INotifyPropertyChanged
    {
        public enum GameStatusEnum { NotStarted, Playing, Winner }
        public enum TurnEnum { None, A, B }

        public bool EnableBtnSwitch = false;
        bool pair = false;
        private TurnEnum _currentturn = TurnEnum.None;
        private GameStatusEnum _gamestatus = GameStatusEnum.NotStarted;
        private int _scorea = 0;
        private int _scoreb = 0;

        Random rnd = new();
        public event PropertyChangedEventHandler? PropertyChanged;

        public Game()
        {
            SetCards();
        }

        public List<Card> lstCards { get; set; } = new();
        public GameStatusEnum GameStatus
        {
            get => _gamestatus;
            private set
            {
                _gamestatus = value;
                InvokePropertyChanged();
            }
        }
        public TurnEnum CurrentTurn
        {
            get => _currentturn;
            private set
            {
                _currentturn = value;
                InvokePropertyChanged();
                InvokePropertyChanged("GameStatusDesc");
            }
        }
        public string GameStatusDesc
        {
            get => SetMessageLabel();
        }
        public TurnEnum Winner { get; private set; } = TurnEnum.None;
        public int ScoreA { get => _scorea; private set { _scorea = value; InvokePropertyChanged(); } }
        public int ScoreB { get => _scoreb; private set { _scoreb = value; InvokePropertyChanged(); } }
        public System.Drawing.Color CardFontColor { get; set; } = System.Drawing.Color.CornflowerBlue;
        public System.Drawing.Color OpenCardBackColor { get; set; } = System.Drawing.Color.White;
        public System.Drawing.Color WinnerBackcolor { get; set; } = System.Drawing.Color.DarkBlue;


        public void SetCards()
        {
            lstCards.Clear();
            for (int i = 0; i < 30; i++)
            {
                Card c = new();
                c.Enabled = false;
                c.BackColor = CardFontColor;
                lstCards.Add(c);
            }
        }

        public void StartGame()
        {
            GameStatus = GameStatusEnum.Playing;
            CurrentTurn = TurnEnum.A;
            this.ScoreA = 0;
            this.ScoreB = 0;
            GiveCardLetter();
            lstCards.ForEach
                (
                    c =>
                    {
                        c.Enabled = true;
                        c.BackColor = CardFontColor;
                    }
                );
            InvokePropertyChanged("GameStatusDesc");
            //lstCards.ForEach(c => c.Enabled = true);
        }

        private void GiveCardLetter()
        {
            List<int> indexes = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
            List<string> lstl = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        
            for (int i = 0; i < 15; i++)
            {
                int c = rnd.Next(lstl.Count());
                int a = indexes[rnd.Next(indexes.Count())];
                lstCards[a].CardText = lstl[c];
                indexes.Remove(a);
                int b = indexes[rnd.Next(indexes.Count())];
                lstCards[b].CardText = lstl[c].ToUpper().ToString();
                indexes.Remove(b);
                lstl.RemoveAt(c);
            }

        }

        public void TakeTurn(int cardindex)
        {
            Card card = this.lstCards[cardindex];
            if (ButtonsClicked() < 2)
            {
                card.BackColor = OpenCardBackColor;
            }
            if (ButtonsClicked() == 2)
            {
                EnableBtnSwitch = true;
            }
        }

        private int ButtonsClicked()
        {
            return lstCards.Where(btn => btn.BackColor == OpenCardBackColor).Count();
        }

        public void SwitchTurn()
        {
            pair = false;
            List<Card> lstcheckb = lstCards.Where(b => b.BackColor == OpenCardBackColor).ToList();
            if (lstcheckb.Count > 1)
            {
                if (lstcheckb[0].CardText.ToLower() == lstcheckb[1].CardText.ToLower())
                {
                    pair = true;
                    switch (CurrentTurn)
                    {
                        case TurnEnum.A:
                            ScoreA++;
                            break;
                        case TurnEnum.B:
                            ScoreB++;
                            break;
                    }
                    lstCards.Where(btn => btn.BackColor == OpenCardBackColor).ToList().ForEach(btn => { btn.Enabled = false; btn.CardText = "!"; btn.FontColor = System.Drawing.Color.White; });
                }
         
                //lstcheckb.ForEach(b => b.Enabled = false);
            }
            SetCurrentTurn();
            lstCards.ForEach(b => { b.BackColor = CardFontColor; b.FontColor = CardFontColor; });
            EnableBtnSwitch = false;
        }
        private void DetectWinner()
        {
            if (ScoreA > ScoreB)
            {
                Winner = TurnEnum.A;
                //lbl backcolor = WinnerBackcolor
            }
            else if (ScoreB > ScoreA)
            {
                //lbl backcolor = WinnerBackcolor
                Winner = TurnEnum.B;
            }
        }

        private void SetCurrentTurn()
        {
            CurrentTurn = CurrentTurn == TurnEnum.A ? TurnEnum.B : TurnEnum.A;
        }

        private string SetMessageLabel()
        {
            string message = "Current turn: Player " + CurrentTurn.ToString();
            string msg = pair == false ? message : "You got it! " + message;
            if (lstCards.Where(b => b.Enabled == true).ToList().Count() == 0)
            {
                DetectWinner();
                msg = (this.Winner != TurnEnum.None? "Winner is player " + Winner.ToString() + ". ":"") + "Click Start Game";
                //lstCards.Clear();
                //in FE winnermode() = enable btnStartGame + lblScorewinner.backcolor + switchTurn = disabled;
            }
            pair = false;
            return msg;
        }

        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}