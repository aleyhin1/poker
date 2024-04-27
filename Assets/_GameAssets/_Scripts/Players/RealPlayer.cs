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

            if(IsMyTurn)
            {
                SpriteRenderer.color = Color.green;
                //Oyuncuya butonlar a��lacak
            }
            else
            {
                if (SpriteRenderer != null)
                    SpriteRenderer.color = DefaultColor;
            }
        } 
    }
}