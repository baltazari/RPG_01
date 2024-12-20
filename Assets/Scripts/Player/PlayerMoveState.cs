using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroudedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    override public void Enter()
    {
        base.Enter();
    }
    override public void Update()
    {
        base.Update();
        

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    override public void Exit()
    {
        base.Exit();
    }

}
