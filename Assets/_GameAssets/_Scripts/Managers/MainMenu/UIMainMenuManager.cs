using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class UIMainMenuManager : MonoBehaviour
{
    public GameSettingsSO gameSettingsSO;
    public string UserName { get; set; }
    public int TotalMoney { get; set; }

    private const int MAX_BOT_COUNT = 6;
    private const int Min_BOT_COUNT = 1;
    private const float TOTAL_MONEY_RAD�O= 0.1f;
    private const float SMALL_BET_RAD�O = 0.05f;

    [Header("Main Menu")]
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Button _quickPlayButton, _customGameButton, _exitGameButton;

    [Header("Custom Game")]
    [SerializeField] private GameObject _customGamePanel;
    [SerializeField] private Button _backButton, _playGameButton;

    [Header("Bot Panel")]
    private int _botCount;
    [SerializeField] private TextMeshProUGUI _botCountText;
    [SerializeField] private Button _increaseBotCountButton, _decreaseBotCountButton;

    [Header("Money Panel")]
    private int _money;
    [SerializeField] private TextMeshProUGUI _totalMoneyText;
    [SerializeField] private Button _increaseMoneyButton, _decreaseMoneyButton;

    private void Start()
    {
        //Test
        TotalMoney = 10000;
        ///

        _mainMenuPanel.SetActive(true);
        _customGamePanel.SetActive(false);

        InitializeMainMenuButtons();
        InitializeCustomGameButtons();
    }

    private void InitializeMainMenuButtons()
    {
        _quickPlayButton.onClick.AddListener(() =>
        {
            gameSettingsSO.BotCount = MAX_BOT_COUNT;
            gameSettingsSO.PlayersTotalMoney = _money;
            gameSettingsSO.SmallBlindBet = Mathf.FloorToInt(TotalMoney * SMALL_BET_RAD�O);

            SceneLoaderManager.Instance.LoadGameScene();
        });

        _customGameButton.onClick.AddListener(() =>
        {
            _mainMenuPanel.SetActive(false);
            _customGamePanel.SetActive(true);
        });

        _exitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    
    private void InitializeCustomGameButtons()
    {
        _money = Mathf.FloorToInt(TotalMoney * TOTAL_MONEY_RAD�O);
        _totalMoneyText.text = _money.ToString();

        _backButton.onClick.AddListener(() =>
        {
            _mainMenuPanel.SetActive(true);
            _customGamePanel.SetActive(false);
        });

        _playGameButton.onClick.AddListener(() =>
        {
            gameSettingsSO.BotCount = _botCount;
            gameSettingsSO.PlayersTotalMoney = _money;
            gameSettingsSO.SmallBlindBet = Mathf.FloorToInt(_money * SMALL_BET_RAD�O);

            SceneLoaderManager.Instance.LoadGameScene();
        });

        InitializeBotPanel();
        InitializeMoneyPanel();
    }

    #region BotPanel
    private void InitializeBotPanel()
    {
        _botCount = gameSettingsSO.BotCount;
        _botCountText.text = _botCount.ToString();

        _increaseBotCountButton.onClick.AddListener(() =>
        {
            IncreaseBotCount();
        });

        _decreaseBotCountButton.onClick.AddListener(() =>
        {
            DecreaseBotCount();
        });
    }

    private void IncreaseBotCount()
    {
        _botCount++;

        if (_botCount > MAX_BOT_COUNT)
            _botCount = Min_BOT_COUNT;

        _botCountText.text = _botCount.ToString();
    }

    private void DecreaseBotCount()
    {
        _botCount--;

        if (_botCount < Min_BOT_COUNT)
            _botCount = MAX_BOT_COUNT;

        _botCountText.text = _botCount.ToString();
    }

    #endregion BotPanel

    #region MoneyPanel
    private void InitializeMoneyPanel()
    {
        _increaseMoneyButton.onClick.AddListener(() =>
        {
            IncreaseMoney();
        });

        _decreaseMoneyButton.onClick.AddListener(() =>
        {
            DecreaseMoney();
        });
    }

    private void IncreaseMoney()
    {
        _money += Mathf.FloorToInt(TotalMoney * TOTAL_MONEY_RAD�O);

        if (_money > TotalMoney)
            _money = TotalMoney;
    
        _totalMoneyText.text = _money.ToString();
    }  

    private void DecreaseMoney()
    {
        _money -= Mathf.FloorToInt(TotalMoney * TOTAL_MONEY_RAD�O);

        if (_money <= 0)
            _money = Mathf.FloorToInt(TotalMoney * TOTAL_MONEY_RAD�O);
        
        _totalMoneyText.text = _money.ToString();
    }
    #endregion MoneyPanel
}