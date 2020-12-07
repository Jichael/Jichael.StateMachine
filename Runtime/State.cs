using System;
using UnityEngine;


namespace Jichaels.StateMachine
{

    public class State : MonoBehaviour
    {

        public event Action OnStateStarted;
        
        public StateEvent[] stateEvent;

        [HideInInspector] public Transition[] transitions;

        public bool endState;

        public void StartState()
        {
            OnStateStarted?.Invoke();
        }

        public int MoveNext()
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].Condition)
                {
                    transitions[i].ResetConditions();
                    return i;
                }
            }

            return -1;
        }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (transform.childCount == 0 && !endState)
        {
            GameObject transition = new GameObject();
            transition.AddComponent<Transition>();
            transition.transform.SetParent(transform);
        }

        transitions = GetComponentsInChildren<Transition>();
    }

    /* Rename all states in order
    [Button(ButtonSizes.Medium)]
    private void RenameState()
    {
        Transform parent = transform.parent;
        int nbChild = parent.childCount;

        for (int i = 0; i < nbChild; i++)
        {
            parent.GetChild(i).name = $"State{i.ToString()}";
        }
    }
    */
#endif

    }

}