using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    //private CharacterBody3D target;
    [Export] public Area3D AttackAreaNode {get; private set; }

    private Vector3 targetPosition;

    protected override void EnterState()
    {
        GD.Print("EnemyAttackState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);


        
//        AttackAreaNode.BodyExited += HandleChaseAreaBodyExited;
        // target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;
        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
        //chaseTimerNode.Timeout += HandleTimeout;
        targetPosition = target.GlobalPosition;
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }



    protected override void ExitState(){

        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        //AttackAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

    private void PerformHit() 
    {
        GD.Print("EnemyAttackState::PerformHit called.");
        characterNode.ToggleHitbox(false);
        characterNode.HitboxNode.GlobalPosition = targetPosition ;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        characterNode.ToggleHitbox(true); 
        Node3D target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();
        if (target == null) {
            Node3D chaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (chaseTarget == null) {
                characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
                return;
            }
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
            return;
        }
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
        targetPosition = target.GlobalPosition;

        Vector3 direction = characterNode.GlobalPosition.DirectionTo(targetPosition);
        characterNode.SpriteNode.FlipH = direction.X < 0;

    }
}
