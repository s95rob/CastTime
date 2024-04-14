using Godot;
using System;

public partial class IntroScreen : GameScreen
{
	[Export]
	Node2D title, brief;

	bool postTitleScreen = false;

	public override void _Process(double delta) {
		if (Input.IsActionJustPressed("ui_accept")) {
			if (!postTitleScreen) {
				title.Visible = false;
				brief.Visible = true;
				postTitleScreen = true;
			}
			else gameState.StartGame();
		}
	}
}
