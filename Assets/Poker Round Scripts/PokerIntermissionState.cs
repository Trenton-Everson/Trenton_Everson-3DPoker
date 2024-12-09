using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class PokerIntermissionState : PokerBaseState
{
    public bool _continue = false;
    public override void EnterState(PokerStateManager pokerRound)
    {
        pokerRound.deck.resetDeck();

        for (int i = 0; i < pokerRound.allPlayersCopyTwo.Length; i++)
        {
            pokerRound.allPlayers[i].resetHand();
            pokerRound.allPlayers[i].Reset();
        }
        pokerRound.currPot.pot=0;
        
        pokerRound.doContinue = false;
        pokerRound.SwitchState(pokerRound.BlindsState);
        
    }

    public override void UpdateState(PokerStateManager pokerRound)
    {
        
    }
}