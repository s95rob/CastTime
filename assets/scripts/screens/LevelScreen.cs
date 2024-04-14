using Godot;
using System;

public partial class LevelScreen : GameScreen {

	[Export]
	Timer gameTimer;
	[Export]
	PlayerController player;
	[Export]
	LevelController level;

	[Export]
	AudioStreamPlayer winSound, timeoutSound;

	// UI
	[Export]
	Label timerLabel, scoreLabel, strikesLabel;

	int score = 0;

	int maxStrikes = 5;
	public int strikes = 0;

	protected override void Initialize() {
		gameTimer.Timeout += LoseLevel;
	}

	protected override void OnEnable() {
		score = 0;
		strikes = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		timerLabel.Text = "Cast Time: " + gameTimer.TimeLeft.ToString("0.00");
		scoreLabel.Text = "Streak: " + score.ToString();
		strikesLabel.Text = "Strikes: " + strikes.ToString();
	}

	private void LoseLevel() {
		strikes++;
		if (strikes >= maxStrikes) {
			gameState.GameOver();
		}

		strikesLabel.Text = "Strikes: " + strikes.ToString();
		score = 0;
		timeoutSound.Play();
		player.Respawn();
		NewLevel();
	}

	public void WinLevel() {
		winSound.Play();
		player.Respawn();
		if (score++ > Score.RunHighScore)
			Score.RunHighScore = score;
		NewLevel();
	}

	private void NewLevel() {
		level.GenerateLevel();
		gameTimer.WaitTime = 10.0f;
		gameTimer.Start();
	}
}
