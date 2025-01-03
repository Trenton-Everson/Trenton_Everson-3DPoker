using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PokerPreflopState : PokerBaseState
{
    int currentPlayer = 0;
    public override void EnterState(PokerStateManager pokerRound)
    {
        //Player,4,3,1,2
        
        pokerRound.allPlayers[0] = pokerRound.playerHand;
        pokerRound.allPlayers[1] = pokerRound.bot4Hand;
        pokerRound.allPlayers[2] = pokerRound.bot3Hand;
        pokerRound.allPlayers[3] = pokerRound.bot1Hand;
        pokerRound.allPlayers[4] = pokerRound.bot2Hand;

        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            pokerRound.allPlayers[i].turnPosition = i;
        }
        pokerRound.bot2Hand.hasResponded = true; //Bot2 pays big blind originally so we dont check for a raise
        pokerRound.dealer.DealCards();
        Debug.Log("Current call is now: " + pokerRound.dealer.currentCall);
    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
            
                if (pokerRound.allPlayers[currentPlayer].myTurn == false)
                {
                Debug.Log("It is now " + pokerRound.allPlayers[currentPlayer].objectName + "'s Turn To Play");
                Debug.Log("They have " + pokerRound.allPlayers[currentPlayer].chips + " chips");
                pokerRound.allPlayers[currentPlayer].myTurn = true;
                }
                
                
                if (pokerRound.allPlayers[currentPlayer].fold == true 
                && pokerRound.allPlayers[currentPlayer].inGame == true 
                && pokerRound.allPlayers[currentPlayer].hasResponded == false)
                {
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " has Folded");
                    pokerRound.allPlayers[currentPlayer].inGame = false;
                    pokerRound.allPlayers[currentPlayer].myTurn = false;
                    currentPlayer++;
                }
                else if (pokerRound.allPlayers[currentPlayer].inGame == true 
                && pokerRound.allPlayers[currentPlayer].call == true 
                && pokerRound.allPlayers[currentPlayer].hasResponded == false)
                {
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " has Called");
                    pokerRound.allPlayers[currentPlayer].payCall(pokerRound.dealer.currentCall);
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " now has " + pokerRound.allPlayers[currentPlayer].chips + " chips");
                    pokerRound.allPlayers[currentPlayer].hasResponded = true;
                    pokerRound.allPlayers[currentPlayer].myTurn = false;
                    currentPlayer++;
                }
                else if (pokerRound.allPlayers[currentPlayer].inGame == true
                 && pokerRound.allPlayers[currentPlayer].raise == true 
                 && pokerRound.allPlayers[currentPlayer].hasResponded == false)
                {
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " has raised: " + pokerRound.allPlayers[currentPlayer].raiseAmount);
                    Debug.Log("Current call is now: " + pokerRound.dealer.currentCall);
                    Debug.Log("Player now has: " + pokerRound.allPlayers[currentPlayer].chips + " chips");
                    pokerRound.allPlayers[currentPlayer].raiseAmount = 0;
                    pokerRound.allPlayers[currentPlayer].hasResponded = true;
                    pokerRound.allPlayers[currentPlayer].myTurn = false;
                    Player_Hand[] newAllPlayers;
                    Player_Hand arrayCheck = pokerRound.allPlayers[currentPlayer];

                    while (true)
                    {
                    newAllPlayers = new Player_Hand[pokerRound.allPlayers.Length];
                    pokerRound.allPlayers = shift(pokerRound.allPlayers, newAllPlayers);
                    if (pokerRound.allPlayers[pokerRound.allPlayers.Length - 1] == arrayCheck) {break;}
                    }                    
                    List<Player_Hand> tList = new List<Player_Hand>();
                    for (int j = 0; j < pokerRound.allPlayers.Length; j++)
                    {
                        if (pokerRound.allPlayers[j].inGame == true)
                        {
                            tList.Add(pokerRound.allPlayers[j]);
                        }
                    }
                    pokerRound.allPlayers = tList.ToArray();

                    for (int j = 0; j < pokerRound.allPlayers.Length - 1; j++)
                    {
                        pokerRound.allPlayers[j].Reset();
                    }
                    currentPlayer = 0;
                }
                else if (currentPlayer == pokerRound.allPlayers.Length)
                {
                    Debug.Log("All players have responded moving to next round");
                }
            
        
        
    }
    public Player_Hand[] shift(Player_Hand[] givenArray, Player_Hand[] newArray)
    {
        
        for (int j = 0; j < givenArray.Length; j++)
                    {
                        if (j < givenArray.Length - 1)
                        {
                            newArray[j] = givenArray[j + 1];
                        }
                        else
                        {
                            newArray[j] = givenArray[0];
                        }
                    }

        return newArray;
    }
}
