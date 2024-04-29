using UnityEngine;

public static class ProbabilitySystem
{
    private static float _probabilityValue = 0f;

    public static bool CallProbability()
    {
        // %70 olasýkla call 
        // % 30 olasýlýkla raise 

        _probabilityValue = Random.value;

        if (_probabilityValue >= 0f && _probabilityValue < 0.7f)
            return true;

        return false;
    }

    public static bool FoldProbability(int totalMoney, int minBet)
    {
        // %5 olasýlýkla keyfi oyunu býrak 
        // min bet tutarý total paradan fazla ise oyunu býrak
        // min bet tutarý total paranýn %40 ýndan fazlasýný kapsýyorsa %30 ihtimalle oyunu býrak

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

        minBet *= 2;

        var betRate = (int)(totalMoney * 0.2f) + minBet;
        var betAmount = Random.Range(minBet, betRate);

        if (betAmount < totalMoney)
        {
            GameManager.Instance.MinBet = betAmount;
            return betAmount;
        }

        return minBet;
    }
}