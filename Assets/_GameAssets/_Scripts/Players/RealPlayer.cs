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
                    SmallBlindBet();
                }
                else if (IsBigBlind)
                {
                    BigBlindBet();
                }
                else
                {
                    GameManager.Instance.UIManager.ChangeVisibilityButtonsPanel(true);   
                }
            }
            else
            {
                SelectedCircle.SetActive(false);
                GameManager.Instance.UIManager.ChangeVisibilityButtonsPanel(false);
            }
        } 
    }
 
    private void SmallBlindBet()
    {
        IsSmallBlind = false;
        IsSmallBlindPaid = true;

        var smallBlindBet = GameManager.Instance.SmallBlindBet;
        MoveManager.Instance.SmallBlindBet(this, smallBlindBet);
    }

    private void BigBlindBet()
    {
        IsBigBlind = false;
        IsBigBlindPaid = true;
        MoveManager.Instance.BigBlindBet(this);
    }

}