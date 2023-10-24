using System.ComponentModel;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace MemorySystem
{
    public class Game : INotifyPropertyChanged
    {
        public enum GameStatusEnum { NotStarted, Playing, Winner }
        public enum TurnEnum { None, A, B }

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
        { get => SetMessageLabel();
            //get => this.CurrentTurn == 
            //    TurnEnum.None ? "Click Start Game" : 
            //    "Current turn: Player " + this.CurrentTurn; 
        }
        public TurnEnum Winner { get; private set; }
        public int ScoreA { get; private set; } = 0;
        public int ScoreB { get; private set; } = 0;

        public void StartGame()
        {
            GameStatus = GameStatusEnum.Playing;
            CurrentTurn = TurnEnum.A;
            //in FE btnStartGame = disabled
            GiveCardLetter();
            Cards.ForEach(c => c.Enabled = true);
        }

        private void Score()
        {
            if(CurrentTurn == TurnEnum.A)
            {
                ScoreA++;
            }
            if(CurrentTurn == TurnEnum.B)
            {
                ScoreB++;
            }
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

        public void ShowCard()
        {
            //im ze klaf sheni veset - likro le score + enabled = false;
        }

        private void DetectWinner()
        {

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
                msg = "Winner is " + Winner.ToString() + ". Click Start Game";
                //in FE winnermode() = enable btnStartGame;
            }
            pair = false;
            return msg;
        }

        public void SwitchTurn()
        {
            //likro kesheyesh 2 clafim ptuhim;
            pair = false;
            List<Card> lstcheckb = Cards.Where(b => b.BackColor == Color.White).ToList();
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
                //bind score in FE to score!
                lstcheckb.ForEach(b => b.Enabled = false);
            }
            SetCurrentTurn();
            Cards.ForEach(b => b.BackColor = b.ForeColor);
        }

        private void InvokePropertyChanged([CallerMemberName] string propertyname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}