using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bot_algorithm : MonoBehaviour
{
    Player_Hand bothand;
    bool once = false;
    void Start()
    {
        bothand = gameObject.GetComponent<Player_Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bothand.myTurn == true && bothand.hasResponded == false)
        {
            string[] temp = bothand.handChecker.testingMethod(bothand.playerHand).Split("|||");

            int[,] botsCurrentCards = bothand.handChecker.handCheckForBot(bothand.playerHand);

            List<int> potentialScores = new List<int>();
            int test = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (botsCurrentCards[j, i] == 1)
                    {
                        test++;
                    }
                }
            }

            int currentScore = int.Parse(temp[1]);
            //Debug.Log("");

        if (test > 2 && test < 7) {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    if (botsCurrentCards[ j - 1, i ] == 0)
                    {
                        potentialScores.Add(bothand.handChecker.botHandScoreChecker(botsCurrentCards, j, i, bothand.playerHand));
                    }
                }
            }
            potentialScores.Sort();
            potentialScores.Reverse();

            int checkScore = potentialScores[potentialScores.Count / 2];
            if (currentScore > 12000)
            {
                bothand.PlayerCall();
            }
            else if (checkScore >= currentScore)
            {
                if (checkScore > currentScore  && once == false)
                {
                    bothand.PlayerRaise(50);
                    once = true;
                }
                else if (checkScore == currentScore)
                {
                    checkScore = potentialScores[potentialScores.Count / 4];
                    if (checkScore > currentScore)
                    {
                        bothand.PlayerCall();
                        once = false;
                    }
                    else if (checkScore == currentScore)
                    {
                        bothand.PlayerFold();
                    }
                }
                else{
                    bothand.PlayerCall();
                    once = false;
                }
            }
            


            /**
            int avgScore = 0;
            for (int i = 0; i < potentialScores.Count; i++)
            {
                avgScore += potentialScores[i];
            }
            avgScore /= potentialScores.Count;
            
            if (avgScore > checkScore)
            {
                bothand.PlayerCall();
            }
            else
            {
                bothand.PlayerFold();
            }
            **/
        }
        else
        {
            bothand.PlayerCall();
        }
        }
    }
}
