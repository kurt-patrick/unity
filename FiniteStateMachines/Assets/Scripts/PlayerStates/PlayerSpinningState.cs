using UnityEngine;

public class PlayerSpinningState : PlayerBaseState
{
    private float rotation = 0f;
    public override void EnterState(PlayerController_FSM player)
    {
    }

    public override void OnColissionEnter(PlayerController_FSM player)
    {
        player.transform.rotation = Quaternion.identity;
        player.TransitionToState(PlayerStates.IdleState);
    }

    public override void Update(PlayerController_FSM player)
    {
        float amountToRotate = 900f * Time.deltaTime;

        rotation += amountToRotate;

        if (rotation < 360)
        {
            player.transform.Rotate(Vector3.up, amountToRotate);
        }
        else
        {
            player.transform.rotation = Quaternion.identity;
            player.TransitionToState(PlayerStates.JumpingState);
        }
    }

}
