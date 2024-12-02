using System;
using System.Linq;
using UnityEngine;

public class PokerShowdownState : PokerBaseState
{
    public override void EnterState(PokerStateManager pokerRound)
    {
        int winningScore = 0;
        string winningPlayer = "";
        Debug.Log("It is now the final Showdown State");

        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            string[] temp = pokerRound.allPlayers[i].handChecker.testingMethod(pokerRound.allPlayers[i].playerHand).Split("|||");
            pokerRound.allPlayers[i].handScore = int.Parse(temp[1]);
        }

        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            string[] temp = pokerRound.allPlayers[i].handChecker.testingMethod(pokerRound.allPlayers[i].playerHand).Split("|||");
            Debug.Log(pokerRound.allPlayers[i].name + "'s hand is a " + temp[0]);
            Debug.Log(pokerRound.allPlayers[i].handScore);
            pokerRound.allPlayers[i].showCards();
            if (pokerRound.allPlayers[i].handScore > winningScore)
            {
                winningScore = pokerRound.allPlayers[i].handScore;
                winningPlayer = pokerRound.allPlayers[i].name;
            }
        }
        Debug.Log("The winner is: " + winningPlayer);

    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        

        //pokerRound.SwitchState(pokerRound.BlindsState);
    }
}
