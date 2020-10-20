using System.Collections;
using System.Collections.Generic;
using CustomPackages.Silicom.Core.Runtime;
using UnityEngine;

namespace Jichaels.StateMachine
{

    public class StateMachine : MonoBehaviour
    {
        public List<string> StatePath { get; } = new List<string>();
        
        public bool IsFinished { get; private set; }
        public State CurrentState { get; private set; }
        public bool WaitingForStateEvents { get; private set; }
        public bool WaitingForTransitionEvents { get; private set; }

        [SerializeField] private State initialState;
        public string stateMachineName;

        public bool dontDestroyOnLoad;

        private bool[] _eventsFinished;
        
        public void LoadState(State state)
        {
            if (state == null)
            {
    #if UNITY_EDITOR
                Debug.LogError($"{CurrentState.name} : Next state is null !", this);
    #endif
                return;
            }
            
            CurrentState = state;
            
            StatePath.Add(CurrentState.name);
            
    #if UNITY_EDITOR
            print($"Entering state '{CurrentState.name}'");
    #endif
            
            if (CurrentState.endState)
            {
                IsFinished = true;
            }

            StartCoroutine(StateEventsCo());
        }

        public void Transition(Transition transition)
        {
            WaitingForTransitionEvents = true;
            StartCoroutine(TransitionCo(transition));
        }

        private IEnumerator TransitionCo(Transition transition)
        {
            
            _eventsFinished = new bool[transition.events.Length];

            for (int i = 0; i < transition.events.Length; i++)
            {
                _eventsFinished[i] = false;
                StartCoroutine(ExecuteEventCo(transition.events[i], i));
            }

            bool allCoroutinesFinished;
            do
            {
                allCoroutinesFinished = true;
                for (int i = 0; i < _eventsFinished.Length; i++)
                {
                    if (_eventsFinished[i] == false) allCoroutinesFinished = false;
                    yield return Yielders.EndOfFrame;
                }
                
            } while (!allCoroutinesFinished);

            WaitingForTransitionEvents = false;
            
            LoadState(transition.nextState);
        }

        private IEnumerator ExecuteEventCo(StateEvent stateEvent, int index)
        {
            yield return new WaitForSeconds(stateEvent.delay);
            stateEvent.events.Invoke();
            _eventsFinished[index] = true;
        }


        private IEnumerator StateEventsCo()
        {
            WaitingForStateEvents = true;

            _eventsFinished = new bool[CurrentState.stateEvent.Length];
            
            for (int i = 0; i < CurrentState.stateEvent.Length; i++)
            {
                _eventsFinished[i] = false;
                StartCoroutine(ExecuteEventCo(CurrentState.stateEvent[i], i));
            }
            
            bool allCoroutinesFinished;
            do
            {
                allCoroutinesFinished = true;
                for (int i = 0; i < _eventsFinished.Length; i++)
                {
                    if (_eventsFinished[i] == false) allCoroutinesFinished = false;
                    yield return Yielders.EndOfFrame;
                }
                
            } while (!allCoroutinesFinished);

            WaitingForStateEvents = false;
        }

        public void StartScenario()
        {
            
    #if UNITY_EDITOR
            print($"Starting state machine '{stateMachineName}'");
    #endif
            IsFinished = false;
            LoadState(initialState);
        }
        
    }

}