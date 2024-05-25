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

    public static (Dictionary<Player, HandInfo>, CardRank?) GetWinnerHands(List<Player> players, List<Card> sharedCards)
    {
        EliminateByHandRank(players, sharedCards, out var winnersByHandRank);

        if (winnersByHandRank.Count == 1)
        {
            return (winnersByHandRank, null);
        }

        EliminateByCardRank(winnersByHandRank, out Dictionary<Player, HandInfo> winnersByCardRank);

        if (winnersByCardRank.Count == 1)
        {
            return (winnersByCardRank, null);
        }

        EliminateByCardValue(winnersByCardRank, out Dictionary<Player, HandInfo> winnersByCardValue, out CardRank? kickerCard);

        return (winnersByCardValue, kickerCard);
    }

    private static void EliminateByHandRank(List<Player> players, List<Card> sharedCards,
        out Dictionary<Player, HandInfo> winnersByHandRank)
    {
        Dictionary<Player, HandInfo> playerHandInfoPairs = GetPlayerHandInfoPairs(players, sharedCards);
        HandRank winnerHandRank = GetWinnerHandRank(playerHandInfoPairs);
        winnersByHandRank = GetHandsWithHandRank(playerHandInfoPairs, winnerHandRank);
    }

    private static void EliminateByCardRank(Dictionary<Player, HandInfo> playersToEliminate, out Dictionary<Player, HandInfo> winnerList)
    {
        winnerList = playersToEliminate;

        Player anyPlayer = playersToEliminate.Keys.FirstOrDefault();
        playersToEliminate.TryGetValue(anyPlayer, out HandInfo handInfo);
        int cardRankArrayCount = handInfo.HandRankCards.Length;

        CardRank[] winnerCardRankArray = new CardRank[cardRankArrayCount];

        for (int i = 0; i < cardRankArrayCount; i++)
        {
            CardRank winnerCardRank = GetWinnerCardRank(winnerList, i);
            winnerList = GetHandsWithCardRank(winnerList, winnerCardRank, i);

            winnerCardRankArray[i] = winnerCardRank;

            if (winnerList.Count == 1)
            {
                return;
            }
        }
    }

    private static void EliminateByCardValue(Dictionary<Player, HandInfo> playersToEliminate, out Dictionary<Player, HandInfo> winnerList, out CardRank? kickerCard)
    {
        winnerList = playersToEliminate;
        kickerCard = CardRank.Ace;
        List<Player> playersWithoutIndexedCard = new List<Player>();

        for (int i = 0; i < 13; i++)
        {
            playersWithoutIndexedCard.Clear();

            foreach (KeyValuePair<Player, HandInfo> playerHandInfoPair in winnerList)
            {
                if (HandCalculator.GetCardWithRank(playerHandInfoPair.Value.BestHand, kickerCard.Value) == null)
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
            if (winnerList.Count == 1)
            {
                return;
            }

            kickerCard++;
        }
        if ((int)kickerCard == 13)
        {
            kickerCard = null;
        }
    }

    private static Dictionary<Player, HandInfo> GetPlayerHandInfoPairs(List<Player> players, List<Card> sharedCards)
    {
        List<Card> playerHand = new List<Card>();
        Dictionary<Player, HandInfo> playerHandInfoPairs = new Dictionary<Player, HandInfo>();

        foreach (Player player in players)
        {
            playerHand.AddRange(player.Cards);
            playerHand.AddRange(sharedCards);

            HandInfo handInfo = HandCalculator.GetHandInfo(playerHand);

            playerHandInfoPairs.Add(player, handInfo);

            playerHand.Clear();
        }

        return playerHandInfoPairs;
    }

    private static HandRank GetWinnerHandRank(Dictionary<Player, HandInfo> playerHandInfoPairs)
    {
        HandRank winnerRank = HandRank.HighCard;

        foreach (HandInfo handInfo in playerHandInfoPairs.Values)
        {
            if (handInfo.HandRank < winnerRank)
            {
                winnerRank = handInfo.HandRank;
            }
        }

        return winnerRank;
    }

    private static Dictionary<Player, HandInfo> GetHandsWithHandRank(Dictionary<Player, HandInfo> playerHandInfoPairs, HandRank rank)
    {
        Dictionary<Player, HandInfo> handsWithRank = new Dictionary<Player, HandInfo>();

        foreach(KeyValuePair<Player, HandInfo> playerHandValuePair in playerHandInfoPairs)
        {
            if (playerHandValuePair.Value.HandRank == rank)
            {
                handsWithRank.Add(playerHandValuePair.Key, playerHandValuePair.Value);
            }
        }

        return handsWithRank;
    }

    private static CardRank GetWinnerCardRank(Dictionary<Player, HandInfo> playerHandInfoPairs, int rankIndex)
    {
        CardRank winnerCardRank = CardRank.Two;

        foreach (HandInfo handInfo in playerHandInfoPairs.Values)
        {
            if (handInfo.HandRankCards[rankIndex] < winnerCardRank)
            {
                winnerCardRank = handInfo.HandRankCards[rankIndex];
            }
        }

        return winnerCardRank;
    }

    private static Dictionary<Player, HandInfo> GetHandsWithCardRank(Dictionary<Player, HandInfo> playerHandInfoPairs, CardRank rank, int rankIndex)
    {
        Dictionary<Player, HandInfo> handsWithCardRank = new Dictionary<Player, HandInfo>();

        foreach (KeyValuePair<Player, HandInfo> playerHandValuePair in playerHandInfoPairs)
        {
            if (playerHandValuePair.Value.HandRankCards[rankIndex] == rank)
            {
                handsWithCardRank.Add(playerHandValuePair.Key, playerHandValuePair.Value);
            }
        }

        return handsWithCardRank;
    }
}
