using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player)
    {
    }

    public override void OnColissionEnter(PlayerController_FSM player)
    {
        player.TransitionToState(PlayerStates.IdleState);
    }

    public override void Update(PlayerController_FSM player)
    {
        if (Input.GetButtonDown("Jump"))
        {
            //player.Rigidbody.AddForce(Vector3.up * player.jumpForce);
            player.TransitionToState(new PlayerSpinningState());
        }
    }
}
