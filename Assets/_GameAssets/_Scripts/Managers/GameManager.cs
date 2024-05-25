using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameSettingsSO _settingsSO;

    public int MinBet;

    [field: SerializeField] public DealerController DealerController { get; private set; }
    [field: SerializeField] public List<Player> AllPlayers { get; private set; } = new List<Player>();
    public List<Player> Players { get; private set; } = new List<Player>();
    [field: SerializeField] public List<Transform> CardLocationOnTheTable { get; private set;} = new List<Transform>();
    [field: SerializeField] public List<Card> CardsOnTheTable { get; private set;} = new List<Card>();

    public PlayerSequenceHandler PlayerSequenceHandler { get; private set; } = new PlayerSequenceHandler();
    public RealPlayer RealPlayer { get; private set;}

    #region Setting SO
    private int _playerCount, _smallBlindBet, _totalMoney;
    #endregion
 
    private void Awake()
    {
        _totalMoney = _settingsSO.PlayersTotalMoney;
        _smallBlindBet = _settingsSO.SmallBlindBet;
        _playerCount = _settingsSO.BotCount;
    }

    private void Start()
    {
        SetPlayers();
        StartGame();
    }

    private void SetPlayers()
    {
        List<AIPlayer> aiPlayers = new List<AIPlayer>();
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
            Debug.LogError("Geçersiz bot sayýsý");
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

        foreach (Player player in AllPlayers)
        {
            if (player.GetType() == typeof(AIPlayer))
            {
                var aiPlayer = (AIPlayer)player;
                if (!aiPlayer.IsActive)
                    continue;
            }
            else if (player.GetType() == typeof(RealPlayer))
            {
                RealPlayer = (RealPlayer)player;
            }
            player.TotalMoney = _totalMoney;
            Players.Add(player);
        }
    }

    public void StartGame()
    {
        MinBet = _smallBlindBet;
        PokerStateManager.Instance.EnterState(PokerState.StaringState);
    }

    public void NewGame()
    {
        StartCoroutine(NewGameDelay());
    }

    private IEnumerator NewGameDelay()
    {
        yield return new WaitForSeconds(2f);

        int count = Players.Count;

        Stack<Player> removedPlayer = new Stack<Player>();

        foreach (Player player in Players)
        {
            if (player.TotalMoney <= 0 || player.TotalMoney <= MinBet)
            {
                if (player.GetType() == typeof(RealPlayer))
                {
                    SceneLoaderManager.LoadMainMenuScene();
                    break;
                }
                count--;
                removedPlayer.Push(player);
            }
        }

        while (removedPlayer.Count > 0)
        {
            Player player = removedPlayer.Pop();
            player.gameObject.SetActive(false);
            Players.Remove(player);
        }

        foreach (var player in Players)
        {
            player.ResetPlayer();
        }

        CardsOnTheTable.Clear();
        DealerController.ResetDealer();
        DeckManager.ResetDeck();

        MinBet = _settingsSO.SmallBlindBet; 

        yield return new WaitForSeconds(1f);

        PokerCanvas.Instance.ChangeVisibilityWinInfoPanel(false);

        if (count > 1)
        {
            StartGame();
        }
        else
        {
            SceneLoaderManager.LoadMainMenuScene();
        }
    }
}