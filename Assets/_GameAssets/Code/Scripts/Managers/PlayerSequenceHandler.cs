using System;
using System.Collections.Generic;

public class PlayerSequenceHandler
{
    public Action NextPlayerAction;

    private List<PlayerController> _playerList;
    private Queue<PlayerController> _playersQueue;
    private PlayerController _currentPlayer;

    public PlayerSequenceHandler(List<PlayerController> playerList)
    {
        _playersQueue = new Queue<PlayerController>();
        _playerList = playerList;
        InitializePlayerList();
        NextPlayerAction += NextPlayer;
    }

    public void OnDisable()
    {
        NextPlayerAction -= NextPlayer;
    }

    private void InitializePlayerList()
    {
        if (_playerList == null || _playerList.Count <= 0)
            return;

        foreach (PlayerController player in _playerList)
        {
            _playersQueue.Enqueue(player);
        }

        _currentPlayer = _playersQueue.Peek();
        _currentPlayer.IsMyTurn = true;
    }

    private void NextPlayer()
    {
        _currentPlayer = _playersQueue.Dequeue();
        _currentPlayer.IsMyTurn = false;

        if (_playersQueue.Count > 0)
            _playersQueue.Peek().IsMyTurn = true;
        
        else
            InitializePlayerList();
    }
}