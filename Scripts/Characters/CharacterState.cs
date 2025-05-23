using Godot;
using System;

public abstract partial class CharacterState: Node
{
    protected Character characterNode;
    public Func<bool> CanTransition = () => true;

    public override void _Ready()
    {
        characterNode = GetOwner<Character>();
        SetPhysicsProcess(false); 
        SetProcessInput(false);
    }


    public override void _Notification(int what)
    {
        
        base._Notification(what);

        if (what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
            SetPhysicsProcess(true); 
            SetProcessInput(true);
        }
        else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            SetPhysicsProcess(false); 
            SetProcessInput(false);
            ExitState();
        }
    }

    protected abstract void EnterState();
    protected virtual void ExitState() {}
}
