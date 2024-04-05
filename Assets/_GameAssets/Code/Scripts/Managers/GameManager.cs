using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private PlayerSequenceHandler _playerSequenceHandler;
    private StateContext _stateContext;
    [SerializeField] private List<Player> _players = new List<Player>();

    //bets
    [SerializeField] private int _smallBlindBet;
    private int _minBet;
    public int SmallBlindBet { get { return _smallBlindBet; }}
    public int MinBet { get { return _minBet; } set { _minBet = value; } }

    //All States
    PokerState _currentState;
    private StartingState _startingState;
    private Preflop _preflopState;
    
    private void Awake()
    {
        _stateContext = new StateContext();
        _minBet = _smallBlindBet * 2;
    }

    private void Start()
    {
       
        _startingState = new StartingState(_players);
        _preflopState = new Preflop();

        _currentState = PokerState.StaringState;
        _stateContext.TransitionTo(_startingState);
    }

    private void Update()
    {
        _stateContext.UpdateState();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerSequenceHandler.NextPlayerAction?.Invoke();
        }
    }

    public void NextPlayer()
    {
        _playerSequenceHandler.NextPlayerAction?.Invoke();
    }

    public void NextState()
    {
        PokerState nextState = _currentState;

        switch (_currentState)
        {
            case PokerState.StaringState:
                nextState = PokerState.Preflop;
                SetPlayerQueue(0);
                _stateContext.TransitionTo(_preflopState);
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
            default:
                break;
        }
        _currentState = nextState;
    }

    public void SetPlayerQueue(int currentPlayerIndex)
    {
        int currentIndex = currentPlayerIndex;
        Queue<Player> playersQueue = new Queue<Player>();

        for (int i = 0; i < _players.Count; i++)
        {
            if (currentIndex >= _players.Count)
                currentIndex = 0;

            playersQueue.Enqueue(_players[currentIndex]);
            currentIndex++;
        }

        _playerSequenceHandler = new PlayerSequenceHandler(playersQueue);
    }
 
    private void OnDisable()
    {
        _playerSequenceHandler.OnDisable();
    }
}
