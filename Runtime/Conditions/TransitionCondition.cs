using UnityEngine;

namespace Jichaels.StateMachine
{

    public abstract class TransitionCondition : MonoBehaviour
    {
        public abstract bool Condition { get; }

        public abstract void Reset();
    }

}