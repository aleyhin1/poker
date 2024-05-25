using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance {  get; private set; }

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

    public void SmallBlindBet(Player player , int betAmount)
    {
        //Debug.Log("Small Blind Player : " + player.name + " Bet : " + betAmount);

        player.TotalMoney -= betAmount;

        player.ShowBetBox(betAmount);
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void BigBlindBet(Player player, int betAmount)
    {
      //  Debug.Log("Big Blind Player : " + player.name + " Bet : " + betAmount);

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
         
       // Debug.Log(player.name + " : Bob");

        player.IsBob = true;
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void Call(Player player , int betAmount, bool callingTheBet)
    {
        // Debug.Log(player.name + " : Call : " + minBet);

        if (callingTheBet)
        {
            player.CallingTheBet = false;

            int bet = DealerController.Instance.HighestBet - player.LastBet;
            player.TotalMoney -= bet;
            player.LastBet += bet;
            betAmount = player.LastBet;
        }
        else
        {
            player.TotalMoney -= betAmount;
        }

        player.IsCall = true;
        player.ShowBetBox(betAmount);

        DealerController.Instance.BetsPlaced = true;
        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }

    public void Fold(Player player)
    {
        //Debug.Log(player.name + " : Fold");
        player.IsFold = true;

        GameManager.Instance.LeaderboardManager.AddFoldPlayer(player);

        if (GameManager.Instance.LeaderboardManager.GetFoldList().Count == GameManager.Instance.Players.Count - 1)
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
       // Debug.Log(player.name + " : Raise :" + raiseAmount);

        player.TotalMoney -= raiseAmount;

        GameManager.Instance.MinBet = raiseAmount;
        player.IsRaise = true;

        player.ShowBetBox(raiseAmount);
        DealerController.Instance.BetsPlaced = true;

        GameManager.Instance.PlayerSequenceHandler.NextPlayer();
    }
}