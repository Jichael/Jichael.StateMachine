using UnityEngine;

namespace Jichaels.StateMachine
{

    public class TCTimer : TransitionCondition
    {
        public override bool Condition => TimerCheck();

        public override void Reset()
        {
            _timer = 0;
        }

        [SerializeField] private float timeToWait;
        private float _timer;

        private bool TimerCheck()
        {
            _timer += Time.deltaTime;
            return _timer >= timeToWait;
        }
    }

}