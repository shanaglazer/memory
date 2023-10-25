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
        List<string> lstletters = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        Random rnd = new();
        public event PropertyChangedEventHandler? PropertyChanged;

        public Game()
        {
            for (int i = 0; i < 30; i++)
            {
                this.Cards.Add(new Card());
            }
            
        }
        public List<Card> Cards { get; private set; } = new();
        public GameStatusEnum GameStatus 
        { 
            get=>_gamestatus;
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
        public TurnEnum Winner { get; private set; }
        public int ScoreA { get; private set; } = 0;
        public int ScoreB { get; private set; } = 0;
        public System.Drawing.Color CardFontColor { get; set; } = System.Drawing.Color.CornflowerBlue;
        public System.Drawing.Color OpenCardBackColor { get; set; } = System.Drawing.Color.White;
        public System.Drawing.Color WinnerBackcolor { get; set; } = System.Drawing.Color.DarkBlue;

        public void StartGame()
        {
            GameStatus = GameStatusEnum.Playing;
            CurrentTurn = TurnEnum.A;
            //in FE btnStartGame = disabled
            GiveCardLetter();
            Cards.ForEach(c => c.Enabled = true);
        }

        private void GiveCardLetter()
        {
            List<Card> lstb = Cards;
            List<string> lstl = lstletters;
            for (int i = 0; i < 15; i++)
            {
                int c = rnd.Next(lstl.Count());
                int a = rnd.Next(lstb.Count());
                lstb[a].CardText = lstl[c].ToString();
                lstb.RemoveAt(a);
                int b = rnd.Next(lstb.Count());
                lstb[b].CardText = lstl[c].ToUpper().ToString();
                lstb.RemoveAt(b);
                lstl.RemoveAt(c);
            }
        }

        public void TakeTurn(Card card)
        {
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
            return Cards.Where(btn => btn.BackColor == OpenCardBackColor).Count();
        }

        public void SwitchTurn()
        {
            pair = false;
            List<Card> lstcheckb = Cards.Where(b => b.BackColor == OpenCardBackColor).ToList();
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
                Cards.Where(btn => btn.BackColor == OpenCardBackColor).ToList().ForEach(btn => btn.Enabled = false);
                //lstcheckb.ForEach(b => b.Enabled = false);
            }
            SetCurrentTurn();//lidog shepo gam mishtane lblmessage! invoke message
            Cards.ForEach(b => b.BackColor = CardFontColor);
            EnableBtnSwitch = false;
            //in FE - btnswitch = disabled
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
            if (Cards.Where(b => b.Enabled == true).ToList().Count() == 0)
            {
                DetectWinner();
                msg = "Winner is player " + Winner.ToString() + ". Click Start Game";
                Cards.Clear();
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