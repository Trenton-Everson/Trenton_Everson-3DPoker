using UnityEngine;

public class PokerFlopState : PokerBaseState
{
    public override void EnterState(PokerStateManager pokerRound)
    {

    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        if (true)
        {
            Debug.Log("Flop State");
            pokerRound.SwitchState(pokerRound.TurnState);
        }
    }
}
