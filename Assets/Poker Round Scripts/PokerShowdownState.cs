using UnityEngine;

public class PokerShowdownState : PokerBaseState
{
    int temp = 0;
    public override void EnterState(PokerStateManager pokerRound)
    {

    }
    public override void UpdateState(PokerStateManager pokerRound)
    {
        /** if (temp == 0)
        {
            Debug.Log("Showdown State");
            temp++;
        } **/

        //pokerRound.SwitchState(pokerRound.BlindsState);
    }
}
