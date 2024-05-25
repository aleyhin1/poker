using System.Collections.Generic;

public class HandInfo
{
    public HandRank HandRank { get; private set; }
    public CardRank[] HandRankCards { get; private set; }
    public List<Card> BestHand { get; private set; }

    public HandInfo(HandRank handRank, CardRank[] handRankCards, List<Card> bestHand)
    {
        HandRank = handRank;
        HandRankCards = handRankCards;
        BestHand = bestHand;
    }
}
