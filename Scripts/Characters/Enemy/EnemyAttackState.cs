using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    private CharacterBody3D target;

    protected override void EnterState()
    {
        GD.Print("EnemyAttackState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;
        //chaseTimerNode.Timeout += HandleTimeout;
    }

}
