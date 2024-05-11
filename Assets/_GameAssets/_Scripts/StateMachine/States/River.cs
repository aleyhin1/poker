using UnityEngine;

public class River : IPokerState
{
    private const int CARD_COUNT = 1;
    private const int CARD_LOCATION_INDEX = 4;

    public void EnterState()
    {
        Debug.Log("--------River----------");
        GameManager.Instance.DealerController.StartDealing(CARD_COUNT, CARD_LOCATION_INDEX);
    }
}
