using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PokerStateManager _pokerStateManager;
    public PokerStateManager GetPokerStateManager { get { return _pokerStateManager; } }

    [SerializeField] private MoveManager _moveManager;
    public MoveManager GetMoveManager { get { return _moveManager; } }
    
    private PlayerSequenceHandler _playerSequenceHandler;
    private StateContext _stateContext;

    [SerializeField] private List<Player> _players = new List<Player>();
    public List<Player> GetPlayers { get { return _players; } }

    #region Setting SO
    [SerializeField] private int _smallBlindBet;
    [SerializeField] private int _totalMoney = 100000;
    #endregion

    [SerializeField] private int _minBet;
    public int GetSmallBlindBet { get { return _smallBlindBet; }}
    public int MinBet { get { return _minBet; } set { _minBet = value; } }

    private void Awake()
    {
        _stateContext = new StateContext();
        _minBet = _smallBlindBet;

        foreach (var player in _players)
        {
            player.TotalMoney = _totalMoney;
        }
    }

    private void Start()
    {
        _pokerStateManager.EnterStartingState();
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

    public void SetPlayerQueue(int currentPlayerIndex = 0)
    {
        int currentIndex = currentPlayerIndex;
        Queue<Player> playersQueue = new Queue<Player>();
        
        foreach (var player in _players)
            player.IsMyTurn = false;
        
        for (int i = 0; i < _players.Count; i++)
        {
            if (currentIndex >= _players.Count)
                break;

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