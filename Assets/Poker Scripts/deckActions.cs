using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class deckActions : MonoBehaviour
{
    public cardStructure[] Deck = new cardStructure[52];
    public cardStructure[] shuffledDeck;
    bool flopShown = false;
    bool turnShown = false;
    bool riverShown = false;
    //int handSize = 0;

    public cardStructure[] playerHand = new cardStructure[2];
    public cardStructure[] flopCards = new cardStructure[3];
    public cardStructure turnCard;
    public cardStructure riverCard;
    List<cardStructure> deckList;
    private deckCardPlacement deckCardPlacement;
    
   


    public cardStructure[] shuffleDeck(cardStructure[] deck)
    {
        deck = deck.OrderBy(x => UnityEngine.Random.value).ToArray();
        return deck;
    }
    public void Awake()
    {
        shuffledDeck = shuffleDeck(Deck);
        deckList = new List<cardStructure>(shuffledDeck);
        deckCardPlacement = GetComponent<deckCardPlacement>();
    }
    public void Start()
    {
        
    }

    public cardStructure Draw()
    {
        cardStructure drawnCard = deckList[0];

        
        deckList.RemoveAt(0);
        shuffledDeck = deckList.ToArray();

        return drawnCard;
    }

    public void Flop()
    {
        if (!flopShown)
        {

            cardStructure firstCard = Draw();
            cardStructure secondCard = Draw();
            cardStructure thirdCard = Draw();

            GameObject firstCardObject = Instantiate(firstCard.card);
            GameObject secondCardObject = Instantiate(secondCard.card);
            GameObject thirdCardObject = Instantiate(thirdCard.card);

            flopCards[0] = firstCard;
            flopCards[1] = secondCard;
            flopCards[2] = thirdCard;


            firstCardObject.transform.position = deckCardPlacement.firstFlopCardPlacement;
            secondCardObject.transform.position = deckCardPlacement.secondFlopCardPlacement;
            thirdCardObject.transform.position = deckCardPlacement.thirdFlopCardPlacement;

            flopShown = true;
        }
        else
        {
            print("flop already shown");
        }
    }
    public void turn()
    {
        if (!turnShown)
        {
            turnCard = Draw();
            GameObject turnCardObject = Instantiate(turnCard.card);


            turnCardObject.transform.position = deckCardPlacement.turnCardPlacement;
            turnShown = true;
        }
        else
        {
            print("turn already shown");
        }
    }

    public void river()
    {
        if (!riverShown)
        {
            riverCard = Draw();
            GameObject riverCardObject = Instantiate(riverCard.card);
            riverCardObject.transform.position = deckCardPlacement.riverCardPlacement;
        
            riverShown = true;
        }
        else
        {
            print("river already shown");
        }
    }

    public void shuffleAndDisplayContents()
    {
        shuffledDeck = shuffleDeck(Deck);
        for (int i = 0; i < shuffledDeck.Length; i++)
        {
            print("Card " + i + " is: " + shuffledDeck[i].rank + " of " + shuffledDeck[i].suit);
        }
    }
}


[System.Serializable]
public class cardStructure
{
    public GameObject card;
    public string suit;
    public int rank;
}
