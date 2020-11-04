using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerController_FSM player);
    public abstract void Update(PlayerController_FSM player);
    public abstract void OnColissionEnter(PlayerController_FSM player);
}

public static class PlayerStates
{
    public static PlayerJumpingState JumpingState => new PlayerJumpingState();
    public static PlayerIdleState IdleState => new PlayerIdleState();
    public static PlayerSpinningState SpinningState => new PlayerSpinningState();
}
