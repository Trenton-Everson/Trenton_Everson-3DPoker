using System.Runtime.InteropServices;
using UnityEngine;

public class PokerBlindsState : PokerBaseState
{
    public override void EnterState(PokerStateManager pokerRound)
    {
        pokerRound.bot1Hand.payBlind(1);
        pokerRound.bot2Hand.payBlind(0);
        Debug.Log("Blind State Ended");
    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        Debug.Log("Bot 1 has paid the small Blind and Bot 2 has Paid the big blind \n Moving to PreFlop");
        pokerRound.SwitchState(pokerRound.PreflopState);
        
    }
}
