using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
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
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     stateMachine.ChangeState(player.idleState);
        // }
    }
    override public void Exit()
    {
        base.Exit();
    }

}
