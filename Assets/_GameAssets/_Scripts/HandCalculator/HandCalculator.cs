using System;
using System.Collections.Generic;

public static class HandCalculator
{
    public static List<Card> GetStraightCards(List<Card> hand)
    {
        if (hand.Count < 5) return null;

        Card highestCard = GetHighestCard(hand);

        if (highestCard.Value.Item2 == CardRank.Ace)
        {
            List<Card> chainDown = GetFiveChainCards(hand, highestCard, true);

            if (chainDown != null)
            {
                chainDown.Reverse();
                return chainDown;
            }
        }

        List<Card> chain = GetFiveChainCards(hand, highestCard, false);
        return chain;
    }

    public static List<Card> GetFlushCards(List<Card> hand)
    {
        if (hand.Count < 5) return null;

        Tuple<CardSuit, List<Card>> _clubCards = new Tuple<CardSuit, List<Card>>(CardSuit.Club, new List<Card>());
        Tuple<CardSuit, List<Card>> _diamondCards = new Tuple<CardSuit, List<Card>>(CardSuit.Diamond, new List<Card>());
        Tuple<CardSuit, List<Card>> _heartCards = new Tuple<CardSuit, List<Card>>(CardSuit.Heart, new List<Card>());
        Tuple<CardSuit, List<Card>> _spadeCards = new Tuple<CardSuit, List<Card>>(CardSuit.Spade, new List<Card>());

        Tuple<CardSuit, List<Card>>[] _suitCardsPairs = new Tuple<CardSuit, List<Card>>[4] { _clubCards, _diamondCards, _heartCards, _spadeCards }; 

        foreach (Card card in hand)
        {
            switch (card.Value.Item1)
            {
                case CardSuit.Club:
                    _clubCards.Item2.Add(card);
                    break;
                case CardSuit.Diamond:
                    _diamondCards.Item2.Add(card);
                    break;
                case CardSuit.Heart:
                    _heartCards.Item2.Add(card);
                    break;
                case CardSuit.Spade:
                    _spadeCards.Item2.Add(card);
                    break;
            }
        }

        List<Card> flushHand = new List<Card>();
        int maxCardCount = 0;

        foreach (Tuple<CardSuit, List<Card>> suitAndCards in _suitCardsPairs)
        {
            if (suitAndCards.Item2.Count > maxCardCount)
            {
                flushHand = suitAndCards.Item2;
                maxCardCount = suitAndCards.Item2.Count;
            }
        }

        return GetTopFiveCards(flushHand);
    }

    private static List<Card> GetFiveChainCards(List<Card> hand, Card pivotCard, bool isIncreasing)
    {
        int increment = isIncreasing ? -1 : 1;

        CardRank cardToLook = pivotCard.Value.Item2;
        List<Card> cardChain = new List<Card>();

        for (int i = 0; i < hand.Count; i++)
        {
            Card nextCard = GetCardWithRank(hand, cardToLook);

            if (nextCard == null)
            {
                cardChain.Clear();
            }
            else
            {
                cardChain.Add(nextCard);
            }


            if (cardChain.Count == 5)
            {
                return cardChain;
            }

            cardToLook = (CardRank) Util.Modulo((int)cardToLook + increment, GetCardRankCount());
        }

        return null;
    }

    private static List<Card> GetTopFiveCards(List<Card> hand)
    {
        hand.Sort();

        switch (hand.Count)
        {
            case 5:
                return hand;
            case 6:
                hand.RemoveAt(5);
                return hand;
            case 7:
                hand.RemoveAt(6);
                hand.RemoveAt(7);
                return hand;
        }

        return null;
    }

    private static Card GetHighestCard(List<Card> hand)
    {
        List<Card> tempHand = new List<Card>(hand);
        tempHand.Sort();

        return tempHand[0];
    }

    private static Card GetCardWithRank(List<Card> hand, CardRank rank)
    {
        foreach (Card card in hand)
        {
            if (card.Value.Item2 == rank)
            {
                return card;
            }
        }
        return null;
    }

    private static int GetCardRankCount()
    {
        return Enum.GetNames(typeof(CardRank)).Length;
    }
}