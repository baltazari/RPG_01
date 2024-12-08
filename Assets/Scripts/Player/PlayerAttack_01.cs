using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_01 : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 0.5f;
    public PlayerAttack_01(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);

        //Choose attack direction
        float attackDir = player.faceDirection;
        if (xInput != 0)
        {
            attackDir = xInput;
        }

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        stateTimer = 0.1f;


    }
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetVelocityZero();
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .13f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

}
