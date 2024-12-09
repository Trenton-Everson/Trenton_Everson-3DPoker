using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PokerShowdownState : PokerBaseState
{
    GameObject winnerNameObj;
    winnerName winnerName;
    public override void EnterState(PokerStateManager pokerRound)
    {
        int winningScore = 0;
        string winningPlayer = "";
        int winningPlayerIndex = 0;
        pokerRound.roundNamer.setName("Showdown");
        winnerNameObj = GameObject.Find("Winner Name");

        winnerName = winnerNameObj.GetComponent<winnerName>();
        string[] temp = new string[0];

        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            temp = pokerRound.allPlayers[i].handChecker.testingMethod(pokerRound.allPlayers[i].playerHand).Split("|||");
            pokerRound.allPlayers[i].handScore = int.Parse(temp[1]);
        }

        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            temp = pokerRound.allPlayers[i].handChecker.testingMethod(pokerRound.allPlayers[i].playerHand).Split("|||");
            Debug.Log(pokerRound.allPlayers[i].name + "'s hand is a " + temp[0]);
            Debug.Log(pokerRound.allPlayers[i].handScore);
            pokerRound.allPlayers[i].showCards();
            if (pokerRound.allPlayers[i].handScore > winningScore)
            {
                winningScore = pokerRound.allPlayers[i].handScore;
                winningPlayer = pokerRound.allPlayers[i].name;
                winningPlayerIndex = i;
            }
            else if (pokerRound.allPlayers[i].handScore == winningScore)
            {
                if (pokerRound.allPlayers[i].playerHand[0].rank > pokerRound.allPlayers[winningPlayerIndex].playerHand[0].rank) //if first card is bigger than their first
                {
                   if (pokerRound.allPlayers[i].playerHand[0].rank > pokerRound.allPlayers[winningPlayerIndex].playerHand[1].rank) //is your first bigger than their second
                    {
                        winningScore = pokerRound.allPlayers[i].handScore;
                        winningPlayer = pokerRound.allPlayers[i].name;
                        winningPlayerIndex = i;
                        //first card of currPlayer are bigger than the current winning
                    }
                    else if  (pokerRound.allPlayers[i].playerHand[0].rank < pokerRound.allPlayers[winningPlayerIndex].playerHand[1].rank)
                    //if first card of currPlayer is bigger than first but less than second check currPlayer second card
                    {
                        if (pokerRound.allPlayers[i].playerHand[1].rank > pokerRound.allPlayers[winningPlayerIndex].playerHand[1].rank)
                        //Second card of curr player is bigger than the second
                        {
                            winningScore = pokerRound.allPlayers[i].handScore;
                            winningPlayer = pokerRound.allPlayers[i].name;
                            winningPlayerIndex = i;
                        }
                    }
                }
                else if (pokerRound.allPlayers[i].playerHand[1].rank > pokerRound.allPlayers[winningPlayerIndex].playerHand[0].rank)
                {
                    if (pokerRound.allPlayers[i].playerHand[1].rank > pokerRound.allPlayers[winningPlayerIndex].playerHand[1].rank)
                    {
                        winningScore = pokerRound.allPlayers[i].handScore;
                        winningPlayer = pokerRound.allPlayers[i].name;
                        winningPlayerIndex = i;
                    }
                }
            }

        }
        Debug.Log("The winner is: " + winningPlayer);
        winnerName.setName(winningPlayer);
        pokerRound.allPlayers[winningPlayerIndex].chips += pokerRound.currPot.pot;
        pokerRound.allPlayers = pokerRound.allPlayersCopyTwo;
        pokerRound.changeButton.gameObject.SetActive(true);
        

    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        if (pokerRound.doContinue == true)
        {
            winnerName.setName("none");
            pokerRound.changeButton.gameObject.SetActive(false);
            pokerRound.SwitchState(pokerRound.IntermissionState);
        }

        //pokerRound.SwitchState(pokerRound.BlindsState);
    }
}
