using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    [SerializeField] private GameSettingsSO _settings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveMoney()
    {
        int moneyToSave = FirebaseManager.Instance.myScore - _settings.PlayersTotalMoney + GameManager.Instance.RealPlayer.TotalMoney;
        moneyToSave = Mathf.Max(0, moneyToSave);
        StartCoroutine(FirebaseManager.Instance.UpdateScore(moneyToSave));
        StartCoroutine(FirebaseManager.Instance.LoadUserData());
    }
}
