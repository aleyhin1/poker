using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    [SerializeField] private float _decisionDuration = 1f;

    private void Start()
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

                if (IsBigBlind)
                {
                    StartCoroutine(BigBlindBetDecision());
                }
                StartCoroutine(NextPlayer());
            }
            else
            {
                SpriteRenderer.color = DefaultColor;
            }
        }
    }
    private IEnumerator BigBlindBetDecision()
    {
        //Wait

        yield return new WaitForSeconds(_decisionDuration);

        var minBet = GameManager.Instance.MinBet;
        var betAmount = Random.Range(minBet, minBet * 2);
        Bet(betAmount);

        if (betAmount > minBet)
            GameManager.Instance.MinBet = betAmount;

        IsBigBlind = false;

        Debug.Log("Bet : " + GameManager.Instance.MinBet);
    }

    private IEnumerator NextPlayer()
    {
        yield return new WaitForSeconds(_decisionDuration);
        GameManager.Instance.NextPlayer();
    }


}