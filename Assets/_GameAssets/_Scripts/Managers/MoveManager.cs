using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>
{
    public void SmallBlindBet(Player player , int betAmount)
    {
        Debug.Log("Small Blind Player : " + player.name + " Bet : " + betAmount);

        player.TotalMoney -= betAmount;
        GameManager.Instance.NextPlayer();
    }

    public void BigBlindBet(Player player)
    {
        int minBet = GameManager.Instance.MinBet;
        int newBetValue = minBet * 2;

        Debug.Log("Big Blind Player : " + player.name + " Bet : " + newBetValue);

        player.TotalMoney -= newBetValue;
        GameManager.Instance.MinBet = newBetValue;
        PokerStateManager.Instance.EnterDealingCardsState();
    }

    public void Bet(Player player, int betAmount)
    {
        Debug.Log(player.name + " Bet : " + betAmount);

        player.TotalMoney -= betAmount;
        GameManager.Instance.NextPlayer();
    }

    public void Bob(Player player)
    {

    }

    public void Call(Player player , int minBet)
    {
        Debug.Log(player.name + " : Call : " + minBet);

        player.TotalMoney -= minBet;
        player.IsCall = true;
        GameManager.Instance.NextPlayer();
    }

    public void Fold(Player player)
    {
        Debug.Log(player.name + " : Fold");

        player.IsFold = true;
        GameManager.Instance.NextPlayer();
    }

    public void Raise(Player player, int raiseAmount)
    {
        Debug.Log(player.name + " : Raise :" + raiseAmount);

        player.TotalMoney -= raiseAmount;

        GameManager.Instance.MinBet = raiseAmount;
        player.IsRaise = true;
        GameManager.Instance.NextPlayer();
    }
}