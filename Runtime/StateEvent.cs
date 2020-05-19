using System;
using UnityEngine.Events;

namespace Jichaels.StateMachine
{

    [Serializable]
    public class StateEvent
    {
        public float delay;
        public UnityEvent events;
    }

}