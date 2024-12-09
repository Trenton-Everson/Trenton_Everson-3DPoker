using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerStateManager : MonoBehaviour
{
    public PokerBaseState currentState;
    public PokerBlindsState BlindsState = new PokerBlindsState();
    public PokerPreflopState PreflopState = new PokerPreflopState();
    public PokerFlopState FlopState = new PokerFlopState();
    public PokerTurnState TurnState = new PokerTurnState();
    public PokerRiverState RiverState = new PokerRiverState();
    public PokerShowdownState ShowdownState = new PokerShowdownState();
    public PokerIntermissionState IntermissionState = new PokerIntermissionState();
    private GameObject player;
    private GameObject bot1;
    private GameObject bot2;
    private GameObject bot3;
    private GameObject bot4;
    private GameObject dealerObj;
    private GameObject deckObj;
    private GameObject potObj;
    public Player_Hand playerHand;
    public Player_Hand bot1Hand;
    public Player_Hand bot2Hand;
    public Player_Hand bot3Hand;
    public Player_Hand bot4Hand;
    public Player_Hand[] allPlayers;
    public Player_Hand[] allPlayersCopyOne; //Array for turn order
    public Player_Hand[] allPlayersCopyTwo;
    public Dealer dealer;
    public deckActions deck;
    public CurrentPot currPot;
    public roundName roundNamer;
    
    public bool doContinue = false;
    public GameObject changeButton;

    // Start is called before the first frame update
    void Start()
    {
        changeButton.gameObject.SetActive(false);
        currentState = BlindsState;
        player = GameObject.Find("Player");
        bot1 = GameObject.Find("Bot 1");
        bot2 = GameObject.Find("Bot 2");
        bot3 = GameObject.Find("Bot 3");
        bot4 = GameObject.Find("Bot 4");
        dealerObj = GameObject.Find("Dealer");
        deckObj = GameObject.Find("Red Deck Complete");
        potObj = GameObject.Find("Pot Ammount");


        dealer = dealerObj.GetComponent<Dealer>();
        playerHand = player.GetComponent<Player_Hand>();
        bot1Hand = bot1.GetComponent<Player_Hand>();
        bot2Hand = bot2.GetComponent<Player_Hand>();
        bot3Hand = bot3.GetComponent<Player_Hand>();
        bot4Hand = bot4.GetComponent<Player_Hand>();
        deck = deckObj.GetComponent<deckActions>();
        currPot = potObj.GetComponent<CurrentPot>();

        

        allPlayers = new Player_Hand[5];

        allPlayers[0] = bot2Hand;
        allPlayers[1] = playerHand;
        allPlayers[2] = bot4Hand;
        allPlayers[3] = bot3Hand;
        allPlayers[4] = bot1Hand;
        
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PokerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    public void changeThing()
    {
        doContinue = true;
    }
}
