using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroudedState : PlayerState
{
    public PlayerGroudedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    override public void Enter()
    {
        base.Enter();
    }
    override public void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
    override public void Exit()
    {
        base.Exit();
    }
}
