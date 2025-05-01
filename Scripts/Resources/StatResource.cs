using System;
using Godot;

[GlobalClass]
public partial class StatResource : Resource 
{
    
    [Export] public Stat  StatType {get; private set;}

    public event Action OnZero;
    public event Action OnUpdate;

    private float _statValue;
    [Export] public float StatValue {
        get => _statValue; 
        set {
            _statValue = Mathf.Clamp(value,0,Mathf.Inf);

            OnUpdate?.Invoke();
            if (_statValue ==0) 
            {
                OnZero?.Invoke();
            }
        }
    }
}