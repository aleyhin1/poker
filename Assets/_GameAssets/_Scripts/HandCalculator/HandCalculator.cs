using System;
using System.Collections.Generic;

public static class HandCalculator
{
    //public static (HandRank, CardRank) GetHandValue(List<Card> hand)
    //{
    //    else if ()
    //    {

    //    }

    //    Card highCard = GetHighCard(hand);
    //    return (HandRank.HighCard, highCard.Value.Item2);
    //}

    public static bool IsHandStraight(List<Card> hand, out List<Card> straightCards)
    {
        straightCards = null;

        if (hand.Count < 5) { return false; }

        Card highestCard = GetHighCard(hand);

        if (highestCard.Value.Item2 == CardRank.Ace)
        {
            straightCards = GetFiveChainCards(hand, highestCard, true);

            if (straightCards != null)
            {
                straightCards.Reverse();
                return true;
            }
        }


        straightCards = GetFiveChainCards(hand, highestCard, false);

        if (straightCards != null)
        {
            return true;
        }

        return false;
    }

    public static bool IsHandFlush(List<Card> hand, out List<Card> flushCards)
    {
        flushCards = null;

        if (hand.Count < 5) { return false; }

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

        flushCards = new List<Card>();
        int maxCardCount = 0;

        foreach (Tuple<CardSuit, List<Card>> suitAndCards in _suitCardsPairs)
        {
            if (suitAndCards.Item2.Count > maxCardCount)
            {
                flushCards = suitAndCards.Item2;
                maxCardCount = suitAndCards.Item2.Count;
            }
        }

        flushCards = GetTopFiveCards(flushCards);

        if (flushCards.Count >= 5)
        {
            return true;
        }

        return false;
    }

    public static bool IsHandPair(List<Card> hand, out List<Card> pairs)
    {
        pairs = null;
        hand.Sort();

        foreach (Card card in hand)
        {
            pairs = GetDuplicateCards(hand, card.Value.Item2, 2);

            if (pairs != null)
            {
                return true;
            }
        }

        return false;
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

    private static List<Card> GetDuplicateCards(List<Card> hand, CardRank rank, int count)
    {
        List<Card> duplicates = new List<Card>();
        List<Card> tempHand = new List<Card>(hand);

        for (int i = 0; i < count; i++)
        {
            Card duplicate = GetCardWithRank(tempHand, rank);
            if (duplicate != null)
            {
                duplicates.Add(duplicate);
                tempHand.Remove(duplicate);
            }
        }

        if (duplicates.Count == count)
        {
            return duplicates;
        }

        return null;
    }

    private static Card GetHighCard(List<Card> hand)
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