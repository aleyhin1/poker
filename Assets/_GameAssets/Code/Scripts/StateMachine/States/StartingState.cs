using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingState : IPokerState
{
    /* TODO
     * Küçük bahisi oynamayan oyuncu belirlenecek 
     * Küçük bahis oynanacak
     * Küçük bahisi oyunayan oyuncudan sonraki oyuncu büyük bahisi oynayacak
     */
    
    private Player _player;
    private Player _nextPlayer;
    private int _playerIndex;
    private int _nextPlayerIndex;

    public StartingState(List<Player> players)
    {
        SetPlayers(players);
    }
     
    private void SetPlayers(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].IsSmallBlindPaid)
            {
                _player = players[i];
                _playerIndex = i;
                break;
            }

            if (i >= players.Count - 1)
            {
                foreach (var player in players)
                    player.IsSmallBlindPaid = false;

                _player = players[0];
                _playerIndex = 0;
                break;
            }
        }

        _nextPlayerIndex = _playerIndex + 1;

        if (_nextPlayerIndex >= players.Count)
            _nextPlayerIndex = 0;

        _nextPlayer = players[_nextPlayerIndex];
    }

    public void EnterState()
    {
        var betAmount = GameManager.Instance.SmallBlindBet;
        _player.Bet(betAmount);
        _player.IsSmallBlindPaid = true;

        _nextPlayer.IsBigBlind = true;

        GameManager.Instance.SetPlayerQueue(_nextPlayerIndex);

        Debug.Log(_player + " : " + betAmount);
        Debug.Log("next Player : " + _nextPlayer);

    }

    public void UpdateState()
    {
         
    }

    public void ExitState()
    {
         
    }
}