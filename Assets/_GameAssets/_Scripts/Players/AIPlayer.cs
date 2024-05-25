using System.Collections; 
using UnityEngine;

public class AIPlayer : Player
{
    private bool _isActive;

    public bool IsActive 
    {
        get { return _isActive; }
        set
        {
            _isActive = value;

            if (!_isActive)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true); 
        } 
    }

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
 
    private IEnumerator Move()
    {
        int minBet = GameManager.Instance.MinBet;

        yield return new WaitForSeconds(MOVE_TIME);

        if (CallingTheBet)
        {
            if (DealerController.Instance.BetsPlaced)
            {
                if (minBet > TotalMoney)
                    MoveManager.Instance.Fold(this);
                else
                    MoveManager.Instance.Call(this, minBet, true);
            }
            else
            {
                MoveManager.Instance.Fold(this);
            }
            yield break;
        }

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

        if (DealerController.Instance.BetsPlaced && ProbabilitySystem.CallProbability() && minBet <= TotalMoney)
        {
            MoveManager.Instance.Call(this, minBet, false);
            yield break;
        }

        minBet = ProbabilitySystem.SetBetRate(TotalMoney, minBet);

        if (minBet > TotalMoney)
        {
            MoveManager.Instance.Fold(this);
            yield break;
        }

        MoveManager.Instance.Raise(this, minBet);
    }
}