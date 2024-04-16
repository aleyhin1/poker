using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using DG.Tweening;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private List<Card> _cards = new List<Card>(2);

    [SerializeField] private Transform _cardPos1, _cardPos2;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _defaultColor;

    [SerializeField] private bool _isMyTurn;
    private bool _isFold;

    [SerializeField] private bool _isSmallBlindPaid;
    [SerializeField] private bool _isBigBlind;

    private int _totalMoney;
    private int _betAmount;

    public virtual List<Card> GetCards { get { return _cards; } }
    public Color DefaultColor { get { return _defaultColor; }}
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } set { _spriteRenderer = value; } }
    
    public int BetAmount { get { return _betAmount; }}
    public int TotalMoney { get { return _totalMoney; } set { _totalMoney = value; } }

    public bool IsFold { get { return _isFold; } set { _isFold = value; } }

    public bool IsSmallBlindPaid { get { return _isSmallBlindPaid; } set { _isSmallBlindPaid = value; } }
    public bool IsBigBlind { get { return _isBigBlind; } set { _isBigBlind = value; } }

    public virtual bool IsMyTurn{ get { return _isMyTurn; } set {_isMyTurn = value; } }

    public void AddCards(Card card)
    {
        var nextPos = new Vector3(0,3,0);
        card.transform.position = nextPos;

        if (_cards.Count < 1)
        {
            nextPos = _cardPos1.position;
            card.transform.rotation = _cardPos1.rotation;
        }
        else
        {
            nextPos = _cardPos2.position;
            card.transform.rotation = _cardPos2.rotation;
        }

        card.transform.DOMove(nextPos, 1f);

        _cards.Add(card);
        card.gameObject.SetActive(true);
    }
}