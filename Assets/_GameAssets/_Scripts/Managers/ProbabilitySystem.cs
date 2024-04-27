using UnityEngine;

public static class ProbabilitySystem
{
    private static float _probabilityValue = 0f;

    public static void PreflopStateMoveDeciding()
    {
        // elindeki kartlar k�t� gelmi� ise oyunu b�rak (olabilir)

        // %5 olas�l�kla keyfi oyunu b�rak 
        // min bet tutar� total paradan fazla ise oyunu b�rak
        // min bet tutar� total paran�n %40 �ndan fazlas�n� kaps�yorsa %30 ihtimalle oyunu b�rak
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