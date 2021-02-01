using System;
using System.Collections;
using CustomPackages.Silicom.Core.Runtime;
using UnityEngine;

namespace Silicom.StateMachine
{

    public class StateMachineManager : MonoBehaviour
    {

        public StateMachine CurrentStateMachine { get; private set; }
        public static event Action<StateMachine> OnStateMachineStarted;
        public static event Action<StateMachine> OnStateMachineFinished;

        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private bool autoStart;

        private void Start()
        {
            if (!stateMachine) return;
            LoadStateMachine(stateMachine);
            if (autoStart) StartLoadedStateMachine();
        }

        public void LoadStateMachine(StateMachine toLoad)
        {
            CurrentStateMachine = toLoad;
        }

        public void StartLoadedStateMachine()
        {
            OnStateMachineStarted?.Invoke(CurrentStateMachine);
            CurrentStateMachine.StartScenario();
            StartCoroutine(ExecuteStateMachine());
        }

        private IEnumerator ExecuteStateMachine()
        {
            while (!CurrentStateMachine.IsFinished)
            {
                if (CurrentStateMachine.WaitingForStateEvents || CurrentStateMachine.WaitingForTransitionEvents)
                {
                    yield return Yielders.EndOfFrame;
                    if (CurrentStateMachine.IsFinished) break;
                    continue;
                }

                int nextState = CurrentStateMachine.CurrentState.MoveNext();

                if (nextState == -1)
                {
                    yield return Yielders.EndOfFrame;
                    continue;
                }

                CurrentStateMachine.Transition(CurrentStateMachine.CurrentState.transitions[nextState]);

                yield return Yielders.EndOfFrame;
            }
            
            OnStateMachineFinished?.Invoke(CurrentStateMachine);

        }
    }

}