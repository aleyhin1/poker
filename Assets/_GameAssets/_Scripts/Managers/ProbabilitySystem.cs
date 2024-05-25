using UnityEngine;

public static class ProbabilitySystem
{
    private static float _probabilityValue = 0f;

    public static bool BobProbability(PokerState currentState)
    {
        if (DealerController.Instance.BetsPlaced)
            return false;

        if (currentState == PokerState.Preflop)
            return false;

        _probabilityValue = Random.value;

        if (_probabilityValue >= 0f && _probabilityValue < 0.75f)
            return true;

        return false;
    }

    public static bool CallProbability()
    {
        _probabilityValue = Random.value;

        if (_probabilityValue >= 0f && _probabilityValue < 0.75f)
            return true;

        return false;
    }

    public static bool FoldProbability(int totalMoney, int minBet)
    {
        _probabilityValue = Random.value;
        var money = totalMoney * 0.4f;

        if (minBet >= totalMoney)
            return true;
       
        if (_probabilityValue >= 0 && _probabilityValue <= 0.05f)
            return true;

        if (minBet >= money && (_probabilityValue > 0.05f && _probabilityValue <= 0.35f))
            return true;

        return false;
    }

    public static int SetBetRate(int totalMoney, int minBet)
    {
        _probabilityValue = Random.value;
        // minBet *= 2;

        var betRate = (int)(totalMoney * _probabilityValue * _probabilityValue) + minBet;
        var betAmount = Random.Range(minBet, betRate);
        betAmount = (betAmount / 100) * 100;

        if (betAmount < totalMoney)
        {
            GameManager.Instance.MinBet = betAmount;
            return betAmount;
        }

        return minBet;
    }
}