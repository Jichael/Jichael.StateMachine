using UnityEngine;

namespace Silicom.StateMachine
{

    public abstract class TransitionCondition : MonoBehaviour
    {
        public abstract bool Condition { get; }

        public abstract void ResetCondition();
    }

}