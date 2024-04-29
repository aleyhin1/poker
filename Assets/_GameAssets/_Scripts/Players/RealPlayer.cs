using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : Player
{
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

            if(IsMyTurn && SpriteRenderer != null)
            {
                SpriteRenderer.color = Color.green;
                HandleStaringState();
            }
            else
            {
                if (SpriteRenderer != null)
                    SpriteRenderer.color = DefaultColor;
            }
        } 
    }

    private void HandleStaringState()
    {
        if (IsSmallBlind)
        {
            SmallBlindBet();
        }
        else if (IsBigBlind)
        {
            BigBlindBet();
        }
    }

    private void SmallBlindBet()
    {
        IsSmallBlind = false;
        IsSmallBlindPaid = true;

        var smallBlindBet = GameManager.Instance.SmallBlindBet;
        MoveManager.Instance.SmallBlindBet(this, smallBlindBet);
    }

    private void BigBlindBet()
    {
        IsBigBlind = false;
        IsBigBlindPaid = true;
        MoveManager.Instance.BigBlindBet(this);
    }

}