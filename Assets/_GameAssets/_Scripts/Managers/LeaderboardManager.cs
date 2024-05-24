using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardManager
{
    public List<Player> Leaderboard = new List<Player>();

    private List<Player> _wonPlayers = new List<Player>();
    private List<Player> _winnersPlayerList = new List<Player>();
    private List<Player> _foldList = new List<Player>();
 
    public void AddWonPlayer(Player player)
    {
        _wonPlayers.Add(player);
    }

    public void AddFoldPlayer(Player player)
    {
        _foldList.Add(player);
    }

    public List<Player> GetFoldList()
    {
        return _foldList;
    }
    public List<Player> GetWonPlayer()
    {
        return _wonPlayers;
    }

    public List<Player> GetWinnersPlayerList()
    {
        _winnersPlayerList.Clear();

        foreach (var player in GameManager.Instance.Players)
        {
            if (!_foldList.Contains(player))
            {
                _winnersPlayerList.Add(player);
            }
        }
        return _winnersPlayerList;
    }

    public void ShowLeaderboard()
    {
       Leaderboard.Clear();

        foreach (var player in _wonPlayers)
        {
            Leaderboard.Add(player);
        }

        foreach (var player in _winnersPlayerList)
        {
            Leaderboard.Add(player);
        }
        
        foreach (var player in _foldList)
        {
            Leaderboard.Add(player);
        }

        Debug.Log("----------Leaderboard-----------");
        foreach (var item in Leaderboard)
        {
            Debug.Log(item);
        }
        Debug.Log("--------------------------------");
    }
}