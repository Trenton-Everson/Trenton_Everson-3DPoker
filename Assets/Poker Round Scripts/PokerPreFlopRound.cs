using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerPreFlopRound : MonoBehaviour
{
    private GameObject player;
    private GameObject bot1;
    private GameObject bot2;
    private GameObject bot3;
    private GameObject bot4;
    public Player_Hand playerHand;
    public Player_Hand bot1Hand;
    public Player_Hand bot2Hand;
    public Player_Hand bot3Hand;
    public Player_Hand bot4Hand;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bot1 = GameObject.Find("Bot 1");
        bot2 = GameObject.Find("Bot 2");
        bot3 = GameObject.Find("Bot 3");
        bot4 = GameObject.Find("Bot 4");

        playerHand = player.GetComponent<Player_Hand>();
        bot1Hand = bot1.GetComponent<Player_Hand>();
        bot2Hand = bot2.GetComponent<Player_Hand>();
        bot3Hand = bot3.GetComponent<Player_Hand>();
        bot4Hand = bot4.GetComponent<Player_Hand>();
    }
    public void waitForPlayerInput(Player_Hand givenHand)
    {
        StartCoroutine(WaitForInput(givenHand));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator WaitForInput(Player_Hand givenHand)
    {
        Debug.Log("Starting Coroutine");
        yield return new WaitUntil(() => givenHand.fold == true);
        Debug.Log("Ending Coroutine");
    }
}
