using UnityEngine;

namespace Jichaels.StateMachine
{

    public class TCStatePathError : TransitionCondition
    {

        [SerializeField] private string scenarioName;
        [SerializeField] private string stateName;
        [SerializeField] private int minNumForError;
        [SerializeField] private bool wantedState;

        public override bool Condition => IsError();

        public override void Reset()
        {

        }

        private bool IsError()
        {
            StateMachine[] scenarios = FindObjectsOfType<StateMachine>();
            StateMachine stateMachine = null;

            for (int i = 0; i < scenarios.Length; i++)
            {
                if (scenarios[i].stateMachineName != scenarioName) continue;
                stateMachine = scenarios[i];
                break;
            }

            if (stateMachine == null)
            {
                Debug.LogError($"Scenario '{scenarioName}' could not be found !");
                return !wantedState;
            }

            int nbError = 0;
            for (int i = 0; i < stateMachine.StatePath.Count; i++)
            {
                if (stateMachine.StatePath[i] == stateName)
                {
                    nbError++;
                }
            }

            if (wantedState)
            {
                return nbError >= minNumForError;
            }

            return nbError < minNumForError;

        }
    }

}