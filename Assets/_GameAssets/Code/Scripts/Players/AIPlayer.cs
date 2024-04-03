using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    private void Start()
    {
        SetSpriteRenderer = GetComponent<SpriteRenderer>();   
    }

    public override bool IsMyTurn
    {
        get => base.IsMyTurn;
        set
        {
            base.IsMyTurn = value;

            if (IsMyTurn)
            {
                //Bot ne yapmasý geriyorsa burada yapýcak
            }
        }
    }

}