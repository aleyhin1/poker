using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndState : IPokerState
{
    public void EnterState()
    {
        Debug.Log("-------EndState--------");

        Debug.Log("oyuncular�n kartlar� hesaplan�yor..");
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {

    }
}
