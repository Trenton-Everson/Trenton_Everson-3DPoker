using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HandChecker : MonoBehaviour
{
    deckActions deckActions;
    private GameObject deck;
    cardStructure[] cardsOnTable = new cardStructure[5];
    int[] cardRanks;
    int[] cardSuits;
    int[,] allCards;
    cardStructure[] fullCards;
    int checkVariable;
    string whatIsHand;
    public void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
    }
    public void Start()
    {
        deckActions = deck.GetComponent<deckActions>();
        
    }
    public string testingMethod(cardStructure[] givenHand)
    {
        whatIsHand = "";
        checkVariable = 0;
        if (deckActions.flopCards[0].card != null){for (int i = 0; i < 3; i++){cardsOnTable[i] = deckActions.flopCards[i];} checkVariable = 3;}
        if (deckActions.turnCard.card != null){cardsOnTable[3] = deckActions.turnCard; checkVariable++;}
        if (deckActions.riverCard.card != null){cardsOnTable[4] = deckActions.riverCard; checkVariable++;}

        cardRanks = new int[13];
        cardSuits = new int[4];
        allCards = new int[13,4];

        fullCards = new cardStructure[givenHand.Length + checkVariable];
        int[,] allCardsAsNum = CheckHand(givenHand, cardsOnTable);

        int cardScore = -1;
        cardScore = CheckRoyalFlush(allCardsAsNum);
        if (cardScore == -1){cardScore = CheckStraightFlush(allCardsAsNum);}
        if (cardScore == -1){cardScore = CheckFourOfAKind(cardRanks);}
        if (cardScore == -1){cardScore = CheckFullHouse(cardRanks);}
        if (cardScore == -1){cardScore = CheckFlush(allCardsAsNum);}
        if (cardScore == -1){cardScore = CheckStraight(cardRanks);}
        if (cardScore == -1){cardScore = CheckThreeOfAKind(cardRanks);}
        if (cardScore == -1){cardScore = CheckTwoPair(cardRanks);}
        if (cardScore == -1){cardScore = CheckHighCard(cardRanks);}
        
        return whatIsHand + "|||" + cardScore;

    }
    public int[,] CheckHand(cardStructure[] givenHand, cardStructure[] cardsOnTable){
        
        for (int i = 0; i < 2; i++){fullCards[i] = givenHand[i];}
        for (int i = 2; i < fullCards.Length; i++){
            fullCards[i] = cardsOnTable[i - 2];}
        //Fill rank array for hand grading purpouses 
        for (int i = 0; i < fullCards.Length; i++){
            cardRanks[fullCards[i].rank - 1] += 1;}
        //Give current card suit and have it be changed into a number and increment the
        //cardallCards array accordingly
        for (int i = 0; i < fullCards.Length; i++){cardSuits[SuitsToNumberConversion(fullCards[i].suit)] += 1;}
        //Add cards to the allCards array to makje easier to grade hand
        for (int i = 0; i < fullCards.Length; i++){
            for (int j = 0; j < 13; j++){
                for (int k = 0; k < 4; k++){
                    if (j == fullCards[i].rank - 1 && k == SuitsToNumberConversion(fullCards[i].suit)){
                        allCards[j, k] = 1;
        }   }   }   }
        return allCards;
    }
    int SuitsToNumberConversion(string cardSuit){
        if (cardSuit == "Clubs")    {return 0;}
        if (cardSuit == "Diamonds") {return 1;}
        if (cardSuit == "Hearts")   {return 2;}
        if (cardSuit == "Spades")   {return 3;}
        else                        {return -1;}
    }
    //[Number, Icon]
    private int CheckRoyalFlush(int[,] givenHand)
    {
        for (int i = 0; i < 4; i++)
		{
            if (givenHand[0, i] + givenHand[9, i] + givenHand[10, i] + givenHand[11, i] + givenHand[12, i] == 5){
                whatIsHand = "Royal Flush!";
                return 20000;
            } 
        }
        return -1;
    }
    private int CheckStraightFlush(int[,] givenHand)
    {
        for (int i = 0; i < 4; i++)
		{
            for (int j = 8; j >= 0; j--)
            {
				if (givenHand[j, i] + givenHand[j + 1, i] + givenHand[j + 2, i] + givenHand[j + 3, i] + givenHand[j + 4, i] == 5)
				{
                    whatIsHand = "Straight Flush!";
                    return 18000 + ((j + 4) * 100);
                } 
			}
        }
        return -1;
    }
    private int CheckFourOfAKind(int[] givenHand)
    {
        for (int i = 0; i < givenHand.Length; i++)
		{
            if (givenHand[i] >= 4)
            {
                if (i == 0)
                {
                    whatIsHand = "Four of a Kind!";
                    return 1600 + 1300;
                }
                whatIsHand = "Four of a Kind!";
                return 16000 + (i * 100);
            }
        }
        return -1;
    }
    private int CheckFullHouse(int[] givenHand)
    {
        for (int i = 0; i < givenHand.Length; i++)
		{
            if (givenHand[i] == 3)
            {
                for (int j = 0; j < givenHand.Length; j++)
		        {
                    if (givenHand[j] == 2)
                    {
                        if (i == 0)
                        {
                            whatIsHand =  "Full House!";
                            return 14000 + 1300;
                        }
                        whatIsHand = "Full House!";
                        return 14000 + (i * 100);
                    }
                }
            }
        }
        return -1;
    }
    private int CheckFlush(int[,] allCards)
    {
        for (int i = 0; i < 4; i++)
		{
			if (allCards[0, i] + allCards[1, i] + allCards[2, i] + allCards[3, i] + allCards[4, i] + allCards[5, i] + allCards[6, i] + allCards[7, i]
				+ allCards[8, i] + allCards[9, i] + allCards[10, i] + allCards[11, i] + allCards[12, i] >= 5)
			{
                if (allCards[0, i] == 1)
                {
                    whatIsHand = "Flush!";
                    return 12000 + 1300;
                }
				for (int j = 12; j <= 0; j--)
				{
					if (allCards[j, i] == 1)
					{
                        whatIsHand = "Flush!";
						return 12000 + (j * 100);
					}
				}
			}

		}

        return -1;
    }
    private int CheckStraight(int[] givenHand)
    {
        for (int i = 8; i >= 0 ; i--)
        {
            if (givenHand[i + 1] > 0)
            {
                if (givenHand[i + 2] > 0)
                {
                    if (givenHand[i + 3] > 0)
                    {
                        if (givenHand[i + 4] > 0)
                        {
                            if (givenHand[0] > 0 && i == 8)
                            {
                                whatIsHand = "Straight!";
                                return 1300 + 10000;
                            }
                            if (givenHand[i] > 0)
                            {
                                whatIsHand = "Straight!";
                                return ((i + 4) * 100) + 10000;
                            }
                        }
                    }
                }
            }
        }
        return -1;
    }
    private int CheckThreeOfAKind(int[] givenHand)
    {
        for (int i = 0; i < givenHand.Length; i++)
        {
            if (i == 0 && givenHand[i] == 3)
            {
                whatIsHand = "Three of a Kind!";
                return 8000 + 1300;
            }
            if (givenHand[i] == 3)
            {
                whatIsHand = "Three of a Kind!";
                return 8000 + (i * 100);
            }
        }
        return -1;
    }
    private int CheckTwoPair(int[] givenHand)
    {
        for (int i = 0; i < givenHand.Length; i++)
        {
            if (givenHand[i] == 2)
            {
                for (int j = 0; j < givenHand.Length; j++)
                {
                    if (givenHand[j] == 2 && i != j)
                    {
                        if (i == 0 && givenHand[i] == 2)
                        {
                            whatIsHand = "Two Pair!";
                            return 6000 + 1300;
                        }

                        if (i > j){whatIsHand = "Two Pair!"; return 6000 + (i * 100);}
                        else if (i < j){whatIsHand = "Two Pair!"; return 6000 + (j * 100);}
                    }
                }
                whatIsHand = "Pair!";
                return 4000 + (i * 100);
            }
        }
        return -1;
    }
    private int CheckHighCard(int[] givenHand)
    {
        for (int i = givenHand.Length - 1; i >= 0 ; i--)
        {
            if (givenHand[0] == 1)
            {
                whatIsHand = "High Card!";
                return (13 * 100) + 2000;
            }
            if (givenHand[i] == 1 && i != 0)
            {
                whatIsHand = "High Card!";
                return (i * 100) + 2000;
            }
        }
        return -1;
    }


}
