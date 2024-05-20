using UnityEngine;

public class MoveManager : MonoSingleton<MoveManager>
{
    public void SmallBlindBet(Player player , int betAmount)
    {
        Debug.Log("Small Blind Player : " + player.name + " Bet : " + betAmount);

        player.TotalMoney -= betAmount;

        player.ShowBetBox(betAmount);
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void BigBlindBet(Player player, int betAmount)
    {
        Debug.Log("Big Blind Player : " + player.name + " Bet : " + betAmount);

        player.TotalMoney -= betAmount;
        GameManager.Instance.MinBet = betAmount;

        player.ShowBetBox(betAmount);

        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void Bob(Player player)
    {
        var pokerState = PokerStateManager.Instance.CurrentState;
        if (pokerState == PokerState.Preflop || pokerState == PokerState.StaringState) 
            return;
         
        Debug.Log(player.name + " : Bob");

        player.IsBob = true;
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void Call(Player player , int minBet)
    {
        DealerController.Instance.BetsPlaced = true;

        Debug.Log(player.name + " : Call : " + minBet);

        player.TotalMoney -= minBet;
        player.IsCall = true;
        player.ShowBetBox(minBet);
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void Fold(Player player)
    {
        Debug.Log(player.name + " : Fold");

        player.IsFold = true;
        GameManager.Instance.Players.Remove(player);
        GameManager.Instance.LeaderBoardPlayerStack.Push(player);

        if (GameManager.Instance.Players.Count == 1)
        {
            PokerStateManager.Instance.EnterState(PokerState.EndState);
        }
        else
        {
            GameManager.Instance.PlayerSequenceHandler.NextPlayer();
        }
    }

    public void Raise(Player player, int raiseAmount)
    {
        DealerController.Instance.BetsPlaced = true;

        Debug.Log(player.name + " : Raise :" + raiseAmount);

        player.TotalMoney -= raiseAmount;

        GameManager.Instance.MinBet = raiseAmount;
        player.IsRaise = true;

        player.ShowBetBox(raiseAmount);
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }
}