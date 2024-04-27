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
                CheckState();
            }
            else
            {
                if (SpriteRenderer != null)
                    SpriteRenderer.color = DefaultColor;
            }
        }
    }

    private void CheckState()
    {
        var pokerState = PokerStateManager.Instance.GetCurrentState;

        switch (pokerState)
        {
            case PokerState.StaringState:

                if (IsSmallBlind)
                    StartCoroutine(SmallBlindBet());
                else if (IsBigBlind)
                    StartCoroutine(BigBlindBet());
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

    private IEnumerator SmallBlindBet()
    {
        IsSmallBlind = false;
        IsSmallBlindPaid = true;
        yield return new WaitForSeconds(1f);
        var smallBlindBet = GameManager.Instance.GetSmallBlindBet;
        GameManager.Instance.GetMoveManager.SmallBlindBet(this, smallBlindBet);
    }

    private IEnumerator BigBlindBet()
    {
        IsBigBlind = false;
        IsBigBlindPaid = true;
        yield return new WaitForSeconds(1f);
        GameManager.Instance.GetMoveManager.BigBlidBet(this);
    }

    private IEnumerator PreflopStateMove()
    {
        //Hamle yapacak 
        yield break;
    }
}