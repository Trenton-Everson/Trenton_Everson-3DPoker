using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Player_Hand : MonoBehaviour
{
    public cardStructure[] playerHand = new cardStructure[2];
    private GameObject deck;
    [HideInInspector] public deckActions deckActions;
    private GameObject potAmmount;
    private GameObject handCheckerObj;
    [HideInInspector] public string objectName;
    CurrentPot currentPot;
    public Vector3 firstCardInHandPosition;
    public Vector3 secondCardInHandPosition;
    public Vector3 firstCardInHandRotation;
    public Vector3 secondCardInHandRotation;
    
    public Vector3 firstCardFoldedPosition;
    public Vector3 secondCardFoldedPosition;
    public Vector3 firstCardFoldedRotation;
    public Vector3 secondCardFoldedRotation;

    [HideInInspector] public bool check = false;
    [HideInInspector] public bool fold = false;
    [HideInInspector] public bool call = false;
    [HideInInspector] public bool raise = false;
 public int chips = 1000;
    [HideInInspector] public bool inGame = true;
    [HideInInspector] public bool hasResponded = false;
    [HideInInspector] public int turnPosition;
    [HideInInspector] public bool myTurn = false;
    [HideInInspector] public int raiseAmount = 0;
    [HideInInspector] public int chipsInThisTurn = 0;
    GameObject dealerObj;
    [HideInInspector] public Dealer dealer;
    [HideInInspector] public HandChecker handChecker;
    [HideInInspector] public int handScore = 0;
    GameObject sampleCard1 = null;
    GameObject sampleCard2 = null;
    public GameObject chipObj;
    public GameObject UICard1Transform;
    public GameObject UICard2Transform;
    GameObject sampleCard3 = null;
    GameObject sampleCard4 = null;
    //string lastAction;
    public GameObject playerLastAction;
    [HideInInspector] public bool allIn = false;


    private void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
        potAmmount = GameObject.Find("Pot Ammount");
        handCheckerObj = GameObject.Find("Hand Checker");
        objectName = gameObject.name;
        dealerObj = GameObject.Find("Dealer");
        dealer = dealerObj.GetComponent<Dealer>();
        handChecker = handCheckerObj.GetComponent<HandChecker>();
    }
    private void Start()
    {

        deckActions = deck.GetComponent<deckActions>();
        currentPot = potAmmount.GetComponent<CurrentPot>();
        chipObj.GetComponent<Chips>().UpdateChips(chips);
    }
    void Update()
    {
        chipObj.GetComponent<Chips>().UpdateChips(chips);
    }

    public void AddCardsToHand(string name)
    {
        if (inGame == true) 
        {
        Destroy(sampleCard1);
        
        playerHand[0] = deckActions.Draw();
        playerHand[1] = deckActions.Draw();
        sampleCard1 = Instantiate(playerHand[0].card, this.transform);
        sampleCard1.transform.position = firstCardInHandPosition;
        sampleCard1.transform.rotation = Quaternion.Euler(firstCardInHandRotation);

        Destroy(sampleCard2);
        sampleCard2 = Instantiate(playerHand[1].card, this.transform);
        sampleCard2.transform.position = secondCardInHandPosition;
        sampleCard2.transform.rotation = Quaternion.Euler(secondCardInHandRotation);
        }
    }
    public void PlayerFold()
    {
        if (myTurn == true)
        {
        Destroy(sampleCard1);
        sampleCard1 = Instantiate(playerHand[0].card, this.transform);
        sampleCard1.transform.position = firstCardFoldedPosition;
        sampleCard1.transform.rotation = Quaternion.Euler(firstCardFoldedRotation);

        Destroy(sampleCard2);
        sampleCard2 = Instantiate(playerHand[1].card, this.transform);
        sampleCard2.transform.position = secondCardFoldedPosition;
        sampleCard2.transform.rotation = Quaternion.Euler(secondCardFoldedRotation);
        fold = true;
        hasResponded = true;
        playerLastAction.GetComponent<lastAction>().setLastAction("Folded");
        }
    }
    public void showCards()
    {
        Destroy(sampleCard1);
        sampleCard1 = Instantiate(playerHand[0].card, this.transform);
        sampleCard1.transform.position = firstCardFoldedPosition;
        sampleCard1.transform.rotation = Quaternion.Euler(0, firstCardFoldedRotation.y, firstCardFoldedRotation.z);

        Destroy(sampleCard2);
        sampleCard2 = Instantiate(playerHand[1].card, this.transform);
        sampleCard2.transform.position = secondCardFoldedPosition;
        sampleCard2.transform.rotation = Quaternion.Euler(0, secondCardFoldedRotation.y, secondCardFoldedRotation.z);

        sampleCard3 = Instantiate(playerHand[0].card, this.transform);
        sampleCard3.transform.position = UICard1Transform.transform.position;
        sampleCard3.transform.rotation = Quaternion.Euler(90, 0, 180);
        sampleCard3.transform.localScale = UICard1Transform.transform.localScale;
        sampleCard3.layer = 5;
        UICard1Transform.SetActive(false);

        sampleCard4 = Instantiate(playerHand[1].card, this.transform);
        sampleCard4.transform.position = UICard2Transform.transform.position;
        sampleCard4.transform.rotation = Quaternion.Euler(90, 0, 180);
        sampleCard4.transform.localScale = UICard2Transform.transform.localScale;
        sampleCard4.layer = 5;
        UICard2Transform.SetActive(false);
    }
    public void PlayerCheck()
    {
        if (myTurn == true)
        {
        check = true;
        hasResponded = true;
        playerLastAction.GetComponent<lastAction>().setLastAction("Check");
        }
    }
    public void PlayerCall()
    {
        if (myTurn == true)
        {
        call = true;
        hasResponded = true;
        playerLastAction.GetComponent<lastAction>().setLastAction("Called");
        }
    }
    public void PlayerRaise(TMP_InputField potAmmount)
    {
        if (myTurn == true)
        {
            if (inGame == true && hasResponded == false)
            {
                int changePot = 0;
                try 
                {
                    raiseAmount = int.Parse(potAmmount.text);
                    if (raiseAmount < chips - dealer.currentCall)
                    {

                    dealer.currentCall += raiseAmount;
                    chipsInThisTurn += raiseAmount;
                    raiseAmount += dealer.currentCall - chipsInThisTurn;
                    
                    chips -= raiseAmount;
                    changePot = raiseAmount;
                    raise = true;
                    hasResponded = true;
                    playerLastAction.GetComponent<lastAction>().setLastAction("Raised");
                    }
                    else if (raiseAmount == chips - dealer.currentCall)
                    {
                    dealer.currentCall += raiseAmount;
                    chipsInThisTurn += raiseAmount;
                    raiseAmount += dealer.currentCall - chipsInThisTurn;
                    
                    chips -= raiseAmount;
                    changePot = raiseAmount;
                    raise = true;
                    hasResponded = true;
                    allIn = true;
                    playerLastAction.GetComponent<lastAction>().setLastAction("Raised All In");  
                    }
                    else
                    {
                        Debug.Log("You do not have that many chips");
                    }
                }
                catch (Exception e)
                {
                    print(e);
                    return;
                }
                currentPot.ChangePot(changePot);
            }
        }
    }
    
    public void PlayerRaise(int potAmmount)
    {
        if (myTurn == true)
        {
            if (inGame == true && hasResponded == false)
            {
                int changePot = 0;
                try 
                {
                    raiseAmount = potAmmount;
                    if (raiseAmount < chips - dealer.currentCall)
                    {

                    dealer.currentCall += raiseAmount;
                    chipsInThisTurn += raiseAmount;
                    raiseAmount += dealer.currentCall - chipsInThisTurn;
                    
                    chips -= raiseAmount;
                    changePot = raiseAmount;
                    raise = true;
                    hasResponded = true;
                    playerLastAction.GetComponent<lastAction>().setLastAction("Raised");
                    }
                    else if (raiseAmount == chips - dealer.currentCall)
                    {
                    dealer.currentCall += raiseAmount;
                    chipsInThisTurn += raiseAmount;
                    raiseAmount += dealer.currentCall - chipsInThisTurn;
                    
                    chips -= raiseAmount;
                    changePot = raiseAmount;
                    raise = true;
                    hasResponded = true;
                    allIn = true;
                    playerLastAction.GetComponent<lastAction>().setLastAction("Raised All In");  
                    }
                    else
                    {
                        Debug.Log("You do not have that many chips");
                    }
                }
                catch (Exception e)
                {
                    print(e);
                    return;
                }
                currentPot.ChangePot(changePot);
            }
        }
    }
    public void payBlind(int blind)
    {
        if (blind == 0)
        {
            chips -= 100;
            dealer.currentCall += 100;
            chipsInThisTurn += 100;
            currentPot.ChangePot(100);
            playerLastAction.GetComponent<lastAction>().setLastAction("Paid Big Blind");
        }
        else if (blind == 1)
        {
            chips -= 50;
            chipsInThisTurn += 50;
            currentPot.ChangePot(50);
            playerLastAction.GetComponent<lastAction>().setLastAction("Paid Small Blind");
        }
    }
    public void payCall(int callAmnt)
    { 
        //When a player calls subtract the ammount of chips they have already called this round before subtracting
        callAmnt -= chipsInThisTurn;
        if ((chips - callAmnt) <= 0)
        {
            chipsInThisTurn += chips;
            currentPot.ChangePot(chips);
            chips = 0;
            allIn = true;
            playerLastAction.GetComponent<lastAction>().setLastAction("Went All In");
        }
        else
        {
        chipsInThisTurn += callAmnt;
        chips -= callAmnt;
        currentPot.ChangePot(callAmnt);
        }
    }
    public void Reset()
    {
        check = false;
        raise = false;
        call = false;
        fold = false;
        hasResponded = false;
        myTurn = false;
        
    }
    public void resetHand()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        handScore = 0;
        UICard1Transform.SetActive(true);
        UICard2Transform.SetActive(true);
        playerLastAction.GetComponent<lastAction>().setLastAction("none");
        allIn = false;
        check = false;
        raise = false;
        call = false;
        fold = false;
        hasResponded = false;
        myTurn = false;
    }
}
