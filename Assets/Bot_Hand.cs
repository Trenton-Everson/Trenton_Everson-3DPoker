using System.Collections;
using System.Collections.Generic;
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
    deckActions deckActions;
    bool handFull = false;
    public cardStructure[] botHand = new cardStructure[2];


    void Awake()
    {
        card1RotationEuler = Quaternion.Euler(card1Rotation);
        card2RotationEuler = Quaternion.Euler(card2Rotation);

        deck = GameObject.Find("Red Deck Complete");
        deckActions = deck.GetComponent<deckActions>();
    }

    public cardStructure[] BotHandCards()
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

}
