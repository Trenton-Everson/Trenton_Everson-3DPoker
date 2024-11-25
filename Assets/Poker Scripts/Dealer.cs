using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Dealer : MonoBehaviour
{
    private GameObject deck;
    private GameObject player;
    private GameObject bot1;
    private GameObject bot2;
    private GameObject bot3;
    private GameObject bot4;
    deckActions deckActions;
    Player_Hand playerHand;
    Player_Hand bot1Hand;
    Player_Hand bot2Hand;
    Player_Hand bot3Hand;
    Player_Hand bot4Hand;
    private GameObject handChecker;
    HandChecker hand_Checker;
    int currentRaise = 0;

    void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
        player = GameObject.Find("Player");
        bot1 = GameObject.Find("Bot 1");
        bot2 = GameObject.Find("Bot 2");
        bot3 = GameObject.Find("Bot 3");
        bot4 = GameObject.Find("Bot 4");
        handChecker = GameObject.Find("Hand Checker");

        deckActions = deck.GetComponent<deckActions>();
        playerHand = player.GetComponent<Player_Hand>();
        bot1Hand = bot1.GetComponent<Player_Hand>();
        bot2Hand = bot2.GetComponent<Player_Hand>();
        bot3Hand = bot3.GetComponent<Player_Hand>();
        bot4Hand = bot4.GetComponent<Player_Hand>();
        hand_Checker = handChecker.GetComponent<HandChecker>();
    }
    void Start()
    {

    }
    public void DealCards()
    {
        playerHand.AddCardsToHand("Player");
        bot1Hand.AddCardsToHand("Bot 1");
        bot2Hand.AddCardsToHand("Bot 2");
        bot3Hand.AddCardsToHand("Bot 3");
        bot4Hand.AddCardsToHand("Bot 4");
    }

    public void CheckHands()
    {
        print("Player's " + hand_Checker.testingMethod(playerHand.playerHand));
        print("Bot 1's " + hand_Checker.testingMethod(bot1Hand.playerHand));
        print("Bot 2's " + hand_Checker.testingMethod(bot2Hand.playerHand));
        print("Bot 3's " + hand_Checker.testingMethod(bot3Hand.playerHand));
        print("Bot 4's " + hand_Checker.testingMethod(bot4Hand.playerHand));
        print("");
    }
}
