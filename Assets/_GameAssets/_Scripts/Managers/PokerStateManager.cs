using UnityEngine;

public class PokerStateManager : MonoSingleton<PokerStateManager>
{
    public PokerState CurrentState { get; private set; }
    private StateContext _stateContext;

    private StartingState _startingState;
    private DealingCards _dealingCards;
    private Preflop _preflopState;
    private Flop _flopState;
    private Turn _turnState;
    private River _riverState;
    private EndState _endState;

    private void Awake()
    {
        _stateContext = new StateContext();
    }

    private void Update()
    {
        _stateContext.UpdateState();
    }

    public void EnterState(PokerState pokerState)
    {
        switch (pokerState)
        {
            case PokerState.StaringState:
                EnterStartingState();
                break;
            case PokerState.DealingCards:
                EnterDealingCardsState();
                break;
            case PokerState.Preflop:
                EnterPreflopState();
                break;
            case PokerState.Flop:
                EnterFlopState();
                break;
            case PokerState.Turn:
                EnterTurnState();
                break;
            case PokerState.River:
                EnterRiverState();
                break;
            case PokerState.EndState:
                EnterEndState();
                break;
        }
    }

    public void NextState()
    {
        PokerState nextState = CurrentState;
        switch (CurrentState)
        {
            case PokerState.StaringState:
                nextState = PokerState.DealingCards;
                break;
            case PokerState.DealingCards:
                nextState = PokerState.Preflop;
                break;
            case PokerState.Preflop:
                nextState = PokerState.Flop;
                break;
            case PokerState.Flop:
                nextState = PokerState.Turn;
                break;
            case PokerState.Turn:
                nextState = PokerState.River;
                break;
            case PokerState.River:
                nextState = PokerState.EndState;
                break;

            case PokerState.EndState:
                
                break;
        }
        EnterState(nextState);
    }

    private void EnterStartingState()
    {
        _startingState = new StartingState(GameManager.Instance.Players);
        CurrentState = PokerState.StaringState;
        _stateContext.TransitionTo(_startingState);
    }

    private void EnterDealingCardsState()
    {
        _dealingCards = new DealingCards();

        CurrentState = PokerState.DealingCards;
        _stateContext.TransitionTo(_dealingCards);
    }

    private void EnterPreflopState()
    {
        _preflopState = new Preflop();
        CurrentState = PokerState.Preflop;
        _stateContext.TransitionTo(_preflopState);
    }

    private void EnterFlopState()
    {   
        _flopState = new Flop();
        CurrentState = PokerState.Flop;
        _stateContext.TransitionTo(_flopState);
        UIManager.Instance.ChangeVisibilityBobButton(true);
    }

    private void EnterTurnState()
    {
        _turnState = new Turn();
        CurrentState = PokerState.Turn;
        _stateContext.TransitionTo(_turnState);
    }

    private void EnterRiverState()
    {
        _riverState = new River();
        CurrentState= PokerState.River;
        _stateContext.TransitionTo(_riverState);
    }

    private void EnterEndState()
    {
        _endState = new EndState();
        CurrentState = PokerState.EndState;
        _stateContext.TransitionTo(_endState);
    }

}