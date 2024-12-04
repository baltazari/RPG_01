using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
    }
    override public void Update()
    {
        base.Update();
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (player.IsWallDetected())
        {
            while (player.IsWallDetected())
            {

                // player.transform.Rotate(0f, 180f, 0f);
                stateMachine.ChangeState(player.wallSlide);
            }

        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }
    }
    override public void Exit()
    {
        base.Exit();
    }
}
