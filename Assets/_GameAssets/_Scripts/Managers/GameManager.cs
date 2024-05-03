using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int MinBet;

    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public DealerController DealerController { get; private set; }

    [field: SerializeField] public List<Player> Players { get; private set; } = new List<Player>();
    [field: SerializeField] public List<Transform> CardLocationOnTheTable { get; private set;} = new List<Transform>();
    [field: SerializeField] public List<Card> CardsOnTheTable { get; private set;} = new List<Card>();

    
    public PlayerSequenceHandler PlayerSequenceHandler { get; private set; }
    public RealPlayer RealPlayer { get; private set;}

    public Stack<Player> LeaderBoardPlayerStack { get; private set;} = new Stack<Player> ();

    #region Setting SO
    [field : SerializeField] public int SmallBlindBet { get; private set; }
    [SerializeField] private int _totalMoney;
    #endregion
 
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        MinBet = SmallBlindBet;

        foreach (var player in Players)
        {
            if (player.GetType() == typeof(RealPlayer))
                RealPlayer = (RealPlayer)player;

            player.TotalMoney = _totalMoney;
        }

        PlayerSequenceHandler = new PlayerSequenceHandler();
        PokerStateManager.Instance.EnterState(PokerState.StaringState);
    }

    public void EndGame()
    {
        if (PokerStateManager.Instance.CurrentState != PokerState.EndState)
            PokerStateManager.Instance.EnterState(PokerState.EndState);
    }
}