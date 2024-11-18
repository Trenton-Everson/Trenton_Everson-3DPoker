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
    Vector3 firstCardTransform;
    Quaternion firstCardRotation;
    Vector3 secondCardTransform;
    Quaternion secondCardRotation;
    bool flopShown = false;
    bool turnShown = false;
    bool riverShown = false;
    int handSize = 0;

    public cardStructure[] playerHand = new cardStructure[2];
    public cardStructure[] flopCards = new cardStructure[3];
    public cardStructure turnCard;
    public cardStructure riverCard;


    public cardStructure[] shuffleDeck(cardStructure[] deck)
    {
        deck = deck.OrderBy(x => UnityEngine.Random.value).ToArray();
        return deck;
    }
    public void Awake()
    {
        firstCardTransform = new Vector3(0.043f, 0.903f, -9.371f);
        secondCardTransform = new Vector3(-0.0231f, 0.903f, -9.371f);
        firstCardRotation = Quaternion.Euler(-39.985f, 0, 0);
        secondCardRotation = Quaternion.Euler(-39.985f, 0, 0);
        shuffledDeck = shuffleDeck(Deck);
    }
    public void Start()
    {
        

    }

    public cardStructure createCard(GameObject cardObject, String cardSuit, int cardRank)
    {
        return null;
    }

    public cardStructure Draw()
    {
        List<cardStructure> deckList = new List<cardStructure>(shuffledDeck);
        cardStructure drawnCard = shuffledDeck[0];

        
        deckList.RemoveAt(0);
        shuffledDeck = deckList.ToArray();

        return drawnCard;
    }

    public void addCardToHand()
    {
        if (handSize == 2)
        {
            print("Hand is full");
        }
        else
        {
            cardStructure sampleCard = Draw();
            
            GameObject sampleCardObject = Instantiate(sampleCard.card);

            if (handSize == 0)
            {
                sampleCardObject.transform.position = firstCardTransform;
                sampleCardObject.transform.rotation = firstCardRotation;
                playerHand[0] = sampleCard;
            }
            else if (handSize == 1)
            {
                sampleCardObject.transform.position = secondCardTransform;
                sampleCardObject.transform.rotation = secondCardRotation;
                playerHand[1] = sampleCard;
            }

            handSize++;
        }
    }
    public void flop()
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

            firstCardObject.transform.position = new Vector3(-0.1413f, 0.827545f, -9.0176f);
            secondCardObject.transform.position = new Vector3(-0.07549999f, 0.827545f, -9.0176f);
            thirdCardObject.transform.position = new Vector3(-0.009699985f, 0.827545f, -9.0176f);

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


            turnCardObject.transform.position = new Vector3(0.05610002f, 0.827545f, -9.0176f);
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
            riverCardObject.transform.position = new Vector3(0.1219f, 0.827545f, -9.0176f);
        
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
