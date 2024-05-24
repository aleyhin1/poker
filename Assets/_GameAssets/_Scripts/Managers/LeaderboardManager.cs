using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardManager
{
    public List<Player> Leaderboard = new List<Player>();
 
    private List<Player> _winnersPlayerList = new List<Player>();
    private List<Player> _foldList = new List<Player>();
 
    public void AddFoldPlayer(Player player)
    {
        _foldList.Add(player);
    }

    public List<Player> GetFoldList()
    {
        return _foldList;
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

    public void ShowLeaderboard(List<Player> winners , List<Player> losers)
    {
        foreach (var winnersPlayer in winners)
        {
            Leaderboard.Add(winnersPlayer);
        }

        foreach (var losersPlayer in losers)
        {
            Leaderboard.Add(losersPlayer);
        }
    }
}