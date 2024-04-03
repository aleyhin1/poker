using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : Player
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

            if(IsMyTurn)
            {
                //Oyuncuya butonlar açýlacak
            }
        } 
    }
}