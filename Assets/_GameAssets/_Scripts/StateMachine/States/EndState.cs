using UnityEngine;
using System.Collections.Generic;

public class EndState : IPokerState
{
    public static bool isForceToFold { get; set; }
    private List<Player> _playerList;

    public void EnterState()
    {
        Debug.Log("-------EndState--------");
        GameManager.Instance.DealerController.CollectBets();

        Debug.Log("oyuncularýn kartlarý hesaplanýyor..");

        _playerList = GameManager.Instance.LeaderboardManager.GetWinnersPlayerList();

        if (_playerList.Count > 1)
        {
            foreach (Player player in _playerList)
            {
                player.Cards[0].FaceUp();
                player.Cards[1].FaceUp();
            }

            GetWinnersAndShowOnUI();
        }
        else
        {
            PokerUIManager.Instance.LowerCurtain();
            _playerList[0].ShowWinBox(true);
        }

        Debug.Log("-------Winners--------");

        foreach (var player in _playerList)
        {
            Debug.Log($"Winners {player}");
        }

        Debug.Log("-------Losers--------");
        foreach (var player in GameManager.Instance.LeaderboardManager.GetFoldList())
        {
            Debug.Log($"Losers {player}");
        }
        Debug.Log("---------------");
    }

    private void GetWinnersAndShowOnUI()
    {
        PokerUIManager.Instance.LowerCurtain();
        (Dictionary<Player, (HandRank, CardRank[], List<Card>)>, CardRank?) winnerHandInfo = HandComparer.GetWinnerHands(_playerList, GameManager.Instance.CardsOnTheTable);
        ShowWinners(winnerHandInfo);
        PokerCanvas.Instance.ShowWinInfo(winnerHandInfo);
    }

    private void ShowWinners((Dictionary<Player, (HandRank, CardRank[], List<Card>)>, CardRank?) winnerHandInfo)
    {
        foreach(Player player in winnerHandInfo.Item1.Keys)
        {
            player.ShowWinBox(true);
        }
    }
}