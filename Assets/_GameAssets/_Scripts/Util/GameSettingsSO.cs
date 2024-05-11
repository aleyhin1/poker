using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "ScriptableObjects/Game Settings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    public int PlayersTotalMoney;
    public int PlayerCount;
    public int SmallBlindBet;
}