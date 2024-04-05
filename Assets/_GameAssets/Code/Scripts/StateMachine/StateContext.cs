public class StateContext
{
    private IPokerState _pokerState;
 
    public void TransitionTo(IPokerState state)
    {
        _pokerState?.ExitState();
        _pokerState = state;
        _pokerState.EnterState();
    }
     
    public void UpdateState()
    {
        _pokerState?.UpdateState();
    }
}
