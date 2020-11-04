using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player)
    {
    }

    public override void OnColissionEnter(PlayerController_FSM player)
    {
    }

    public override void Update(PlayerController_FSM player)
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.Rigidbody.AddForce(Vector3.up * player.jumpForce);
            player.TransitionToState(PlayerStates.JumpingState);
        }
    }
}
