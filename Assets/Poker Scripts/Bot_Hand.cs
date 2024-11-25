using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bot_Hand : MonoBehaviour
{
    public Vector3 card1Position;
    public Vector3 card1Rotation;
    public Vector3 card2Position;
    public Vector3 card2Rotation;
    Quaternion card1RotationEuler;
    Quaternion card2RotationEuler;
    private GameObject deck;
    private GameObject potAmmount;
    private GameObject raiseAmnt;
    deckActions deckActions;
    bool handFull = false;
    
    CurrentPot currentPot;
    public cardStructure[] botHand = new cardStructure[2];
    bool check = false;
    bool fold = false;
    bool call = false;
    int raise;
    public int chips = 1000;
    [HideInInspector] int turnPosition;


    void Awake()
    {
        card1RotationEuler = Quaternion.Euler(card1Rotation);
        card2RotationEuler = Quaternion.Euler(card2Rotation);

        deck = GameObject.Find("Red Deck Complete");
        potAmmount = GameObject.Find("Pot Ammount");
        deckActions = deck.GetComponent<deckActions>();
        currentPot = potAmmount.GetComponent<CurrentPot>();
    }

    public cardStructure[] BotHandCards(string name)
    {

        if (!handFull) 
        {
        botHand[0] = deckActions.Draw();
        botHand[1] = deckActions.Draw();
        GameObject sampleCard = Instantiate(botHand[0].card);
        sampleCard.transform.position = card1Position;
        sampleCard.transform.rotation = card1RotationEuler;

        sampleCard = Instantiate(botHand[1].card);
        sampleCard.transform.position = card2Position;
        sampleCard.transform.rotation = card2RotationEuler;
        handFull = true;
        }

        return botHand;
    }
    public void BotFold()
    {
        fold = true;
    }
    public void BotCheck()
    {
        check = true;
    }
    public void BotCall()
    {

    }
    public void BotRaise(TMP_InputField potAmmount)
    {
        int changePot = 0;
        try {
        changePot = int.Parse(potAmmount.text);
        }
        catch (Exception e)
        {
            print(e);
        }
        currentPot.ChangePot(changePot);
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
    public void setTurnPosition(int position)
    {
        turnPosition = position;
    }
}
