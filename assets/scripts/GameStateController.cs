using Godot;
using System;
using System.IO;

public partial class GameStateController : Node
{
	[Export]
	IntroScreen introScreen;

	[Export]
	LevelScreen levelScreen;

	[Export]
	GameOverScreen gameOverScreen;


	public override void _Ready() {
		Score.SavedScore = Score.LoadScore();
	}
	
	public void StartGame() {
		introScreen.Disable();
		gameOverScreen.Disable();
		levelScreen.Enable();
	}

	public void GameOver() {
		if (Score.RunHighScore > Score.SavedScore)
			Score.SaveScore(Score.RunHighScore);

		levelScreen.Disable();
		gameOverScreen.Enable();
	}
}
