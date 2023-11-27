using MemorySystem;


namespace MemoryMaui;

public partial class Memory : ContentPage
{
    Game activegame;
    List<Game> lstgame = new() { new Game(), new Game(), new Game() };

    List<Button> lstbuttons;

    public Memory()
	{
		InitializeComponent();
        Game1Rb.BindingContext = lstgame[0];
        Game2Rb.BindingContext = lstgame[1];
        Game3Rb.BindingContext = lstgame[2];
        activegame = lstgame[0];
        this.BindingContext = activegame;
        lstbuttons = new() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20, btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28, btn29, btn30 };
    }

    private void StartGame()
    {
        activegame.StartGame();
        StartGameBtn.IsEnabled = false;
    }

    private void DoTurn(Button btn)
    {
        int index = lstbuttons.IndexOf(btn);
        activegame.TakeTurn(index);
        SwitchTurnBtn.IsEnabled = activegame.EnableBtnSwitch;
    }

    private void SwitchTurn()
    {
        SwitchTurnBtn.IsEnabled = false;
        activegame.SwitchTurn();
        if (activegame.Winner != Game.TurnEnum.None)
        {
            StartGameBtn.IsEnabled = true;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
	{
        if (sender is Button)
        {
            DoTurn((Button)sender);
        }
    }

	private void Button_Clicked_Start(object sender, EventArgs e)
	{
        StartGame();
	}

	private void Button_Clicked_Switch(object sender, EventArgs e)
	{
        SwitchTurn();
	}

    private void Game_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        RadioButton rb = (RadioButton)sender;
        if(rb.IsChecked && rb.BindingContext != null)
        {
            activegame = (Game)rb.BindingContext;
            this.BindingContext = activegame;
        }
    }
}