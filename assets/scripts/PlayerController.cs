using Godot;
using System;
using System.Diagnostics;

public partial class PlayerController : Area2D
{
	enum MoveState {
		Idle, Moving
	} 

	[Export]
	LevelScreen levelScreenRef;

	MoveState moveState;

	Vector2 targetPosition, nextDirection,
		startPosition,
		lastPosition;
	float moveUnit = 16.0f;
	[Export]
	public float moveSpeed = 10.0f;

	[Export]
	public RayCast2D collisionTest;

	[Export]
	public CollisionShape2D Collider;
	public Vector2 baseColliderPosition;

	[Export]
	AudioStreamPlayer moveSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		BodyEntered += OnTileCollision;
		AreaEntered += OnTileCollision;
		moveState = MoveState.Idle;
		baseColliderPosition = Collider.Position;
		startPosition = Position;
		nextDirection = new Vector2(0.0f, 0.0f);
	}

	public void Respawn() {
		Position = startPosition;
		moveState = MoveState.Idle;
		nextDirection = new Vector2(0.0f, 0.0f);
		moveSound.PitchScale = 1.0f;
	}

	public void OnTileCollision(Node2D body) {
		if (body.Name == "Portal") {
			levelScreenRef.WinLevel();
		}
		else {
			Respawn();
		}
	}
	
	public override void _Process(double delta) {
		// Check for input every frame 
		Vector2 newDirection = GetMovementInput();

		if (newDirection.Length() > 0.0f)
			nextDirection = newDirection;
	}

	public override void _PhysicsProcess(double delta) {
		if (moveState == MoveState.Idle) {
			targetPosition = nextDirection;
			if (targetPosition.Length() > 0.0f) {
				targetPosition += Position;
				moveSound.Play();
				moveSound.PitchScale += 0.1f;
				moveState = MoveState.Moving;
			}
		}
		else if (moveState == MoveState.Moving) {
			Position = new Vector2((float)Mathf.MoveToward(Position.X, targetPosition.X, delta * moveSpeed),
			(float)Mathf.MoveToward(Position.Y, targetPosition.Y, delta * moveSpeed));
			if (Position == targetPosition) {
				moveState = MoveState.Idle;
			}
		}

		
	}

	public Vector2 GetMovementInput() {
		Vector2 pos = new Vector2(0.0f, 0.0f);

		if (Input.IsActionPressed("ui_up"))
			pos = new Vector2(0.0f, -moveUnit);
		else if (Input.IsActionPressed("ui_down"))
			pos = new Vector2(0.0f, moveUnit);
		else if (Input.IsActionPressed("ui_left"))
			pos = new Vector2(-moveUnit, 0.0f);
		else if (Input.IsActionPressed("ui_right"))
			pos = new Vector2(moveUnit, 0.0f);
			
		return pos;
	}

	public bool CheckTileCollision(Vector2 queryPosition) {
		collisionTest.TargetPosition = queryPosition;
		return collisionTest.IsColliding();
	}
}
