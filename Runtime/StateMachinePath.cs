using System.Collections.Generic;
using Jichaels.StateMachine;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class StateMachinePath : MonoBehaviour
{
    
    public List<State> Path { get; } = new List<State>();

    [SerializeField] private StateMachine stateMachine;

    [SerializeField] private bool logs;

    private void OnValidate()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    private void Start()
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
        if(logs) Debug.Log($"StateMachine started : '{stateMachine.stateMachineName}'");
        stateMachine.OnStateStarted += OnStateStarted;
        stateMachine.OnStateFinished += OnStateFinished;
    }

    private void OnStateMachineFinished(StateMachine stateMachine)
    {
        if (logs)
        {
            Debug.Log($"StateMachine finished : '{stateMachine.stateMachineName}'");
            PrintPath();
        }
        stateMachine.OnStateStarted -= OnStateStarted;
        stateMachine.OnStateFinished -= OnStateFinished;
    }
    
    private void OnStateStarted(State state)
    {
        if(logs) Debug.Log($"Started state '{state.name}'");
        Path.Add(state);
    }
    
    private void OnStateFinished(State state)
    {
        if(logs) Debug.Log($"Finished state '{state.name}'");
    }
    
    private void PrintPath()
    {
        for (int i = 0; i < Path.Count; i++)
        {
            Debug.Log($"Path[{i.ToString()}] : {Path[i]}");
        }
    }
}
