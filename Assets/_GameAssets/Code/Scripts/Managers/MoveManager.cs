using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>, IPokerMoves
{
    [SerializeField] private float _decisionDuration = 1f;

    public IEnumerator BlidBet(Player player)
    {
        var minBet = GameManager.Instance.MinBet;
        var newBetValue = minBet * 2;
        GameManager.Instance.MinBet = newBetValue;

        yield return new WaitForSeconds(_decisionDuration);

        Debug.Log("Blind Player : " + player + " Bet : " + newBetValue);
        player.IsBigBlind = false;
 
        GameManager.Instance.NextState();
    }

    public void Bet(Player player, int betAmount)
    {
        Debug.Log("Player : " + player + " Bet : " + betAmount);
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