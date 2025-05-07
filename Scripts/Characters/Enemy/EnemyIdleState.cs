using Godot;
using System;

public partial class EnemyIdleState : EnemyState
{
    public override void _PhysicsProcess(double delta)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    protected override void EnterState()
    {
        GameConstants.DPrint("EnemyIdleState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }
}
