using Godot;
using System;

public partial class EnemyStunState : EnemyState
{
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_STUN);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {

        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
    }


    private void HandleAnimationFinished(StringName animName)
    {
        if (characterNode.AttackAreaNode.HasOverlappingBodies()) {
            characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
        } else if (characterNode.ChaseAreaNode.HasOverlappingBodies()) {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        } else {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
    }
}
