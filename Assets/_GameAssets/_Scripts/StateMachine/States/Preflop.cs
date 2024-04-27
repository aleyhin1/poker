using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preflop : IPokerState
{
    private int _bigBlindPlayerIndex;

    public void EnterState()
    {
        Debug.Log("-Preflop-");

        List<Player> players = GameManager.Instance.Players;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsBigBlindPaid)
            {
                int playerIndex = i;
                playerIndex++;

                if (playerIndex < players.Count)
                    _bigBlindPlayerIndex = playerIndex;
                else
                    _bigBlindPlayerIndex = 0;
                break;
            }
        }
        GameManager.Instance.SetPlayerQueue(_bigBlindPlayerIndex);
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
   
    }
}