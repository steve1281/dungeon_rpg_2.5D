using Godot;
using System;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer dashTimerNode;
    [Export] private Timer cooldownTimerNode;
    [Export] private PackedScene bombScene;

    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10f;


    public override void _Ready()
    {
        base._Ready();
        dashTimerNode.Timeout += HandleDashTimeout;
        CanTransition = () => cooldownTimerNode.IsStopped();
    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide(); 
        characterNode.Flip();
    }

    private void HandleDashTimeout()
    {
        cooldownTimerNode.Start();
        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();

    }

    protected override void EnterState()
    {
        GameConstants.DPrint("PlayerDashState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new (
            characterNode.direction.X, 0, characterNode.direction.Y
        );
        // what if Velocity is 0,0,0 ? In this case assign a RIGHT/LEFT based on FLipH
        if (characterNode.Velocity == Vector3.Zero) {
            characterNode.Velocity = characterNode.SpriteNode.FlipH ? 
            Vector3.Left : Vector3.Right;
        }
        characterNode.Velocity *= speed;
        dashTimerNode.Start();

        Node3D bomb = bombScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = characterNode.GlobalPosition;
        
    }

}

