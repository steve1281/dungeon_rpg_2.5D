using Godot;
using System;

public partial class Player : Character
{

    public override void _Ready()
    {
        base._Ready();

        GameEvents.OnReward += HandleReward;
    }


    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    private void HandleReward(RewardResource resource)
    {
        StatResource targetStat = GetStatResource(resource.TargetStat);
        targetStat.StatValue += resource.Amount;
    }


}
