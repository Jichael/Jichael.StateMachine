using Jichaels.StateMachine;
using UnityEngine;

public class TCControlledState : TransitionCondition
{
    [SerializeField] private TCControlled controlled;
    [SerializeField] private bool wantedState;
    
    public override bool Condition => controlled.State == wantedState;
    public override void Reset()
    {
        controlled.State = false;
    }
}