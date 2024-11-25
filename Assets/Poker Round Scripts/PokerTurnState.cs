using UnityEngine;

public class PokerTurnState : PokerBaseState
{
    public override void EnterState(PokerStateManager pokerRound)
    {

    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        if (true)
        {
            Debug.Log("Turn State");
            pokerRound.SwitchState(pokerRound.RiverState);
        }
    }
}
