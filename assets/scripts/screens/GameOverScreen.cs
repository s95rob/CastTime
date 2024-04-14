using System.Runtime.CompilerServices;
using Godot;

public partial class GameOverScreen : GameScreen {

    [Export]
    Timer inputTimer;

    [Export]
    Label runScoreLabel, overallScoreLabel, inputLabel;

    [Export]
    AudioStreamPlayer loseSound;

    bool allowInput = false;

    protected override void Initialize() {
        inputTimer.Timeout += () => { allowInput = true; inputLabel.Visible = true; };
    }

    protected override void OnEnable() {
        loseSound.Play();
        inputLabel.Visible = false;
        allowInput = false;
        inputTimer.Start();

        runScoreLabel.Text = "This Run: " + Score.RunHighScore.ToString();
        overallScoreLabel.Text = "Overall: " + Score.SavedScore.ToString();
    }

    public override void _Process(double delta) {
        if (Input.IsActionPressed("ui_accept") && allowInput)
            gameState.StartGame();
    }
}