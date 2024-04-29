using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flop : IPokerState
{
    public void EnterState()
    {
        Debug.Log("-----Flop-----");
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
    }
}
