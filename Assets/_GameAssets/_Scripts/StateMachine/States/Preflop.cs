using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preflop : IPokerState
{
    public void EnterState()
    {
        Debug.Log("-Preflop-");

        List<Player> players = GameManager.Instance.Players;
        Queue<Player> playerQueue = new Queue<Player>();

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsBigBlindPaid)
            {
                int playerIndex = i;
                 
                for (int j = 0; j < players.Count; j++)
                {
                    playerIndex++;

                    if (playerIndex >= players.Count)
                        playerIndex = 0;

                    playerQueue.Enqueue(players[playerIndex]);
                }
                GameManager.Instance.PlayerSequenceHandler.SetPlayers(playerQueue);
                break;
            }
        }
    }

    public void UpdateState()
    {
        
    }

    public void ExitState()
    {
   
    }
}