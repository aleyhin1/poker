using System.Collections; 
using UnityEngine;

public class AIPlayer : Player
{
    private const float MOVE_TIME = 1f;

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
                CheckState();
            }
            else if (SpriteRenderer != null)
            {
                SpriteRenderer.color = DefaultColor;
            }
        }
    }

    private void CheckState()
    {
        var pokerState = PokerStateManager.Instance.CurrentState;

        switch (pokerState)
        {
            case PokerState.StaringState:
                HandleStaringState();
                break;
            case PokerState.Preflop:
                StartCoroutine(PreflopStateMove());
                break;
            case PokerState.Flop:
                break;
            case PokerState.Turn:
                break;
            case PokerState.River:
                break;
            case PokerState.EndState:
                break;
            default:
                break;
        }
    }

    private void HandleStaringState()
    {
        if (IsSmallBlind)
        {
            StartCoroutine(SmallBlindBet());
        }
        else if (IsBigBlind)
        {
            StartCoroutine(BigBlindBet());
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

    private IEnumerator PreflopStateMove()
    {
        int minBet = GameManager.Instance.MinBet;

        yield return new WaitForSeconds(MOVE_TIME);

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