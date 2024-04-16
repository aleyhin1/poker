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
                //Oyuncuya butonlar açýlacak
            }
            else
            {
                SpriteRenderer.color = DefaultColor;
            }
        } 
    }

    public void FaceUpCards()
    {
        List<Card> cards = GetCards;
        foreach (var card in cards)
        {
            card.FaceUp();
        }
    }
}