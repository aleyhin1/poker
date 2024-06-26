using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;

public class EndState : IPokerState
{
    private List<Player> _playerList = new List<Player>();

    public void EnterState()
    {
        Debug.Log("-------EndState--------");
        GameManager.Instance.DealerController.CollectBets();

        Debug.Log("oyuncuların kartları hesaplanıyor..");

        foreach (var player in GameManager.Instance.Players)
        {
            if (!player.IsFold)
            {
                _playerList.Add(player);        
            }
        }

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
 
        GameManager.Instance.DealerController.PayingOut(_playerList);

        SaveManager.Instance.SaveMoney();
        GameManager.Instance.NewGame();
    }

    private void GetWinnersAndShowOnUI()
    {
        PokerUIManager.Instance.LowerCurtain();
        (Dictionary<Player, HandInfo>, CardRank?) winnerHandInfo = HandComparer.GetWinnerHands(_playerList, GameManager.Instance.CardsOnTheTable);
        ShowWinners(winnerHandInfo);
        PokerCanvas.Instance.ShowWinInfo(winnerHandInfo);

        if (winnerHandInfo.Item1.Keys.Count > 0)
        {
            _playerList.Clear();
            foreach (var item in winnerHandInfo.Item1.Keys)
            {
                _playerList.Add(item);
            }
        }
    }

    private void ShowWinners((Dictionary<Player, HandInfo>, CardRank?) winnerHandInfo)
    {
        foreach(Player player in winnerHandInfo.Item1.Keys)
        {
            player.ShowWinBox(true);
        }
    }
}