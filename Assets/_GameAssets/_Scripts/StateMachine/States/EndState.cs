using UnityEngine;
using System.Collections.Generic;

public class EndState : IPokerState
{
    public static bool isForceToFold { get; set; }

    private Stack<Player> _leaderBoardPlayerStack;
    private List<Player> _playerList;

    public void EnterState()
    {
        Debug.Log("-------EndState--------");
        GameManager.Instance.DealerController.CollectBets();


        Debug.Log("oyuncularýn kartlarý hesaplanýyor..");

        _leaderBoardPlayerStack = GameManager.Instance.LeaderBoardPlayerStack;
        _playerList = GameManager.Instance.Players;

        foreach (Player player in _playerList)
        {
            player.Cards[0].FaceUp();
            player.Cards[1].FaceUp();
        }

        // TEST
        (Dictionary<Player, (HandRank, CardRank[], List<Card>)>, CardRank?) winnerHandInfoPairs = HandComparer.GetWinnerHands(_playerList, GameManager.Instance.CardsOnTheTable);

        Debug.Log("Winners: ");
        Player winner = null;

        foreach (Player player in winnerHandInfoPairs.Item1.Keys)
        {
            winner = player;
            Debug.Log(winner);
        }

        winnerHandInfoPairs.Item1.TryGetValue(winner, out (HandRank, CardRank[], List<Card>) winInfo);
        Debug.Log("Winning Hand is " + winInfo.Item3.ToString());

        // TEST
        if (isForceToFold)
        {
            ForceToFold();
            return;
        }

        ShowLeaderBoard();
    }
    
    private void ForceToFold()
    {
        Queue<AIPlayer> aIPlayers = new Queue<AIPlayer>();

        while (aIPlayers.Count >= 0)
        {
            if (aIPlayers.Count == 0)
            {
                foreach (var player in _playerList)
                {
                    if (player.GetType() == typeof(AIPlayer) && !player.IsFold)
                        aIPlayers.Enqueue((AIPlayer)player);
                }

                if (aIPlayers.Count <= 0)
                    break;
            }

            if (aIPlayers.TryDequeue(out AIPlayer aIPlayer))
                aIPlayer.ForceToFold();
        };

        ShowLeaderBoard();
    }

    private void ShowLeaderBoard()
    {
        Debug.Log("Lider Tablosu Görüntüleniyor...");

        int count = 1;

        while (_leaderBoardPlayerStack.Count > 0)
        {
            if (_leaderBoardPlayerStack.TryPop(out Player player))
            {
                Debug.Log($"{count}. oyuncu : {player.name}");
                count++;
            }
            else
            {
                break;
            }
        }
    }
}