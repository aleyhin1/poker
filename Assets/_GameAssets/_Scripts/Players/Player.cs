using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using System.Collections;

public abstract class Player : MonoBehaviour
{
    public const float MOVE_TIME = 1f;

    [field: SerializeField] public List<Card> Cards { get; private set; } = new List<Card>(2);
    [SerializeField] private Transform _cardPos1, _cardPos2;

    public GameObject SelectedCircle;

    public GameObject BetBox;

    private Vector2 _betBoxPos;
    [SerializeField] private GameObject _winBox;
    [SerializeField] private GameObject _betBox => BetBox;
    [SerializeField] private TextMeshPro _betText;

    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TextMeshPro _dialogText;

    [SerializeField] private TextMeshPro _totalMoneyText;

    public virtual bool IsMyTurn { get; set; }

    private int _totalMoney;
    public int TotalBet;
    public int LastBet;

    public bool CallingTheBet;

    public bool IsBet;
    public bool IsSmallBlindPaid;
    public bool IsBigBlindPaid;
    public bool IsBigBlind;
    public bool IsSmallBlind;

    private bool _isBob;
    private bool _isCall;
    private bool _isFold;
    private bool _isRaise;

    private void Start()
    {
        _betBoxPos = _betBox.transform.position;
        _betBox.SetActive(false);
        _dialogBox.SetActive(false);
    }

    public int TotalMoney 
    {
        get { return _totalMoney; }
        set 
        {
            if (_totalMoney != value)
                _totalMoneyText.text = "$"+value.ToString();
           
            _totalMoney = value;
        }
    }
    public bool IsBob 
    {
        get { return _isBob; }
        set 
        {  
            _isBob = value;
            if (_isBob)
                StartCoroutine(ShowDialogue("Bob"));
        }
    }
    public bool IsCall
    {
        get { return _isCall; } 
        set 
        { 
            _isCall = value;
            if (_isCall)
                StartCoroutine(ShowDialogue($"Call"));
        }
    }
    public bool IsRaise 
    {
        get {  return _isRaise; }
        set 
        {
            _isRaise = value;
            if (_isRaise)
                StartCoroutine(ShowDialogue($"Raise"));
        }
    }
    public bool IsFold 
    {
        get { return _isFold; }
        set 
        {
            _isFold = value;
            if (_isFold)
            {
                StartCoroutine(ShowDialogue("Fold"));
                DealerController.Instance.CollectBets();
                PutDownCards();
            }
        } 
    }

    public void ResetBetBox()
    {
        LastBet = 0;
        IsBet = false;
        _betBox.SetActive(false);
        _betBox.transform.position = _betBoxPos;
    }

    public void ShowBetBox(int bet)
    {
        IsBet = true;

        LastBet = bet;
        TotalBet += LastBet;
        var targetPos = _betBox.transform.position;
        _betBox.transform.position = transform.position;

        _betText.text = "$" + bet.ToString();
        _betBox.SetActive(true);

        _betBox.transform.DOMove(targetPos, 0.5f)
            .SetEase(Ease.Linear);
    }

    public void ShowWinBox(bool state)
    {
        _winBox.gameObject.SetActive(state);
    }

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

    private void PutDownCards()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            PutDownCardsAnimaton(Cards[i]);
        }
    }

    private void PutDownCardsAnimaton(Card card)
    {
        var pos = new Vector3(0, 4, 0);
        
        card.transform.DOMove(pos, 0.7f)
            .SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                card.gameObject.SetActive (false);
            });
    }

    private IEnumerator ShowDialogue(string dialogue)
    {
        _dialogText.text = dialogue;
        _dialogBox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        _dialogBox.gameObject.SetActive(false);
    }

    public IEnumerator SmallBlindBet()
    {
        IsSmallBlind = false;
        IsSmallBlindPaid = true;
        yield return new WaitForSeconds(MOVE_TIME);
        var smallBlindBet = GameManager.Instance.MinBet;
        MoveManager.Instance.SmallBlindBet(this, smallBlindBet);
    }

    public IEnumerator BigBlindBet()
    {
        int minBet = GameManager.Instance.MinBet;
        int bigBlindBet = minBet * 2;

        IsBigBlind = false;
        IsBigBlindPaid = true;

        GameManager.Instance.MinBet = bigBlindBet;
        yield return new WaitForSeconds(MOVE_TIME);
        MoveManager.Instance.BigBlindBet(this, bigBlindBet);
    }

    public void ResetPlayer()
    {
        Cards.Clear();

        CallingTheBet = false;
        _isBob = false;
        _isCall = false;
        _isFold = false;
        _isRaise = false;
        
        IsMyTurn = false;
        IsBigBlind = false;
        IsSmallBlind = false;
        
        LastBet = 0;
        TotalBet = 0;

        _betBoxPos = _betBox.transform.position;
        _betBox.SetActive(false);
        _dialogBox.SetActive(false);

        ShowWinBox(false);
    }
}