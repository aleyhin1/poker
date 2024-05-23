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

        
        ShowLeaderBoard();
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

    private void ShowLeaderBoard()
    {
        //Debug.Log("Lider Tablosu Görüntüleniyor...");

        //int count = 1;

        //while (_leaderBoardPlayerStack.Count > 0)
        //{
        //    if (_leaderBoardPlayerStack.TryPop(out Player player))
        //    {
        //        Debug.Log($"{count}. oyuncu : {player.name}");
        //        count++;
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}
    }
}