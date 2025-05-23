using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        GameConstants.DPrint($"StateMachine::_Ready... {currentState} is {GetType().Name} ");
        DumpDebugStates();
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>() {

        CharacterState newState = states.Where ( (state) => state is T).FirstOrDefault();

        if (newState == null) { return;}
        if (currentState is T) { return;}
        if (!newState.CanTransition()) { return;}

        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);

    }

    private void DumpDebugStates() {
        foreach (Node state in states) {
            GameConstants.DPrint(state.GetType().Name);

        }

    }
}
