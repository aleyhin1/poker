using System.Collections.Generic;
using UnityEngine;

public class StartingState : IPokerState
{
    private Player _player;
    private Player _nextPlayer;
    private int _playerIndex;
    private int _nextPlayerIndex;

    public StartingState(List<Player> players)
    {  
        SetPlayers(players);
    }

    public void EnterState()
    {
        Debug.Log("-StartingState-");

        UIManager.Instance.HigherCurtain();

        _player.IsSmallBlind = true;
        _nextPlayer.IsBigBlind = true;

        Queue<Player> playerQueue = new Queue<Player>();

        playerQueue.Enqueue(_player);
        playerQueue.Enqueue(_nextPlayer);

        GameManager.Instance.PlayerSequenceHandler.SetPlayers(playerQueue);
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
}