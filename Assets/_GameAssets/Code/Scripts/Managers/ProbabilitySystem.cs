using UnityEngine;

public static class ProbabilitySystem
{
    private static float _probabilityValue = 0f;

    public static void PreflopStateMoveDeciding()
    {
        // elindeki kartlar kötü gelmiþ ise oyunu býrak (olabilir)

        // %5 olasýlýkla keyfi oyunu býrak 
        // min bet tutarý total paradan fazla ise oyunu býrak
        // min bet tutarý total paranýn %40 ýndan fazlasýný kapsýyorsa %30 ihtimalle oyunu býrak
        _probabilityValue = Random.value;

        


    }

    public static bool FoldProbability(int totalMoney, int minBet)
    {
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
  
        if (_probabilityValue >= 0f && _probabilityValue < 0.3f)
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