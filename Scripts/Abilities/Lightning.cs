using Godot;
using System;

public partial class Lightning : Ability
{
    public override void _Ready()
    {
        playerNode.AnimationFinished += (animName) =>
        {
            GameConstants.DPrint("Lightning::Lambda:Freeing the lightning animation.");
            QueueFree();
        };
    }


}
