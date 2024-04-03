using UnityEngine;
using System.Collections.Generic;

public abstract class Player : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color defaultColor;

    public List<GameObject> Cards;
    private bool _isMyTurn;
    private bool _isFold;
    private bool _isSmallBlindPaid;

    public SpriteRenderer SetSpriteRenderer { set { _spriteRenderer = value; } }

    public virtual bool IsMyTurn 
    { 
        get { return _isMyTurn; } 
        set 
        {
            _isMyTurn = value;

            if (_isMyTurn) 
                _spriteRenderer.color = Color.green; 
            else 
                _spriteRenderer.color = defaultColor;
        } 
    }

    public bool IsFold { get { return _isFold; } set { _isFold = value; } }
    public bool IsSmallBlindPaid { get { return _isSmallBlindPaid; } set { _isSmallBlindPaid = value; } }

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