using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public int TotalMoney;
    public bool IsSmallBlindPaid;
    public bool IsBigBlindPaid;
    public bool IsBigBlind;
    public bool IsSmallBlind;

    public bool IsFold;
    public bool IsCall;
    public bool IsBob;
    public bool IsRaise;

    [field : SerializeField] public List<Card> Cards { get; private set; } = new List<Card>(2);
    [SerializeField] private Transform _cardPos1, _cardPos2;
    [field : SerializeField] public Color DefaultColor { get; private set; }
    public virtual bool IsMyTurn { get; set; }


    public (Vector3 pos, Quaternion rot) AddCards(Card card)
    {
        Quaternion rot = card.transform.rotation;
        Vector3 pos = card.transform.position;
        
        if (Cards.Count < 1)
        {
            pos = _cardPos1.position;
            rot = _cardPos1.rotation;
        }
        else
        {
            pos = _cardPos2.position;
            rot = _cardPos2.rotation;
        }
        Cards.Add(card);
        card.gameObject.SetActive(true);

        return (pos, rot);
    }
}