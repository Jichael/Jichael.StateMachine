using UnityEngine;

namespace Silicom.StateMachine
{

    public class Transition : MonoBehaviour
    {

        public State nextState;

        public StateEvent[] events;

        [HideInInspector] public TransitionCondition[] conditions;


        public bool Condition => CheckCondition();

        private bool CheckCondition()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].Condition) return false;
            }

            return true;
        }

        public void ResetConditions()
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                conditions[i].ResetCondition();
            }
        }


#if UNITY_EDITOR
    private void OnValidate()
    {
        conditions = GetComponents<TransitionCondition>();
        if(nextState != null) name = $"Transition_{nextState.name}";
    }
#endif

    }

}