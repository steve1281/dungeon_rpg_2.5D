using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero) {
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>();                
        }
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
    }

    protected override void EnterState() 
    {
        GameConstants.DPrint("PlayerIdleState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }
}
