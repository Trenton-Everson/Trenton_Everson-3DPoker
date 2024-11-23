using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HandChecker : MonoBehaviour
{
    deckActions deckActions;
    Player_Hand player_Hand;
    private GameObject deck;
    private GameObject player;
    cardStructure[] cardsOnTable = new cardStructure[5];
    public void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
        player = GameObject.Find("Player");
    }
    public void Start()
    {
        deckActions = deck.GetComponent<deckActions>();
        player_Hand = player.GetComponent<Player_Hand>();
        
    }
    public void testingMethod()
    {
        for (int i = 0; i < 3; i++)
        {
            cardsOnTable[i] = deckActions.flopCards[i];
        }
        cardsOnTable[3] = deckActions.turnCard;
        cardsOnTable[4] = deckActions.riverCard;
        
        CheckHand(player_Hand.playerHand, cardsOnTable);
    }

    
    public int CheckHand(cardStructure[] givenHand, cardStructure[] cardsOnTable)
    {
        int[] cardRanks = new int[13];
        int[] cardSuits = new int[4];
        int[,] allCards = new int[cardRanks.Length,cardSuits.Length];
        cardStructure[] fullCards = new cardStructure[7];

        for (int i = 0; i < 2; i++)
        {
            fullCards[i] = givenHand[i];
        }
        for (int i = 2; i < 7; i++)
        {
            fullCards[i] = cardsOnTable[i - 2];
        }


        //Fill rank array for hand grading purpouses 
        for (int i = 0; i < fullCards.Length; i++)
        {
            cardRanks[fullCards[i].rank - 1] += 1;
        }

        //Fill suit array for hand grading purpouses
        for (int i = 0; i < fullCards.Length; i++)
        {
            //Give current card suit and have it be changed into a number and increment the
            //cardRanks array accordingly
            cardSuits[SuitsToNumberConversion(fullCards[i].suit)] += 1;
        }


        for (int i = 0; i < fullCards.Length; i++)
        {
            for (int j = 0; j < cardRanks.Length; j++)
            {
                for (int k = 0; k < cardSuits.Length; k++)
                {
                    if (j == fullCards[i].rank && k == SuitsToNumberConversion(fullCards[i].suit))
                    {
                        allCards[j, k] = 1;
                    }
                }
            }
        }
        return 0;
    }

    int SuitsToNumberConversion(string cardSuit)
    {
        if (cardSuit == "Clubs")
        {
            return 0;
        }
        if (cardSuit == "Diamonds")
        {
            return 1;
        }
        if (cardSuit == "Hearts")
        {
            return 2;
        }
        if (cardSuit == "Spades")
        {
            return 3;
        }
        else
        {
            return -1;
        }
    }
}
