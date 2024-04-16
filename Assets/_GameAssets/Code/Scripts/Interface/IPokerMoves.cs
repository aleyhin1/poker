using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPokerMoves
{
    public void Bet(Player player , int betAmount);
    public void Raise(Player player, int raiseAmount);
    public void Bob(Player player);
    public void Call(Player player);
    public void Fold(Player player);
}