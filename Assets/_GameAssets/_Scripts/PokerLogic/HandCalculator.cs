using System;
using System.Collections.Generic;

public static class HandCalculator
{
    // Returns HandRank, CardRanks for hand value, and the best hand.
    public static (HandRank, CardRank[], List<Card>) GetHandInfo(List<Card> hand)
    {
        if (IsHandRoyalFlush(hand, out List<Card> royalFlush, out CardRank[] royalFlushRank))
        {
            return (HandRank.RoyalFlush, royalFlushRank, royalFlush);
        }
        else if (IsHandStraightFlush(hand, out List<Card> straightFlush, out CardRank[] straightFlushRank))
        {
            return (HandRank.StraightFlush, straightFlushRank, straightFlush);
        }
        else if (IsHandFourOfAKind(hand, out List<Card> fourOfAKind, out CardRank[] fourOfAKindRank))
        {
            List<Card> bestHand = GetHandWithKickers(hand, fourOfAKind);
            return (HandRank.FourOfAKind, fourOfAKindRank, bestHand);
        }
        else if (IsHandFullHouse(hand, out List<Card> fullHouse, out CardRank[] fullHouseRanks))
        {
            return (HandRank.FullHouse, fullHouseRanks, fullHouse);
        }
        else if (IsHandFlush(hand, out List<Card> flush, out CardRank[] flushRank))
        {
            return (HandRank.Flush, flushRank, flush);
        }
        else if (IsHandStraight(hand, out List<Card> straight, out CardRank[] straightRank))
        {
            return (HandRank.Straight, straightRank, straight);
        }
        else if (IsHandThreeOfAKind(hand, out List<Card> threeOfAKind, out CardRank[] threeOfAKindRank))
        {
            List<Card> bestHand = GetHandWithKickers(hand, threeOfAKind);
            return (HandRank.ThreeOfAKind, threeOfAKindRank, bestHand);
        }
        else if (IsHandTwoPairs(hand, out List<Card> twoPairs, out CardRank[] twoPairsRanks))
        {
            List<Card> bestHand = GetHandWithKickers(hand, twoPairs);
            return (HandRank.TwoPairs, twoPairsRanks, bestHand);
        }
        else if (IsHandPair(hand, out List<Card> pairs, out CardRank[] pairRank))
        {
            List<Card> bestHand = GetHandWithKickers(hand, pairs);
            return (HandRank.Pair, pairRank, bestHand); 
        }
        else
        {
            List<Card> bestHand = GetTopFiveCards(hand);

            CardRank[] highestCardRank = new CardRank[1];
            highestCardRank[0] = bestHand[0].Value.Item2;

            return (HandRank.HighCard, highestCardRank, bestHand);
        }
    }

    public static Card GetCardWithRank(List<Card> hand, CardRank rank)
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

    private static bool IsHandRoyalFlush(List<Card> hand, out List<Card> royalFlush, out CardRank[] royalFlushRank)
    {
        royalFlush = null;
        royalFlushRank = new CardRank[1];

        if (IsHandStraight(hand, out List<Card> straightCards, out CardRank[] straightHandRank))
        {
            Card aceCard = GetCardWithRank(straightCards, CardRank.Ace);
            Card kingCard = GetCardWithRank(straightCards,CardRank.King);

            if (aceCard != null && kingCard != null)
            {
                if (IsHandFlush(straightCards, out List<Card> flushCards, out CardRank[] flushHandRank))
                {
                    royalFlush = flushCards;
                    royalFlushRank = new CardRank[1];
                    royalFlushRank[0] = CardRank.Ace;
                    return true;
                }
            }
        }
        return false;
    }

    private static bool IsHandStraightFlush(List<Card> hand, out List<Card> straightFlush, out CardRank[] straightFlushRank)
    {
        straightFlushRank = new CardRank[1];
        straightFlush = null;

        if (IsHandFlush(hand, out List<Card> flushCards, out CardRank[] flushHandRank))
        {
            if (IsHandStraight(flushCards, out List<Card> straightCards, out CardRank[] straightHandRank))
            {
                straightFlush = straightCards;
                straightFlushRank[0] = straightFlush[0].Value.Item2;
                return true;
            }
        }
        return false;
    }

    private static bool IsHandFourOfAKind(List<Card> hand, out List<Card> fourOfAKind, out CardRank[] fourOfAKindRank)
    {
        fourOfAKindRank = new CardRank[1];
        fourOfAKind = null;
        List<Card> tempHand = new List<Card>(hand);
        tempHand.Sort();
        int handCount = tempHand.Count;

        for (int i = 0; i < handCount; i++)
        {
            Card card = tempHand[0];
            List<Card> duplicateCards = GetDuplicateCards(tempHand, card.Value.Item2, 4);

            if (duplicateCards != null)
            {
                fourOfAKind = duplicateCards;
                fourOfAKindRank[0] = card.Value.Item2;
                return true;
            }
            else
            {
                tempHand.Remove(card);
            }
        }
        return false;
    }

