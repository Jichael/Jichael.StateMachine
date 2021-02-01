using System;
using UnityEngine.Events;

namespace Silicom.StateMachine
{

    [Serializable]
    public class StateEvent
    {
        public float delay;
        public UnityEvent events;
    }

}