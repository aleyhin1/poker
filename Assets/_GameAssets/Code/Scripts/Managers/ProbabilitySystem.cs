using UnityEngine;

public static class ProbabilitySystem
{
    public static int SetBetRate(int totalMoney, int minBet)
    {
        var value = Random.value;

        if (value >= 0f && value < 0.3f)
        {
            var betRate = (int)(totalMoney * 0.2f) + minBet;
            var betAmount = Random.Range(minBet, betRate);

            if (betAmount < totalMoney)
            {
                GameManager.Instance.MinBet = betAmount;
                return betAmount;
            }
        }
        return minBet;
    }
}