    private static bool IsHandFullHouse(List<Card> hand, out List<Card> fullHouse, out CardRank[] fullHouseRanks)
    {
        fullHouseRanks = new CardRank[2];
        fullHouse = null;
        List<Card> tempHand = new List<Card>(hand);

        if (IsHandThreeOfAKind(tempHand, out List<Card> threeOfAKind, out CardRank[] threeOfaKindRank))
        {
            fullHouse = new List<Card>();
            fullHouseRanks[0] = threeOfaKindRank[0];

            foreach (Card card in threeOfAKind)
            {
                fullHouse.Add(card);
                tempHand.Remove(card);
            }

            if (IsHandPair(tempHand, out List<Card> pairs, out CardRank[] pairRank))
            {
                fullHouseRanks[1] = pairRank[0];

                foreach (Card card in pairs)
                {
                    fullHouse.Add(card);
                }

                fullHouse.Sort();
                return true;
            }
            else
            {
                fullHouse = null;
            }
        }

        return false;
    }

    private static bool IsHandFlush(List<Card> hand, out List<Card> flushCards, out CardRank[] flushRank)
    {
        flushRank = new CardRank[1];
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

        if (flushCards != null && flushCards.Count >= 5)
        {
            flushRank[0] = flushCards[0].Value.Item2;
            return true;
        }

        return false;
    }

    private static bool IsHandStraight(List<Card> hand, out List<Card> straightCards, out CardRank[] straightRank)
    {
        straightRank = new CardRank[1];
        straightCards = null;

        if (hand.Count < 5) { return false; }

        Card highestCard = GetHighCard(hand);

        if (highestCard.Value.Item2 == CardRank.Ace)
        {
            straightCards = GetFiveChainCards(hand, highestCard, true);

            if (straightCards != null)
            {
                straightCards.Reverse();
                straightRank[0] = straightCards[0].Value.Item2;
                return true;
            }
        }


        straightCards = GetFiveChainCards(hand, highestCard, false);

        if (straightCards != null)
        {
            straightRank[0] = straightCards[0].Value.Item2;
            return true;
        }

        return false;
    }

    private static bool IsHandThreeOfAKind(List<Card> hand, out List<Card> threeOfAKind, out CardRank[] threeOfAKindRank)
    {
        threeOfAKindRank = new CardRank[1];
        threeOfAKind = null;
        List<Card> tempHand = new List<Card>(hand);
        tempHand.Sort();
        int handCount = tempHand.Count;

        for (int i = 0; i < handCount; i++)
        {
            Card card = tempHand[0];
            List<Card> duplicateCards = GetDuplicateCards(tempHand, card.Value.Item2, 3);

            if (duplicateCards != null)
            {
                threeOfAKind = duplicateCards;
                threeOfAKindRank[0] = card.Value.Item2;
                return true;
            }
            else
            {
                tempHand.Remove(card);
            }
        }
        return false;
    }

    private static bool IsHandTwoPairs(List<Card> hand, out List<Card> twoPairs, out CardRank[] twoPairsRanks)
    {
        twoPairsRanks = new CardRank[2];
        twoPairs = null;
        List<Card> tempHand = new List<Card>(hand);

        if (IsHandPair(hand, out List<Card> pairs, out CardRank[] pairRank))
        {
            twoPairsRanks[0] = pairRank[0];
            twoPairs = new List<Card>();

            foreach(Card card in pairs)
            {
                twoPairs.Add(card);
                tempHand.Remove(card);
            }

            if (IsHandPair(tempHand, out List<Card> anotherPairs, out CardRank[] secondPairRank))
            {
                twoPairsRanks[1] = secondPairRank[0];
                foreach(Card card in anotherPairs)
                {
                    twoPairs.Add(card);
                }

                return true;
            }
        }

        return false;
    }

    private static bool IsHandPair(List<Card> hand, out List<Card> pairs, out CardRank[] pairRank)
    {
        pairRank = new CardRank[1];
        pairs = null;
        hand.Sort();

        foreach (Card card in hand)
        {
            pairs = GetDuplicateCards(hand, card.Value.Item2, 2);

            if (pairs != null)
            {
                pairRank[0] = card.Value.Item2;
                return true;
            }
        }

        return false;
    }

    private static List<Card> GetHandWithKickers(List<Card> hand, List<Card> handWithValue)
    {
        List<Card> tempHand = new List<Card>(hand);
        int numberOfCardsToAdd = 5 - handWithValue.Count;
        
        foreach(Card card in handWithValue)
        {
            tempHand.Remove(card);
        }
        tempHand.Sort();

        for (int i = 0; i < numberOfCardsToAdd; i++)
        {
            handWithValue.Add(tempHand[i]);
        }

        return handWithValue;
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
                hand.RemoveAt(5);
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

    

    private static int GetCardRankCount()
    {
        return Enum.GetNames(typeof(CardRank)).Length;
    }
}