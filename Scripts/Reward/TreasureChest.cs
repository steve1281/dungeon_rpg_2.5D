using Godot;
using System;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;
    [Export] private RewardResource reward;

    public override void _Ready()
    {
        areaNode.BodyEntered += (body) => {
            GameConstants.DPrint($"{body.Name} {CollisionMask} <========== ");
            spriteNode.Visible = true;
        };

        areaNode.BodyExited += (body) =>
        {
            spriteNode.Visible = false;
        };
    }

    public override void _Input(InputEvent @event)
    {
        if (!areaNode.Monitoring ||
            !areaNode.HasOverlappingBodies() || 
            !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)) { return; }
        
        GameEvents.RaiseReward(reward);
        areaNode.Monitoring = false;

        GameConstants.DPrint("TreasureChest::_Input:Opened Chest!");
    }

}

