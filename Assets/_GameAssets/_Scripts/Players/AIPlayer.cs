using System.Collections; 
using UnityEngine;

public class AIPlayer : Player
{
    private const float MOVE_TIME = 1f;

    public override bool IsMyTurn
    {
        get => base.IsMyTurn;
        set
        {
            base.IsMyTurn = value;

            if (IsMyTurn)
            {
                SelectedCircle.SetActive(true);

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
                SelectedCircle.SetActive(false);
            }
        }
    }
 
    private IEnumerator SmallBlindBet()
    {
        IsSmallBlind = false;
        IsSmallBlindPaid = true;
        yield return new WaitForSeconds(MOVE_TIME);
        var smallBlindBet = GameManager.Instance.SmallBlindBet;
        MoveManager.Instance.SmallBlindBet(this, smallBlindBet);
    }

    private IEnumerator BigBlindBet()
    {
        IsBigBlind = false;
        IsBigBlindPaid = true;
        yield return new WaitForSeconds(MOVE_TIME);
        MoveManager.Instance.BigBlindBet(this);
    }

    private IEnumerator Move()
    {
        int minBet = GameManager.Instance.MinBet;

        yield return new WaitForSeconds(MOVE_TIME);

        if (ProbabilitySystem.BobProbability(PokerStateManager.Instance.CurrentState))
        {
            MoveManager.Instance.Bob(this);
            yield break;
        }

        if (ProbabilitySystem.FoldProbability(TotalMoney,minBet))
        {
            MoveManager.Instance.Fold(this);
            yield break;
        }

        if (ProbabilitySystem.CallProbability())
        {
            MoveManager.Instance.Call(this, minBet);
            yield break;
        }

        minBet = ProbabilitySystem.SetBetRate(TotalMoney,minBet);
        MoveManager.Instance.Raise(this,minBet);
    }

   
}