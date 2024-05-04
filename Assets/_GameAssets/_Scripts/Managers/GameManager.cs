using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameSettingsSO _settingsSO;

    public int MinBet;

    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public DealerController DealerController { get; private set; }

    [field: SerializeField] public List<Player> AllPlayers { get; private set; } = new List<Player>();
    public List<Player> Players { get; private set; } = new List<Player>();

    [field: SerializeField] public List<Transform> CardLocationOnTheTable { get; private set;} = new List<Transform>();
    [field: SerializeField] public List<Card> CardsOnTheTable { get; private set;} = new List<Card>();

    public PlayerSequenceHandler PlayerSequenceHandler { get; private set; }
    public RealPlayer RealPlayer { get; private set;}

    public Stack<Player> LeaderBoardPlayerStack { get; private set;} = new Stack<Player> ();

    #region Setting SO
    private int _playerCount, _smallBlindBet, _totalMoney;
    #endregion
 
    private void Start()
    {
        _totalMoney = _settingsSO.PlayersTotalMoney;
        _smallBlindBet = _settingsSO.SmallBlindBet;
        _playerCount = _settingsSO.PlayerCount;

        SetPlayers();
    }

    private void SetPlayers()
    {
        List<AIPlayer> aiPlayers = new List<AIPlayer> ();
        List<AIPlayer> activePlayers = new List<AIPlayer>();

        foreach (Player player in AllPlayers)
        {
            if (player.GetType() == typeof(AIPlayer))
            {
                AIPlayer aiPlayer = (AIPlayer)player;

                aiPlayer.IsActive = false;
                aiPlayers.Add(aiPlayer);
            }
        }

        if (_playerCount > aiPlayers.Count)
        {
            Debug.Log("Geçersiz bot sayýsý");
            return;
        }

        while (_playerCount > 0)
        {
            var rndPlayer = aiPlayers[Random.Range(0, aiPlayers.Count)];

            if (!activePlayers.Contains(rndPlayer))
            {
                rndPlayer.IsActive = true;
                activePlayers.Add(rndPlayer);
                _playerCount--;
            }
        }

        StartGame();
    }

    private void StartGame()
    {
        MinBet = _smallBlindBet;

        foreach (Player player in AllPlayers)
        {
            if (player.GetType() == typeof(AIPlayer))
            {
                var aiPlayer = (AIPlayer)player;
                if (!aiPlayer.IsActive)
                    continue;
            }
            else if(player.GetType() == typeof(RealPlayer))
            {
                RealPlayer = (RealPlayer)player;
            }
            player.TotalMoney = _totalMoney;
            Players.Add(player);
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