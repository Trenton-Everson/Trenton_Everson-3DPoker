using UnityEngine;

public class PokerRiverState : PokerBaseState
{
    public override void EnterState(PokerStateManager pokerRound)
    {

    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        if (true)
        {
            Debug.Log("River State");
            pokerRound.SwitchState(pokerRound.ShowdownState);
        }
    }
}
