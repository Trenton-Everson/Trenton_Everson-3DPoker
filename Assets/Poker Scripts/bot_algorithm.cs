using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bot_algorithm : MonoBehaviour
{
    Player_Hand bothand;
    int handScore = 0;
    int random = 0;

    void Start()
    {
        bothand = gameObject.GetComponent<Player_Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bothand.myTurn == true && bothand.hasResponded == false)
        {
            random =  UnityEngine.Random.Range(1, 101);
            string[] temp = bothand.handChecker.testingMethod(bothand.playerHand).Split("|||");
            handScore = int.Parse(temp[1]);
            
            if (random >= 10)
            {
                bothand.PlayerCall();
            }
            else if (random < 10)
            {
                bothand.PlayerFold();
            }
            //Debug.Log(bothand.name + ": has a score of: " + handScore);
        }
    }
}
