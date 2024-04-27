using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{
    [SerializeField] private List<Card> _cards = new List<Card>(2);
    [SerializeField] private Transform _cardPos1, _cardPos2;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private bool _isMyTurn;
    [SerializeField] private bool _isSmallBlindPaid;
    [SerializeField] private bool _isBigBlindPaid;
    [SerializeField] private bool _isBigBlind;
    [SerializeField] private bool _isSmallBlind;
    private SpriteRenderer _spriteRenderer;
    private bool _isFold;
    private int _totalMoney;

    public virtual List<Card> GetCards { get { return _cards; } }
    public Color DefaultColor { get { return _defaultColor; }}
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } set { _spriteRenderer = value; } }
  
    public int TotalMoney { get { return _totalMoney; } set { _totalMoney = value; } }

    public bool IsFold { get { return _isFold; } set { _isFold = value; } }

    public bool IsSmallBlind { get { return _isSmallBlind; } set { _isSmallBlind = value; } }
    public bool IsSmallBlindPaid { get { return _isSmallBlindPaid; } set { _isSmallBlindPaid = value; } }
    public bool IsBigBlind { get { return _isBigBlind; } set { _isBigBlind = value; } }
    public bool IsBigBlindPaid { get { return _isBigBlindPaid; } set { _isBigBlindPaid = value; } }

    public virtual bool IsMyTurn{ get { return _isMyTurn; } set {_isMyTurn = value; } }

    public (Vector3 pos, Quaternion rot) AddCards(Card card)
    {
        Quaternion rot = card.transform.rotation;
        Vector3 pos = card.transform.position;
        
        if (_cards.Count < 1)
        {
            pos = _cardPos1.position;
            rot = _cardPos1.rotation;
        }
        else
        {
            pos = _cardPos2.position;
            rot = _cardPos2.rotation;
        }
        _cards.Add(card);
        card.gameObject.SetActive(true);

        return (pos, rot);
    }
}