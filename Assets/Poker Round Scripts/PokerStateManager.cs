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
    private GameObject player;
    private GameObject bot1;
    private GameObject bot2;
    private GameObject bot3;
    private GameObject bot4;
    private GameObject dealerObj;
    private GameObject deckObj;
    public Player_Hand playerHand;
    public Player_Hand bot1Hand;
    public Player_Hand bot2Hand;
    public Player_Hand bot3Hand;
    public Player_Hand bot4Hand;
    public Player_Hand[] allPlayers;
    public Player_Hand[] allPlayersCopy;
    public Dealer dealer;
    public deckActions deck;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BlindsState;
        player = GameObject.Find("Player");
        bot1 = GameObject.Find("Bot 1");
        bot2 = GameObject.Find("Bot 2");
        bot3 = GameObject.Find("Bot 3");
        bot4 = GameObject.Find("Bot 4");
        dealerObj = GameObject.Find("Dealer");
        deckObj = GameObject.Find("Red Deck Complete");

        dealer = dealerObj.GetComponent<Dealer>();
        playerHand = player.GetComponent<Player_Hand>();
        bot1Hand = bot1.GetComponent<Player_Hand>();
        bot2Hand = bot2.GetComponent<Player_Hand>();
        bot3Hand = bot3.GetComponent<Player_Hand>();
        bot4Hand = bot4.GetComponent<Player_Hand>();
        deck = deckObj.GetComponent<deckActions>();
        

        allPlayers = new Player_Hand[5];
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
}
