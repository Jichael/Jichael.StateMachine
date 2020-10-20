using System.Collections;
using CustomPackages.Silicom.Core.Runtime;
using UnityEngine;

namespace Jichaels.StateMachine
{

    public class StateMachineManager : MonoBehaviour
    {

        public StateMachine CurrentStateMachine { get; private set; }

        [SerializeField] private StateMachine stateMachine;

        private void Start()
        {
            LoadStateMachine(stateMachine);
            StartLoadedStateMachine();
        }

        public void LoadStateMachine(StateMachine toLoad)
        {
            CurrentStateMachine = toLoad;
        }

        public void StartLoadedStateMachine()
        {
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

#if UNITY_EDITOR
        print($"{CurrentStateMachine.name} is finished !");
        PrintPath();
#endif

            if (CurrentStateMachine.dontDestroyOnLoad)
            {
                CurrentStateMachine.transform.DetachChildren();
                CurrentStateMachine.transform.SetParent(null);
                DontDestroyOnLoad(CurrentStateMachine.gameObject);
            }


        }

#if UNITY_EDITOR
        private void PrintPath()
        {
            for (int i = 0; i < CurrentStateMachine.StatePath.Count; i++)
            {
                Debug.Log($"Path[{i.ToString()}] : {CurrentStateMachine.StatePath[i]}");
            }
        }
#endif

    }

}