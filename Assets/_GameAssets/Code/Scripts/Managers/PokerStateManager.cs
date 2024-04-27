using UnityEngine;

public class PokerStateManager : MonoSingleton<PokerStateManager>
{
    StateContext _stateContext;

    //All States
    PokerState _currentState;
    private StartingState _startingState;
    private DealingCards _dealingCards;
    private Preflop _preflopState;

    public PokerState GetCurrentState { get { return _currentState; } }


    private void Awake()
    {
        _stateContext = new StateContext();
    }

    private void Update()
    {
        _stateContext.UpdateState();
    }

    public void EnterStartingState()
    {
        _startingState = new StartingState(GameManager.Instance.GetPlayers);
        _currentState = PokerState.StaringState;
        _stateContext.TransitionTo(_startingState);
    } 

    public void EnterDealingCardsState()
    {
        if (_currentState != PokerState.StaringState)
        {
            Debug.Log("HATA !!! : " + _currentState);
            return;
        }
        _dealingCards = new DealingCards();

        _currentState = PokerState.DealingCards;
        _stateContext.TransitionTo(_dealingCards);
    } 

    public void EnterPreflopState()
    {
        if (_currentState != PokerState.DealingCards)
        {
            Debug.Log("HATA !!! : " + _currentState);
            return;
        }
        _preflopState = new Preflop();
        _currentState = PokerState.Preflop;
        _stateContext.TransitionTo(_preflopState);
    } 
}