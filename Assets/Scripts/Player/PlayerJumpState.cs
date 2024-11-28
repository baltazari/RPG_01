using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }
    override public void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
    }
    override public void Exit()
    {
        base.Exit();
    }
}
