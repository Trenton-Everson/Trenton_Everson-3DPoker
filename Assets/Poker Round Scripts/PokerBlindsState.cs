using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class PokerBlindsState : PokerBaseState
{
    public override void EnterState(PokerStateManager pokerRound)
    {
        pokerRound.roundNamer.setName("Blinds");
        Player_Hand[] newAllPlayers = new Player_Hand[pokerRound.allPlayers.Length];
        pokerRound.allPlayers = shift(pokerRound.allPlayers, newAllPlayers);

        pokerRound.allPlayersCopyOne = pokerRound.allPlayers;
        pokerRound.allPlayersCopyTwo = pokerRound.allPlayers;

        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            pokerRound.allPlayers[i].turnPosition = i;
        }
        pokerRound.allPlayersCopyOne = pokerRound.allPlayers;
        //pokerRound.allPlayers[pokerRound.allPlayers.Length - 1].hasResponded = true;
        
        for (int i = 0; i < pokerRound.allPlayers.Length; i++)
        {
            if (pokerRound.allPlayers[i].chips == 0)
            {
                pokerRound.allPlayers[i].inGame = false;
                pokerRound.allPlayers[i].chips = 1000;

                List<Player_Hand> players = pokerRound.allPlayers.ToList<Player_Hand>();
                players.RemoveAt(i);
                i--;
                pokerRound.allPlayersCopyOne = players.ToArray();
                pokerRound.allPlayers = players.ToArray();
                if (pokerRound.allPlayers.Length == 1)
                {
                    pokerRound.allPlayers = pokerRound.allPlayersCopyTwo;
                    i = 10;
                }
            }
            else
            {
                pokerRound.allPlayers[i].inGame = true;
            }
        }
        pokerRound.allPlayers[pokerRound.allPlayers.Length - 2].payBlind(1);
        pokerRound.allPlayers[pokerRound.allPlayers.Length - 1].payBlind(0);
        Debug.Log("Blind State Ended");
        Debug.Log(pokerRound.allPlayers[pokerRound.allPlayers.Length - 2].name + " has paid the small Blind and " + pokerRound.allPlayers[pokerRound.allPlayers.Length - 1].name
         + " has Paid the big blind \n Moving to PreFlop");
        
        pokerRound.SwitchState(pokerRound.PreflopState);
    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        
        
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
