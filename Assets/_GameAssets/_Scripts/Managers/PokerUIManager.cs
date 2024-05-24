using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DG.Tweening;

public class PokerUIManager : MonoSingleton<PokerUIManager>
{
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private Button _foldButton, _callButton, _bobButton , _raisePanelButton;

    [Header("Raise Panel")]
    private RealPlayer _realPlayer;
    private int _sliderValue;

    [SerializeField] private GameObject _raisePanelUI;
    [SerializeField] private Button _raiseButton, _raiseExitButton;
    [SerializeField] private Slider _raiseAmountSlider;
    [SerializeField] private TextMeshProUGUI _raiseText;
    [Header("Curtain Config")]
    [Space]
    [SerializeField] private SpriteRenderer _curtain;
    [SerializeField] private float _curtainMaxAlphaValue;
    [SerializeField] private float _curtainLowerTime;


    private void Start()
    {
        _buttonsPanel.SetActive(false);
        ChangeVisibilityBobButton(false);
        _raisePanelUI.SetActive(false);
        _realPlayer = GameManager.Instance.RealPlayer;

        _foldButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            {
                MoveManager.Instance.Fold(_realPlayer);
            }
        });

        _callButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            {
                var minBet = GameManager.Instance.MinBet;
                MoveManager.Instance.Call(_realPlayer, minBet, _realPlayer.CallingTheBet);
            }
        });

        _bobButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn && !DealerController.Instance.BetsPlaced)
            { 
                MoveManager.Instance.Bob(_realPlayer);
            }
        });

        _raisePanelButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            {
                var minValue = GameManager.Instance.MinBet * 2;

                if (minValue > _realPlayer.TotalMoney)
                    return;

                _raiseAmountSlider.minValue = minValue;
                _raiseAmountSlider.maxValue = _realPlayer.TotalMoney;
                _raisePanelUI.SetActive(true);
            }
        });

        #region Raise Panel 
        _raiseExitButton.onClick.AddListener(() =>
        {
            _raisePanelUI.SetActive(false);
        });

        _raiseAmountSlider.onValueChanged.AddListener((float value) =>
        {
            _sliderValue = Mathf.FloorToInt(value);
            _raiseText.text = _sliderValue.ToString();
        });

        _raiseButton.onClick.AddListener(() =>
        {
            MoveManager.Instance.Raise(_realPlayer, _sliderValue);
            _raisePanelUI.SetActive(false);
        });
        #endregion
    }

    public void CallingBetVisibilityButton()
    {
        if (_realPlayer != null)
        {
            if (_realPlayer.TotalMoney >= GameManager.Instance.MinBet)
                _callButton.gameObject.SetActive(true);
            else
                _callButton.gameObject.SetActive(false);

            ChangeVisibilityBobButton(false);
            _raisePanelButton.gameObject.SetActive(false);
            
            _foldButton.gameObject.SetActive(true);
            
            _buttonsPanel.SetActive(true);
        }
    }

    public void ChangeVisibilityButtonsPanel(bool isActive)
    {
        if (_realPlayer != null)
        {
            if (_realPlayer.TotalMoney < GameManager.Instance.MinBet * 2)
            {
                _raisePanelButton.gameObject.SetActive(false);
                _raisePanelUI.gameObject.SetActive(false);
            }
            
            if (_realPlayer.TotalMoney < GameManager.Instance.MinBet)
            {
                _callButton.gameObject.SetActive(false);
            }

            _buttonsPanel.SetActive(isActive);

            if (!isActive)
                _raisePanelUI.SetActive(isActive);
        }
    }

    public void ChangeVisibilityBobButton(bool isActive)
    {
        _bobButton.gameObject.SetActive(isActive);
    }

    public void LowerCurtain()
    {
        Color color = _curtain.color;

        _curtain.DOColor(new Color(color.r, color.g, color.b, _curtainMaxAlphaValue), _curtainLowerTime);
    }

    public void HigherCurtain()
    {
        Color color = _curtain.color;

        _curtain.DOColor(new Color(color.g, color.g, color.b, 0), _curtainLowerTime);
    }
}