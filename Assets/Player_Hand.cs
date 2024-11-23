using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Player_Hand : MonoBehaviour
{
    public cardStructure[] playerHand = new cardStructure[2];
    private GameObject deck;
    deckActions deckActions;
    public Vector3 firstCardInHandPosition;
    public Vector3 secondCardInHandPosition;
    bool handFull = false;

    private void Awake()
    {
        deck = GameObject.Find("Red Deck Complete");
    }
    private void Start()
    {

        deckActions = deck.GetComponent<deckActions>();
    }

    public void AddCardsToHand()
    {
        if (!handFull) 
        {
        playerHand[0] = deckActions.Draw();
        playerHand[1] = deckActions.Draw();
        GameObject sampleCard = Instantiate(playerHand[0].card);
        sampleCard.transform.position = firstCardInHandPosition;
        sampleCard.transform.rotation = Quaternion.Euler(-39.985f, 0, 0);

        sampleCard = Instantiate(playerHand[1].card);
        sampleCard.transform.position = secondCardInHandPosition;
        sampleCard.transform.rotation = Quaternion.Euler(-39.985f, 0, 0);
        handFull = true;
        }
    }
    public void testingMethod()
    {
        
    }
}
