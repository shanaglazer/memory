using MemorySystem;


namespace MemoryMaui;

public partial class Memory : ContentPage
{
    Game game = new();

    List<Button> lstbuttons;

    public Memory()
	{
		InitializeComponent();
        this.BindingContext = game;
        lstbuttons = new() { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18, btn19, btn20, btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28, btn29, btn30 };
    }

    private void StartGame()
    {
        game.StartGame();
        StartGameBtn.IsEnabled = false;
    }

    private void DoTurn(Button btn)
    {
        int index = lstbuttons.IndexOf(btn);
        game.TakeTurn(index);
        SwitchTurnBtn.IsEnabled = game.EnableBtnSwitch;
    }

    private void SwitchTurn()
    {
        SwitchTurnBtn.IsEnabled = false;
        game.SwitchTurn();
        if (game.Winner != Game.TurnEnum.None)
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
}