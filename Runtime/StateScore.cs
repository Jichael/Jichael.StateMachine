using UnityEngine;

namespace Jichaels.StateMachine
{

    public class StateScore : MonoBehaviour
    {

        public int maximumScore;

        public int CurrentScore { get; private set; }


        private void Awake()
        {
            CurrentScore = maximumScore;
        }

        public void ModifyScore(int modifier)
        {
            CurrentScore = Mathf.Clamp(CurrentScore + modifier, 0, maximumScore);
        }

    }

}