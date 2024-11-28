using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroudedState
{
    // Start is called before the first frame update
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
    }
    override public void Update()
    {
        base.Update();
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
    override public void Exit()
    {
        base.Exit();
    }
}
