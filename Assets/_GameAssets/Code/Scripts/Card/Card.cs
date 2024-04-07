using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Tuple<CardSuit, CardRank> Value { get; private set; }
    private Sprite _sprite;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void CreateCard(CardSuit suit, CardRank rank, Sprite sprite)
    {
        Value = new Tuple<CardSuit, CardRank>(suit, rank);
        _sprite = sprite;
        _renderer.sprite = _sprite;
    }
}
