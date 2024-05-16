using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HandComparer
{
    /// <summary>
    /// Returns winning player, best hand rank, cardranks that makes up handrank, and kicker card if it exist.
    /// For example: 'Player1' won, winner hand is 'two pairs' with '7' and '4' and with a kicker card of 'A'.
    /// </summary>

    public static (Dictionary<Player, (HandRank, CardRank[], List<Card>)>, CardRank?) GetWinnerHands(List<Player> players, List<Card> sharedCards)
    {
        EliminateByHandRank(players, sharedCards, out var winnersByHandRank);

        if (winnersByHandRank.Count == 1)
        {
            return (winnersByHandRank, null);
        }

        EliminateByCardRank(winnersByHandRank, out Dictionary<Player, (HandRank, CardRank[], List<Card>)> winnersByCardRank);

        if (winnersByCardRank.Count == 1)
        {
            return (winnersByCardRank, null);
        }

        EliminateByCardValue(winnersByCardRank, out Dictionary<Player, (HandRank, CardRank[], List<Card>)> winnersByCardValue, out CardRank kickerCard);

        return (winnersByCardValue, kickerCard);
    }

    private static void EliminateByHandRank(List<Player> players, List<Card> sharedCards,
        out Dictionary<Player, (HandRank, CardRank[], List<Card>)> winnersByHandRank)
    {
        Dictionary<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPairs = GetPlayerHandInfoPairs(players, sharedCards);
        HandRank winnerHandRank = GetWinnerHandRank(playerHandInfoPairs);
        winnersByHandRank = GetHandsWithHandRank(playerHandInfoPairs, winnerHandRank);
    }

    private static void EliminateByCardRank(Dictionary<Player, (HandRank, CardRank[], List<Card>)> playersToEliminate, 
        out Dictionary<Player, (HandRank, CardRank[], List<Card>)> winnerList)
    {
        winnerList = playersToEliminate;

        Player anyPlayer = playersToEliminate.Keys.FirstOrDefault();
        playersToEliminate.TryGetValue(anyPlayer, out (HandRank, CardRank[], List<Card>) handInfo);
        int cardRankArrayCount = handInfo.Item2.Length;

        CardRank[] winnerCardRankArray = new CardRank[cardRankArrayCount];

        for (int i = 0; i < cardRankArrayCount; i++)
        {
            CardRank winnerCardRank = GetWinnerCardRank(winnerList, i);
            Dictionary<Player, (HandRank, CardRank[], List<Card>)> winnersByCardRank = GetHandsWithCardRank(winnerList, winnerCardRank, i);

            winnerCardRankArray[i] = winnerCardRank;

            if (winnersByCardRank.Count == 1)
            {
                return;
            }

            winnerList = winnersByCardRank;
        }
    }

    private static void EliminateByCardValue(Dictionary<Player, (HandRank, CardRank[], List<Card>)> playersToEliminate,
        out Dictionary<Player, (HandRank, CardRank[], List<Card>)> winnerList, out CardRank kickerCard)
    {
        winnerList = playersToEliminate;
        kickerCard = CardRank.Ace;
        List<Player> playersWithoutIndexedCard = new List<Player>();

        for (int i = 0; i < 13; i++)
        {
            playersWithoutIndexedCard.Clear();

            foreach (KeyValuePair<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPair in winnerList)
            {
                if (HandCalculator.GetCardWithRank(playerHandInfoPair.Value.Item3, kickerCard) == null)
                {
                    playersWithoutIndexedCard.Add(playerHandInfoPair.Key);
                }
            }

            
            if (winnerList.Count > playersWithoutIndexedCard.Count && winnerList.Count > 1)
            {
                foreach(Player player in playersWithoutIndexedCard)
                {
                    winnerList.Remove(player);
                }
            }
            else if (winnerList.Count == 1)
            {
                return;
            }

            kickerCard++;
        }
    }

    private static Dictionary<Player, (HandRank, CardRank[], List<Card>)> GetPlayerHandInfoPairs(List<Player> players, List<Card> sharedCards)
    {
        List<Card> playerHand = new List<Card>();
        Dictionary<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPairs = new Dictionary<Player, (HandRank, CardRank[], List<Card>)>();

        foreach (Player player in players)
        {
            playerHand.AddRange(player.Cards);
            playerHand.AddRange(sharedCards);

            (HandRank, CardRank[], List<Card>) handInfo = HandCalculator.GetHandInfo(playerHand);

            playerHandInfoPairs.Add(player, handInfo);

            playerHand.Clear();
        }

        return playerHandInfoPairs;
    }

    private static HandRank GetWinnerHandRank(Dictionary<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPairs)
    {
        HandRank winnerRank = HandRank.HighCard;

        foreach ((HandRank, CardRank[], List<Card>) handInfo in playerHandInfoPairs.Values)
        {
            if (handInfo.Item1 < winnerRank)
            {
                winnerRank = handInfo.Item1;
            }
        }

        return winnerRank;
    }

    private static Dictionary<Player, (HandRank, CardRank[], List<Card>)> GetHandsWithHandRank
        (Dictionary<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPairs, HandRank rank)
    {
        Dictionary<Player, (HandRank, CardRank[], List<Card>)> handsWithRank = new Dictionary<Player, (HandRank, CardRank[], List<Card>)>();

        foreach(KeyValuePair<Player, (HandRank, CardRank[], List<Card>)> playerHandValuePair in playerHandInfoPairs)
        {
            if (playerHandValuePair.Value.Item1 == rank)
            {
                handsWithRank.Add(playerHandValuePair.Key, playerHandValuePair.Value);
            }
        }

        return handsWithRank;
    }

    private static CardRank GetWinnerCardRank(Dictionary<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPairs, int rankIndex)
    {
        CardRank winnerCardRank = CardRank.Two;

        foreach ((HandRank, CardRank[], List<Card>) handInfo in playerHandInfoPairs.Values)
        {
            if (handInfo.Item2[rankIndex] < winnerCardRank)
            {
                winnerCardRank = handInfo.Item2[rankIndex];
            }
        }

        return winnerCardRank;
    }

    private static Dictionary<Player, (HandRank, CardRank[], List<Card>)> GetHandsWithCardRank
        (Dictionary<Player, (HandRank, CardRank[], List<Card>)> playerHandInfoPairs, CardRank rank, int rankIndex)
    {
        Dictionary<Player, (HandRank, CardRank[], List<Card>)> handsWithCardRank = new Dictionary<Player, (HandRank, CardRank[], List<Card>)>();

        foreach (KeyValuePair<Player, (HandRank, CardRank[], List<Card>)> playerHandValuePair in playerHandInfoPairs)
        {
            if (playerHandValuePair.Value.Item2[rankIndex] == rank)
            {
                handsWithCardRank.Add(playerHandValuePair.Key, playerHandValuePair.Value);
            }
        }

        return handsWithCardRank;
    }
}
