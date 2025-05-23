using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{


    public override void _Ready()
    {
        base._Ready();
        
        GameConstants.DPrint("EnemyReturnState::_Ready");
        destination = GetPointGlobalPosition(0);
    }

    protected override void EnterState()
    {
        GameConstants.DPrint("EnemyReturnState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        characterNode.AgentNode.TargetPosition = destination;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void ExitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        } 
        Move();
    }


}
