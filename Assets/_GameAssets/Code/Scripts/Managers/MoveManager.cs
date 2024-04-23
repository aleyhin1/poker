using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>, IPokerMoves
{

    public void SmallBlindBet(Player player)
    {
        Debug.Log("Small Blind Player : " + player + " Bet : " );
        GameManager.Instance.NextPlayer();
    }

    public void BigBlidBet(Player player)
    {
        var minBet = GameManager.Instance.MinBet;
        var newBetValue = minBet * 2;
        GameManager.Instance.MinBet = newBetValue;

        Debug.Log("Big Blind Player : " + player + " Bet : " + newBetValue);

        GameManager.Instance.GetPokerStateManager.EnterDealingCardsState();
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
        throw new System.NotImplementedException();
    }

    public void Raise(Player player, int raiseAmount)
    {
        throw new System.NotImplementedException();
    }
}