using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Player_Hand : MonoBehaviour
{
    public cardStructure[] playerHand = new cardStructure[2];
    private GameObject deck;
    deckActions deckActions;
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

    bool handFull = false;
    [HideInInspector] public bool check = false;
    [HideInInspector] public bool fold = false;
    [HideInInspector] public bool call = false;
    [HideInInspector] public bool raise = false;
    [HideInInspector] public int chips = 1000;
    [HideInInspector] public bool inGame = true;
    [HideInInspector] public bool hasResponded = false;
    [HideInInspector] public int turnPosition;
    [HideInInspector] public bool myTurn = false;
    [HideInInspector] public int raiseAmount = 0;
    [HideInInspector] public int chipsInThisTurn = 0;
    GameObject dealerObj;
    Dealer dealer;
    [HideInInspector] public HandChecker handChecker;
    [HideInInspector] public int handScore = 0;
    GameObject sampleCard1 = null;
    GameObject sampleCard2 = null;

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
    }

    public void AddCardsToHand(string name)
    {

        if (!handFull) 
        {
        playerHand[0] = deckActions.Draw();
        playerHand[1] = deckActions.Draw();
        sampleCard1 = Instantiate(playerHand[0].card, this.transform);
        sampleCard1.transform.position = firstCardInHandPosition;
        sampleCard1.transform.rotation = Quaternion.Euler(firstCardInHandRotation);

        sampleCard2 = Instantiate(playerHand[1].card, this.transform);
        sampleCard2.transform.position = secondCardInHandPosition;
        sampleCard2.transform.rotation = Quaternion.Euler(secondCardInHandRotation);
        handFull = true;
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
    }
    public void PlayerCheck()
    {
        if (myTurn == true)
        {
        check = true;
        hasResponded = true;
        }
    }
    public void PlayerCall()
    {
        if (myTurn == true)
        {
        call = true;
        hasResponded = true;
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
                    dealer.currentCall += raiseAmount;
                    chipsInThisTurn += raiseAmount;
                    raiseAmount += dealer.currentCall - chipsInThisTurn;
                    
                    chips -= raiseAmount;
                    changePot = raiseAmount;
                    raise = true;
                    hasResponded = true;
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
            hasResponded = true;
        }
        else if (blind == 1)
        {
            chips -= 50;
            chipsInThisTurn += 50;
            currentPot.ChangePot(50);
        }
    }
    public void payCall(int callAmnt)
    { 
        //When a player calls subtract the ammount of chips they have already called this round before subtracting
        callAmnt -= chipsInThisTurn;
        chipsInThisTurn += callAmnt; 
        chips -= callAmnt;
        currentPot.ChangePot(callAmnt);
    }
    public void Reset()
    {
        check = false;
        raise = false;
        call = false;
        hasResponded = false;
        
    }
}
