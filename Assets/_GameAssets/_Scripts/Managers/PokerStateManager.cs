using UnityEngine;

public class PokerStateManager : MonoSingleton<PokerStateManager>
{
    public PokerState CurrentState { get; private set; }
    private StateContext _stateContext;
    private StartingState _startingState;
    private DealingCards _dealingCards;
    private Preflop _preflopState;

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
        _startingState = new StartingState(GameManager.Instance.Players);
        CurrentState = PokerState.StaringState;
        _stateContext.TransitionTo(_startingState);
    } 

    public void EnterDealingCardsState()
    {
        if (CurrentState != PokerState.StaringState)
        {
            Debug.Log("HATA !!! : " + CurrentState);
            return;
        }
        _dealingCards = new DealingCards();

        CurrentState = PokerState.DealingCards;
        _stateContext.TransitionTo(_dealingCards);
    } 

    public void EnterPreflopState()
    {
        if (CurrentState != PokerState.DealingCards)
        {
            Debug.Log("HATA !!! : " + CurrentState);
            return;
        }
        _preflopState = new Preflop();
        CurrentState = PokerState.Preflop;
        _stateContext.TransitionTo(_preflopState);
    } 
}