using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Tuple<CardSuit, CardRank> Value { get; private set; }
    private Texture2D _sprite;

    public void CreateCard(CardSuit suit, CardRank rank, Texture2D sprite)
    {
        Value = new Tuple<CardSuit, CardRank>(suit, rank);
        _sprite = sprite;
    }
}
