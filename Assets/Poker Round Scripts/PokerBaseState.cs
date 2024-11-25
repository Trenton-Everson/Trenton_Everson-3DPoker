using UnityEngine;

public abstract class PokerBaseState
{
    public abstract void EnterState(PokerStateManager pokerRound);
    public abstract void UpdateState(PokerStateManager pokerRound);
    
}
