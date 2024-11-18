using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Deck_Creation : MonoBehaviour
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
    }
    public void Start()
    {
        shuffledDeck = shuffleDeck(Deck);

    }

    public cardStructure createCard(GameObject cardObject, String cardSuit, int cardRank)
    {
        return null;
    }

    public void drawCard()
    {
        if (shuffledDeck.Length == 0)
        {
            print("No cards Left");
        }
        else if (handSize == 2)
        {
            print("Hand is full");
        }
        else
        {
            List<cardStructure> deckList = new List<cardStructure>(shuffledDeck);
            cardStructure sampleCard = shuffledDeck[0];
            
            GameObject sampleCardObject = Instantiate(sampleCard.card);
            

            deckList.RemoveAt(0);
            shuffledDeck = deckList.ToArray();
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
        List<cardStructure> deckList = new List<cardStructure>(shuffledDeck);

        cardStructure firstCard = shuffledDeck[0];
        cardStructure secondCard = shuffledDeck[1];
        cardStructure thirdCard = shuffledDeck[2];

        GameObject firstCardObject = Instantiate(shuffledDeck[0].card);
        GameObject secondCardObject = Instantiate(shuffledDeck[1].card);
        GameObject thirdCardObject = Instantiate(shuffledDeck[2].card);

        flopCards[0] = firstCard;
        flopCards[1] = secondCard;
        flopCards[2] = thirdCard;
        
        for (int i = 0; i < 3; i++)
        {
            deckList.RemoveAt(0);
        }

        firstCardObject.transform.position = new Vector3(-0.1413f, 0.827545f, -9.0176f);
        secondCardObject.transform.position = new Vector3(-0.07549999f, 0.827545f, -9.0176f);
        thirdCardObject.transform.position = new Vector3(-0.009699985f, 0.827545f, -9.0176f);

        shuffledDeck = deckList.ToArray();
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
        List<cardStructure> deckList = new List<cardStructure>(shuffledDeck);
        turnCard = shuffledDeck[0];
        GameObject turnCardObject = Instantiate(shuffledDeck[0].card);
        deckList.RemoveAt(0);
        shuffledDeck = deckList.ToArray();

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
        List<cardStructure> deckList = new List<cardStructure>(shuffledDeck);
        riverCard = shuffledDeck[0];
        GameObject riverCardObject = Instantiate(shuffledDeck[0].card);
        riverCardObject.transform.position = new Vector3(0.1219f, 0.827545f, -9.0176f);

        deckList.RemoveAt(0);
        shuffledDeck = deckList.ToArray();
        
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
