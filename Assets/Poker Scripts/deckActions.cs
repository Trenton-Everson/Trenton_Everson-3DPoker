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

    public cardStructure[] flopCards = new cardStructure[3];
    public cardStructure turnCard;
    public cardStructure riverCard;
    List<cardStructure> deckList;
    private deckCardPlacement deckCardPlacement;
    public GameObject FlopUICard1;
    public GameObject FlopUICard2;
    public GameObject FlopUICard3;
    public GameObject TurnUICard;
    public GameObject RiverUICard;

    GameObject sampleCard1;
    GameObject sampleCard2;
    GameObject sampleCard3;
    GameObject sampleCard4;
    GameObject sampleCard5;
    
   


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

/**
    public string drawSpecial()
    {
       int random = UnityEngine.Random.Range(0, 101);
       if (random <= 10)
       {
        return "redraw";
       }
       else if (random <= 20)
       {
        return "redraw";
       }
       else if (random <= 30)
       {
        return "redraw";
       }
       else if (random <= 40)
       {
        return "redraw";
       }
       else if (random <= 50)
       {
        return "redraw";
       }
       else if (random <= 60)
       {
        return "redraw";
       }
       else if (random <= 70)
       {
        return "redraw";
       }
       else if (random <= 80)
       {
        return "redraw";
       }
       else if (random <= 90)
       {
        return "redraw";
       }
       else if (random <= 100)
       {
        return "redraw";
       }
       else
       {
        return null;
       }
    }
**/
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

            firstCardObject.transform.SetParent(this.transform);
            secondCardObject.transform.SetParent(this.transform);
            thirdCardObject.transform.SetParent(this.transform);


            firstCardObject.transform.position = deckCardPlacement.firstFlopCardPlacement;
            firstCardObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            secondCardObject.transform.position = deckCardPlacement.secondFlopCardPlacement;
            secondCardObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            thirdCardObject.transform.position = deckCardPlacement.thirdFlopCardPlacement;
            thirdCardObject.transform.rotation = Quaternion.Euler(0, 0, 0);


            sampleCard1 = Instantiate(flopCards[0].card, this.transform);
            sampleCard1.transform.position = FlopUICard1.transform.position;
            sampleCard1.transform.rotation = Quaternion.Euler(90, 0, 180);
            sampleCard1.transform.localScale = new Vector3(16.5f, 16.5f, 16.5f);
            sampleCard1.layer = 5;
            FlopUICard1.SetActive(false);

            sampleCard2 = Instantiate(flopCards[1].card, this.transform);
            sampleCard2.transform.position = FlopUICard2.transform.position;
            sampleCard2.transform.rotation = Quaternion.Euler(90, 0, 180);
            sampleCard2.transform.localScale = new Vector3(16.5f, 16.5f, 16.5f);
            sampleCard2.layer = 5;
            FlopUICard2.SetActive(false);

            sampleCard3 = Instantiate(flopCards[2].card, this.transform);
            sampleCard3.transform.position = FlopUICard3.transform.position;
            sampleCard3.transform.rotation = Quaternion.Euler(90, 0, 180);
            sampleCard3.transform.localScale = new Vector3(16.5f, 16.5f, 16.5f);
            sampleCard3.layer = 5;
            FlopUICard3.SetActive(false);

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
            turnCardObject.transform.SetParent(this.transform);


            turnCardObject.transform.position = deckCardPlacement.turnCardPlacement;
            turnCardObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            turnShown = true;

            sampleCard4 = Instantiate(turnCard.card, this.transform);
            sampleCard4.transform.position = TurnUICard.transform.position;
            sampleCard4.transform.rotation = Quaternion.Euler(90, 0, 180);
            sampleCard4.transform.localScale = new Vector3(16.5f, 16.5f, 16.5f);
            sampleCard4.layer = 5;
            TurnUICard.SetActive(false);
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
            riverCardObject.transform.SetParent(this.transform);
            riverCardObject.transform.position = deckCardPlacement.riverCardPlacement;
            riverCardObject.transform.rotation = Quaternion.Euler(0, 0, 0);

            sampleCard5 = Instantiate(riverCard.card, this.transform);
            sampleCard5.transform.position = RiverUICard.transform.position;
            sampleCard5.transform.rotation = Quaternion.Euler(90, 0, 180);
            sampleCard5.transform.localScale = new Vector3(16.5f, 16.5f, 16.5f);
            sampleCard5.layer = 5;
            RiverUICard.SetActive(false);
        
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
    
    public void resetDeck()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        shuffledDeck = shuffleDeck(Deck);
        deckList = new List<cardStructure>(shuffledDeck);
        flopShown = false;
        riverShown = false;
        turnShown = false;
        FlopUICard1.SetActive(true);
        FlopUICard2.SetActive(true);
        FlopUICard3.SetActive(true);
        TurnUICard.SetActive(true);
        RiverUICard.SetActive(true);
    }
}


[System.Serializable]
public class cardStructure
{
    public GameObject card;
    public string suit;
    public int rank;
}
