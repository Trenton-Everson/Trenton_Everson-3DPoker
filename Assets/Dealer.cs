using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Bot_Hand bot1Hand;
    Bot_Hand bot2Hand;
    Bot_Hand bot3Hand;
    Bot_Hand bot4Hand;

    void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
        player = GameObject.Find("Player");
        bot1 = GameObject.Find("Bot 1");
        bot2 = GameObject.Find("Bot 2");
        bot3 = GameObject.Find("Bot 3");
        bot4 = GameObject.Find("Bot 4");

        deckActions = deck.GetComponent<deckActions>();
        playerHand = player.GetComponent<Player_Hand>();
        bot1Hand = bot1.GetComponent<Bot_Hand>();
        bot2Hand = bot2.GetComponent<Bot_Hand>();
        bot3Hand = bot3.GetComponent<Bot_Hand>();
        bot4Hand = bot4.GetComponent<Bot_Hand>();
    }
    void Start()
    {

    }
    public void DealCards()
    {
        playerHand.AddCardsToHand();
        bot1Hand.BotHandCards();
        bot2Hand.BotHandCards();
        bot3Hand.BotHandCards();
        bot4Hand.BotHandCards();
    }
}
