using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _defaultColor;

    public List<GameObject> Cards;
    [SerializeField] private bool _isMyTurn;
    private bool _isFold;

    [SerializeField] private bool _isSmallBlindPaid;
    [SerializeField] private bool _isBigBlind;

    private int _allMoney;
    private int _betAmount;

    public Color DefaultColor { get { return _defaultColor; }}
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } set { _spriteRenderer = value; } }
    
    public int BetAmount { get { return _betAmount; }}
    public int AllMoney { get { return _allMoney; } set { _allMoney = value; } }

    public bool IsFold { get { return _isFold; } set { _isFold = value; } }

    public bool IsSmallBlindPaid { get { return _isSmallBlindPaid; } set { _isSmallBlindPaid = value; } }
    public bool IsBigBlind { get { return _isBigBlind; } set { _isBigBlind = value; } }


    public virtual bool IsMyTurn
    {
        get { return _isMyTurn; }
        set {_isMyTurn = value; }
    }

    public void Bet(int betAmount)
    {
        _betAmount = betAmount;
    }

    public void Bob()
    {
        Debug.Log("Bob");     
    }

    public void Call()
    {
        Debug.Log("Call");
    }

    public void Fold()
    {
        Debug.Log("Fold");
    }

    public void Raise()
    {
        Debug.Log("Raise");
    }
}