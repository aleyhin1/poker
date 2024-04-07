using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Tuple<CardSuit, CardRank> CardValue { get; private set; }

    public void CreateCard(CardSuit suit, CardRank rank)
    {
        CardValue = new Tuple<CardSuit, CardRank>(suit, rank);
    }
}
