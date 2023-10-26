using MemorySystem;

namespace MemoryTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStartGame()
        {
            Game game = new();
            game.StartGame();
            string msg = $"game status = {game.GameStatus.ToString()} current turn = {game.CurrentTurn.ToString()} num cards = {game.lstCards.Count}";
            Assert.IsTrue(game.GameStatus == Game.GameStatusEnum.Playing && game.CurrentTurn == Game.TurnEnum.A && game.lstCards.Count == 30, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestTakeTurn()
        {
            Game game = new();
            game.StartGame();
            game.TakeTurn(0);
            string msg = $"text for card 0 = {game.lstCards[0].CardText.ToString()}. current turn = {game.CurrentTurn.ToString()}";
            Assert.IsTrue(game.lstCards[0].CardText != "" && game.CurrentTurn == Game.TurnEnum.A, msg);
            TestContext.WriteLine(msg);
        }

        [Test]
        public void TestSwitchTurn()
        {
            Game game = new();
            game.StartGame();
            game.TakeTurn(0);
            game.TakeTurn(5);
            game.SwitchTurn();
            string msg = $"num open card = {game.lstCards.Count(c=> c.BackColor == game.OpenCardBackColor)}. current turn = {game.CurrentTurn.ToString()}";
            Assert.IsTrue(game.lstCards.Count(c => c.BackColor == game.OpenCardBackColor) == 0 && game.CurrentTurn == Game.TurnEnum.B, msg);
            TestContext.WriteLine(msg);
        }
        
    }
}