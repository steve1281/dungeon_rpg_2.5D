using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;
    
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready(); // need to call this
        comboTimerNode.Timeout += () => comboCounter =1;
    }

    protected override void EnterState()
    {
        GD.Print("PlayerAttackState::EnterState");
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK + comboCounter, -1, 1.5f);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        comboTimerNode.Start();
    }

    private void HandleAnimationFinished(StringName animName)
    {
        // we can safely assume that the attack animation is playing (ATTACK1 or ATTACK2)
        GD.Print("PlayerAttackState::HandleAnimationFinished: ", animName);
        comboCounter++;
        comboCounter = Mathf.Wrap(comboCounter, 1, maxComboCount + 1);
        characterNode.ToggleHitbox(true);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
        
    }

    private void PerformHit() 
    {
        GD.Print("PlayerAttackState::PerformHit called.");
        Vector3 newPosition = characterNode.SpriteNode.FlipH ?
            Vector3.Left :
            Vector3.Right;
        //hitbox is dependent 
        float distanceMultiplier = 0.75f;
        newPosition *= distanceMultiplier;
        characterNode.HitboxNode.Position = newPosition ;
        characterNode.ToggleHitbox(false);
    }

}
