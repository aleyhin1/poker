using UnityEngine;

public class Flop : IPokerState
{
    private const int CARD_COUNT = 3;
    private const int CARD_LOCATION_INDEX = 0;

    public void EnterState()
    {
        Debug.Log("-----Flop-----");
        GameManager.Instance.DealerController.StartDealing(CARD_COUNT, CARD_LOCATION_INDEX);
    }
}