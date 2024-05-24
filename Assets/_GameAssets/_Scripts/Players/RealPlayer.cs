using System.Collections;
using UnityEngine;

public class RealPlayer : Player
{
    public override bool IsMyTurn 
    { 
        get => base.IsMyTurn; 
        set 
        {
            base.IsMyTurn = value;

            if(IsMyTurn)
            {
                SelectedCircle.SetActive(true);
                if (IsSmallBlind)
                {
                    StartCoroutine(SmallBlindBet());
                }
                else if (IsBigBlind)
                {
                    StartCoroutine(BigBlindBet());
                }
                else
                {
                    if (CallingTheBet)
                    {
                        PokerUIManager.Instance.CallingBetVisibilityButton();
                        return;
                    }
                    PokerUIManager.Instance.ChangeVisibilityBobButton(!DealerController.Instance.BetsPlaced);
                    PokerUIManager.Instance.ChangeVisibilityButtonsPanel(true);   
                }
            }
            else
            {
                SelectedCircle.SetActive(false);
                PokerUIManager.Instance.ChangeVisibilityButtonsPanel(false);
            }
        } 
    }
}