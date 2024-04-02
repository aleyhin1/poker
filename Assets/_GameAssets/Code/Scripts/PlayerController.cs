using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<GameObject> _cardsInHand;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _isMyTurn;
    public bool IsMyTurn
    {
        get
        {
            return _isMyTurn;
        }
        set
        {
            _isMyTurn = value;
            if (_isMyTurn)
                _spriteRenderer.color = Color.green;
            else
                _spriteRenderer.color = Color.white;
        }
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}