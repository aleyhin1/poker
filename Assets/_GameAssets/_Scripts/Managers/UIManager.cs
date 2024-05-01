using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private MoveManager _moveManager;

    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private Button _foldButton, _callButton, _bobButton , _raisePanelButton;

    [Header("Raise Panel")]
    private RealPlayer _realPlayer;
    private int _sliderValue;

    [SerializeField] private GameObject _raisePanelUI;
    [SerializeField] private Button _raiseButton, _raiseExitButton;
    [SerializeField] private Slider _raiseAmountSlider;
    [SerializeField] private TextMeshProUGUI _raiseText;

    private void Start()
    {
        ChangeVisibilityBobButton(false);
        ChangeVisibilityButtonsPanel(false);
        _raisePanelUI.SetActive(false);
        _realPlayer = GameManager.Instance.RealPlayer;

        _foldButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            {
                _moveManager.Fold(_realPlayer);
            }
        });

        _callButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            {
                var minBet = GameManager.Instance.MinBet;
                _moveManager.Call(_realPlayer, minBet);
            }
        });

        _bobButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            { 
                _moveManager.Bob(_realPlayer);
            }
        });

        _raisePanelButton.onClick.AddListener(() =>
        {
            if (_realPlayer.IsMyTurn)
            {
                _raiseAmountSlider.minValue = GameManager.Instance.MinBet * 2;
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
            _moveManager.Raise(_realPlayer, _sliderValue);
            _raisePanelUI.SetActive(false);
        });
        #endregion
    }

    public void ChangeVisibilityButtonsPanel(bool isActive)
    {
        _buttonsPanel.SetActive(isActive);
    }

    public void ChangeVisibilityBobButton(bool isActive)
    {
        _bobButton.gameObject.SetActive(isActive);
    }
}