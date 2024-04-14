using Godot;
using System;
using System.Runtime.InteropServices;

public partial class LevelController : Node2D
{
	[Export]
	Vector2 levelOrigin;

	[Export]
	Vector2 levelSize;

	[Export]
	Area2D blockTemplate;

	private Random random;
	private float levelUnit = 16.0f;

	public override void _Ready() {
		random = new Random();
		GenerateLevel();
	}
	
	public void GenerateLevel() {
		foreach (Node child in GetChildren()) 
			RemoveChild(child);

		for (int x = 0; x < levelSize.X; x++) {
			for (int y = 0; y < levelSize.Y; y++) {
				if (Chance(4)) {
					Vector2 blockPosition = new Vector2(x * levelUnit, y * levelUnit);
					CreateBlock(blockPosition);
				}
			}
		}
	}

	private void CreateBlock(Vector2 position) {
		Area2D block = (Area2D)blockTemplate.Duplicate();
		AddChild(block);
		block.Position = position;
	}

	private bool Chance(int odds) {
		int chance = (int)(random.NextSingle() * (float)odds);
		return chance == 0;
	}
}
