using Silicom.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TCKeyTrigger : TransitionCondition
{

    [SerializeField] private InputActionReference inputActionReference;

    private bool _performed;

    private bool _enabled;

    public override bool Condition => EnableCondition();
    public override void ResetCondition()
    {
        _enabled = false;
        _performed = false;
        inputActionReference.action.performed -= OnKeyEvent;
    }

    private bool EnableCondition()
    {
        if (_enabled) return _performed;
        
        _enabled = true;
        inputActionReference.action.performed += OnKeyEvent;
        return false;
    }

    private void OnKeyEvent(InputAction.CallbackContext ctx)
    {
        _performed = true;
    }

}
