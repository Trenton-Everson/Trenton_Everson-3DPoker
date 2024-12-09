using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PokerPreflopState : PokerBaseState
{
    int currentPlayer;
    public override void EnterState(PokerStateManager pokerRound)
    {
        pokerRound.roundNamer.setName("Preflop");
        currentPlayer = 0;
        //Player,4,3,1,2
        pokerRound.dealer.DealCards();
        Debug.Log("Current call is now: " + pokerRound.dealer.currentCall);
    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
            
                if (pokerRound.allPlayers[currentPlayer].myTurn == false && pokerRound.allPlayers[currentPlayer].allIn == false)
                {
                Debug.Log("It is now " + pokerRound.allPlayers[currentPlayer].objectName + "'s Turn To Play");
                Debug.Log("They have " + pokerRound.allPlayers[currentPlayer].chips + " chips");
                pokerRound.allPlayers[currentPlayer].myTurn = true;
                }
                
                
                if (pokerRound.allPlayers[currentPlayer].fold == true 
                && pokerRound.allPlayers[currentPlayer].inGame == true 
                && pokerRound.allPlayers[currentPlayer].hasResponded == true
                && pokerRound.allPlayers[currentPlayer].allIn == false)
                {
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " has Folded");
                    pokerRound.allPlayers[currentPlayer].inGame = false;
                    pokerRound.allPlayers[currentPlayer].myTurn = false;
                    if (currentPlayer != pokerRound.allPlayers.Length -1)
                    {
                    currentPlayer++;
                    }
                }
                else if (pokerRound.allPlayers[currentPlayer].inGame == true 
                && pokerRound.allPlayers[currentPlayer].call == true 
                && pokerRound.allPlayers[currentPlayer].hasResponded == true
                && pokerRound.allPlayers[currentPlayer].allIn == false)
                {
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " has Called");
                    pokerRound.allPlayers[currentPlayer].payCall(pokerRound.dealer.currentCall);
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " now has " + pokerRound.allPlayers[currentPlayer].chips + " chips");
                    //pokerRound.allPlayers[currentPlayer].hasResponded = true;
                    pokerRound.allPlayers[currentPlayer].myTurn = false;

                    if (currentPlayer != pokerRound.allPlayers.Length -1)
                    {
                    currentPlayer++;
                    }
                }
                else if (pokerRound.allPlayers[currentPlayer].inGame == true
                 && pokerRound.allPlayers[currentPlayer].raise == true 
                 && pokerRound.allPlayers[currentPlayer].hasResponded == true
                 && pokerRound.allPlayers[currentPlayer].allIn == false)
                {
                    Debug.Log(pokerRound.allPlayers[currentPlayer].objectName + " has raised: " + pokerRound.allPlayers[currentPlayer].raiseAmount);
                    Debug.Log("Current call is now: " + pokerRound.dealer.currentCall);
                    Debug.Log("Player now has: " + pokerRound.allPlayers[currentPlayer].chips + " chips");
                    pokerRound.allPlayers[currentPlayer].raiseAmount = 0;
                    //pokerRound.allPlayers[currentPlayer].hasResponded = true;
                    pokerRound.allPlayers[currentPlayer].myTurn = false;
                    pokerRound.allPlayers[currentPlayer].raise = false;
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
                if (pokerRound.allPlayers[currentPlayer].hasResponded == true && currentPlayer == pokerRound.allPlayers.Length - 1)
                {
                    
                    Player_Hand[] newAllPlayers;
                    Player_Hand arrayCheck = pokerRound.allPlayersCopyOne[0];
                    
                    for (int i = 0; i < pokerRound.allPlayers.Length; i++)
                    {
                        if (pokerRound.allPlayers[i].fold == false)
                        {
                            arrayCheck = pokerRound.allPlayers[i];
                            i = pokerRound.allPlayersCopyOne.Length;
                        }
                    }
                    
                    

                    while (true)
                    {
                    newAllPlayers = new Player_Hand[pokerRound.allPlayers.Length];
                    pokerRound.allPlayers = shift(pokerRound.allPlayers, newAllPlayers);
                    if (pokerRound.allPlayers[0] == arrayCheck) {break;}
                    }
                    List<Player_Hand> tempList = new List<Player_Hand>();

                    for (int i = 0; i < pokerRound.allPlayers.Length; i++)
                    {
                        if (pokerRound.allPlayers[i].fold == false)
                        {
                            tempList.Add(pokerRound.allPlayers[i]);
                        }
                    }
                    pokerRound.allPlayers = tempList.ToArray();

                    for (int i = 0; i < pokerRound.allPlayers.Length; i++)
                    {
                        pokerRound.allPlayers[i].Reset();
                    }
                    

                    Debug.Log("All players have responded moving to next round");
                    pokerRound.SwitchState(pokerRound.FlopState);
                }
                else if (pokerRound.allPlayers[currentPlayer].inGame == false)
                {
                    currentPlayer ++;
                }
                else if (pokerRound.allPlayers[currentPlayer].allIn == true)
                {
                    pokerRound.allPlayers[currentPlayer].hasResponded = true;
                    if (currentPlayer != pokerRound.allPlayers.Length - 1)
                    {
                    currentPlayer++;
                    }
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
