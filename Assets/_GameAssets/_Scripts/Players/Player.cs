using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using System.Collections;

public abstract class Player : MonoBehaviour
{
    [field: SerializeField] public List<Card> Cards { get; private set; } = new List<Card>(2);
    [SerializeField] private Transform _cardPos1, _cardPos2;

    public GameObject SelectedCircle;

    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TextMeshPro _dialogText;

    public virtual bool IsMyTurn { get; set; }

    public int TotalMoney;

    public bool IsSmallBlindPaid;
    public bool IsBigBlindPaid;
    public bool IsBigBlind;
    public bool IsSmallBlind;

    public bool IsCall;
    public bool IsBob;
    
    private bool _isFold;
    private bool _isRaise;

    public bool IsRaise 
    {
        get {  return _isRaise; }
        set 
        {
            _isRaise = value;
            if (_isRaise)
            {
                StartCoroutine(ShowDialogue("Raise"));
            }
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
                PutDownCards();
            }
        } 
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

}