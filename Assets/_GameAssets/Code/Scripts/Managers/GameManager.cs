using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private PlayerSequenceHandler _playerSequenceHandler;
    private StateContext _stateContext;

    [SerializeField] private List<Player> _players = new List<Player>();

    //bets
    [SerializeField] private float _smallBlindBet;
    private float _minBigBlindBet;
    public float MinBigBlindBet { get { return _minBigBlindBet; }}

    //All States
    private Preflop _preflop;

    private void Awake()
    {
        _stateContext = new StateContext();
        _minBigBlindBet = _smallBlindBet * 2;
    }

    private void Start()
    {
        _preflop = new Preflop();
        _stateContext.TransitionTo(_preflop);
        _playerSequenceHandler = new PlayerSequenceHandler(_players);
    }

    private void Update()
    {
        _stateContext.UpdateState();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerSequenceHandler.NextPlayerAction?.Invoke();
        }
    }
    
    
    public void Player()
    {
        Debug.Log("Player");
    }


    private void OnDisable()
    {
        _playerSequenceHandler.OnDisable();
    }
}