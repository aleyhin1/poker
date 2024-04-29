using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int MinBet;
    [field: SerializeField] public List<Player> Players { get; private set; } = new List<Player>();
    public PlayerSequenceHandler PlayerSequenceHandler { get; private set; }
 
    #region Setting SO
    [field : SerializeField] public int SmallBlindBet { get; private set; }
    [SerializeField] private int _totalMoney;
    #endregion

    private void Awake()
    {    
        MinBet = SmallBlindBet;

        foreach (var player in Players)
        {
            player.TotalMoney = _totalMoney;
        }
    }

    private void Start()
    {
        PlayerSequenceHandler = new PlayerSequenceHandler();
        PokerStateManager.Instance.EnterState(PokerState.StaringState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerSequenceHandler.NextPlayer();
        }
    } 
}