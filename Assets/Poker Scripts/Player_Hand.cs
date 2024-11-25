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
    [HideInInspector] public string objectName;
    CurrentPot currentPot;
    public Vector3 firstCardInHandPosition;
    public Vector3 secondCardInHandPosition;
    public Vector3 firstCardInHandRotation;
    public Vector3 secondCardInHandRotation;
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
    
    private void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
        potAmmount = GameObject.Find("Pot Ammount");
        objectName = gameObject.name;
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
        GameObject sampleCard = Instantiate(playerHand[0].card, this.transform);
        sampleCard.transform.position = firstCardInHandPosition;
        sampleCard.transform.rotation = Quaternion.Euler(firstCardInHandRotation);

        sampleCard = Instantiate(playerHand[1].card, this.transform);
        sampleCard.transform.position = secondCardInHandPosition;
        sampleCard.transform.rotation = Quaternion.Euler(secondCardInHandRotation);
        handFull = true;
        }
    }
    public void PlayerFold()
    {
        if (myTurn == true)
        {
        fold = true;
        }
    }
    public void PlayerCheck()
    {
        if (myTurn == true)
        {
        check = true;
        }
    }
    public void PlayerCall()
    {
        if (myTurn == true)
        {
        call = true;
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
                    changePot = int.Parse(potAmmount.text);
                    raise = true;
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
        }
        else if (blind == 1)
        {
            chips -= 50;
        }
    }
    public void Reset()
    {
        check = false;
        raise = false;
        call = false;
        hasResponded = false;
        
    }
}
