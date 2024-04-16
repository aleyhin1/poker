using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    private MoveManager _moveManager;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void Start()
    {
        _moveManager = GameManager.Instance.GetMoveManager;
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

                if (IsBigBlind)
                {
                    StartCoroutine(MoveManager.Instance.BlidBet(this));
                    return;
                }
                StartCoroutine(Move());
            }
            else
            {
                SpriteRenderer.color = DefaultColor;
            }
        }
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);

        var betAmount = ProbabilitySystem.SetBetRate(TotalMoney, GameManager.Instance.MinBet);
        GameManager.Instance.GetMoveManager.Bet(this, betAmount);
        
        GameManager.Instance.NextPlayer();
    }
}