using UnityEngine;

namespace Jichaels.StateMachine
{

    public class TCStatePathError : TransitionCondition
    {

        [SerializeField] private StateMachinePath stateMachinePath;
        [SerializeField] private State state;
        [SerializeField] private int minNumForError;
        [SerializeField] private bool wantedState;

        public override bool Condition => IsError();

        public override void Reset()
        {

        }

        private bool IsError()
        {
            int nbError = 0;
            
            for (int i = 0; i < stateMachinePath.Path.Count; i++)
            {
                if (stateMachinePath.Path[i] == state)
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