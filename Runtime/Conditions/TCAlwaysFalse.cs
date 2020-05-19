namespace Jichaels.StateMachine
{
    public class TCAlwaysFalse : TransitionCondition
    {
        public override bool Condition => false;

        public override void Reset()
        {

        }
    }
}