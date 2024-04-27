using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int MinBet;
    [field: SerializeField] public List<Player> Players { get; private set; } = new List<Player>();
    private PlayerSequenceHandler _playerSequenceHandler;
    private StateContext _stateContext;

    #region Setting SO
    [field : SerializeField] public int SmallBlindBet { get; private set; }
    [SerializeField] private int _totalMoney;
    #endregion

    private void Awake()
    {
        _stateContext = new StateContext();
        MinBet = SmallBlindBet;

        foreach (var player in Players)
        {
            player.TotalMoney = _totalMoney;
        }
    }

    private void Start()
    {
        PokerStateManager.Instance.EnterStartingState();
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
        
        foreach (var player in Players)
            player.IsMyTurn = false;
        
        for (int i = 0; i < Players.Count; i++)
        {
            if (currentIndex >= Players.Count)
                break;

            playersQueue.Enqueue(Players[currentIndex]);
            currentIndex++;
        }

        _playerSequenceHandler = new PlayerSequenceHandler(playersQueue);
    }

    private void OnDisable()
    {
        _playerSequenceHandler.OnDisable();
    }
}