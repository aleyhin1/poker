using System.Collections; 
using UnityEngine;

public class AIPlayer : Player
{

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();   
    }
 
    public override bool IsMyTurn
    {
        get => base.IsMyTurn;
        set
        {
            base.IsMyTurn = value;

            if (IsMyTurn)
            {
                SpriteRenderer.color = Color.green;

                if (IsSmallBlind)
                {
                    StartCoroutine(SmallBlindBet());
                }
                else if (IsBigBlind)
                {
                    StartCoroutine(BigBlindBet());
                }
                else
                {
                    StartCoroutine(Move());
                }
            }
            else
            {
                if (SpriteRenderer != null)
                    SpriteRenderer.color = DefaultColor;
            }
        }
    }

    private IEnumerator SmallBlindBet()
    {
        IsSmallBlind = false;
        IsSmallBlindPaid = true;

        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.GetMoveManager.SmallBlindBet(this);
    }

    private IEnumerator BigBlindBet()
    {
        IsBigBlind = false;
        IsBigBlindPaid = true;

        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.GetMoveManager.BigBlidBet(this);
    }

    private IEnumerator Move()
    {
        var betAmount = ProbabilitySystem.SetBetRate(TotalMoney, GameManager.Instance.MinBet);

        yield return new WaitForSeconds(1.5f);
        
        GameManager.Instance.GetMoveManager.Bet(this, betAmount);
    }
}