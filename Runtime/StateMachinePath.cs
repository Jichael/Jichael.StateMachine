using System.Collections.Generic;
using Silicom.StateMachine;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class StateMachinePath : MonoBehaviour
{
    
    public List<State> Path { get; } = new List<State>();

    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private bool logs;
    
    private void Awake()
    {
        StateMachineManager.OnStateMachineStarted += OnStateMachineStarted;
        StateMachineManager.OnStateMachineFinished += OnStateMachineFinished;
    }

    private void OnDestroy()
    {
        StateMachineManager.OnStateMachineStarted -= OnStateMachineStarted;
        StateMachineManager.OnStateMachineFinished -= OnStateMachineFinished;
    }

    private void OnStateMachineStarted(StateMachine stateMachine)
    {
        if (stateMachine != this.stateMachine) return;
        if(logs) Debug.Log($"StateMachine started : '{stateMachine.stateMachineName}'", stateMachine);
        stateMachine.OnStateStarted += OnStateStarted;
        stateMachine.OnStateFinished += OnStateFinished;
    }

    private void OnStateMachineFinished(StateMachine stateMachine)
    {
        if (stateMachine != this.stateMachine) return;
        if (logs)
        {
            Debug.Log($"StateMachine finished : '{stateMachine.stateMachineName}'", stateMachine);
            PrintPath();
        }
        stateMachine.OnStateStarted -= OnStateStarted;
        stateMachine.OnStateFinished -= OnStateFinished;
    }
    
    private void OnStateStarted(State state)
    {
        if(logs) Debug.Log($"Started state '{state.name}'", state);
        Path.Add(state);
    }
    
    private void OnStateFinished(State state)
    {
        if(logs) Debug.Log($"Finished state '{state.name}'", state);
    }
    
    private void PrintPath()
    {
        for (int i = 0; i < Path.Count; i++)
        {
            Debug.Log($"Path[{i.ToString()}] : {Path[i]}");
        }
    }
}
