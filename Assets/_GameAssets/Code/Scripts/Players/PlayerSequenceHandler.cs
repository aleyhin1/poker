using System;
using System.Collections.Generic;

public class PlayerSequenceHandler
{
    public Action NextPlayerAction;

    private List<Player> _playerList;
    private Queue<Player> _playersQueue;
    private Player _currentPlayer;

    public PlayerSequenceHandler(List<Player> playerList)
    {
        _playersQueue = new Queue<Player>();
        _playerList = playerList;
        InitializePlayerQueue();
        NextPlayerAction += NextPlayer;
    }

    public void OnDisable()
    {
        NextPlayerAction -= NextPlayer; 
    }

    private void InitializePlayerQueue()
    {
        if (_playerList == null || _playerList.Count <= 0)
            return;

        foreach (Player player in _playerList)
            _playersQueue.Enqueue(player);

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
            InitializePlayerQueue();
    }
}