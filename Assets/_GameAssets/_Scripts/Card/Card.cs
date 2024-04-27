using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Tuple<CardSuit, CardRank> Value { get; private set; }
    private Sprite _front;
    private Sprite _back;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void CreateCard(CardSuit suit, CardRank rank, Sprite front, Sprite back)
    {
        Value = new Tuple<CardSuit, CardRank>(suit, rank);
        _front = front;
        _back = back;
        FaceUp();
    }

    public void FaceUp()
    {
        _renderer.sprite = _front;
    }

    public void FaceDown()
    {
        _renderer.sprite = _back;
    }
}
