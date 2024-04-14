using System;
using Godot;

public partial class GameScreen : Node2D {
    protected GameStateController gameState;

    public override void _Ready() {
        gameState = GetNode<GameStateController>("/root/Game");
        Initialize();
    }

    public void Enable() {
        ProcessMode = ProcessModeEnum.Inherit;
        Visible = true;
        OnEnable();
    }

    public void Disable() {
        ProcessMode = ProcessModeEnum.Disabled;
        Visible = false;
        OnDisable();
    }

    protected virtual void Initialize() {}
    protected virtual void OnEnable() {}
    protected virtual void OnDisable() {}
}