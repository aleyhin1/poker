public class StateContext
{
    private IPokerState _pokerState;
 
    public void TransitionTo(IPokerState state)
    {
        _pokerState = state;
        _pokerState.EnterState();
    }
}
