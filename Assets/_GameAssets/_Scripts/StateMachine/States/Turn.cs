using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turn : IPokerState
{
    private const int CARD_COUNT = 1;
    private const int CARD_LOCATION_INDEX = 3;
   
    public void EnterState()
    {
        Debug.Log("-----Turn-----");
        GameManager.Instance.DealerController.StartDealing(CARD_COUNT, CARD_LOCATION_INDEX);
    }

    public void UpdateState()
    {
         
    }

    public void ExitState()
    {
         
    }
}