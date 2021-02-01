using UnityEngine;

namespace Silicom.StateMachine
{
    public class TCTransformDistance : TransitionCondition
    {

        [SerializeField] private Transform transform1;
        [SerializeField] private Transform transform2;

        [SerializeField] private float distance;

        [SerializeField] private bool checkIfWithin;

        public override bool Condition => CheckForDistance();

        public override void ResetCondition()
        {

        }

        private bool CheckForDistance()
        {
            float dist = Vector3.Distance(transform1.position, transform2.position);

            if (checkIfWithin)
            {
                return dist <= distance;
            }

            return dist >= distance;
        }

    }
}