using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>
{
    public void SmallBlindBet(Player player , int bet)
    {
        Debug.Log("Small Blind Player : " + player + " Bet : " + bet);
        GameManager.Instance.NextPlayer();
    }

    public void BigBlindBet(Player player)
    {
        int minBet = GameManager.Instance.MinBet;
        int newBetValue = minBet * 2;

        GameManager.Instance.MinBet = newBetValue;

        Debug.Log("Big Blind Player : " + player + " Bet : " + newBetValue);

        PokerStateManager.Instance.EnterDealingCardsState();
    }

    public void Bet(Player player, int betAmount)
    {
        Debug.Log("Normal : " + player + " Bet : " + betAmount);

        GameManager.Instance.NextPlayer();

    }

    public void Bob(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Call(Player player)
    {
        throw new System.NotImplementedException();
    }

    public void Fold(Player player)
    {
        player.IsFold = true;
        GameManager.Instance.NextPlayer();
    }

    public void Raise(Player player, int raiseAmount)
    {
        throw new System.NotImplementedException();
    }
}