using Silicom.StateMachine;
using UnityEngine;

public class TCControlledState : TransitionCondition
{
    [SerializeField] private TCControlled controlled;
    [SerializeField] private bool wantedState;
    
    public override bool Condition => controlled.State == wantedState;
    public override void ResetCondition()
    {
        controlled.State = false;
    }
}