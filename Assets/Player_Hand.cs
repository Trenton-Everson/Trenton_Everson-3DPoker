using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hand : MonoBehaviour
{
    public cardStructure[] playerHand = new cardStructure[2];
    deckActions deck = new deckActions();

    private void Start()
    {
        
    }

    public void AddCardsToHand()
    {
        //playerHand[0] = deck.Draw();
        //playerHand[1] = deck.Draw();
    }
}
