public class StateContext
{
    private IPokerState currentState;

    public void TransitionTo(IPokerState state)
    {
        currentState?.ExitState();
        currentState = state;
        currentState.EnterState();
    }
    public void UpdateState()
    {
        currentState?.UpdateState();
    }
}
