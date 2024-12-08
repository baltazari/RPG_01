using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attack Movement")]
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float jumpForce = 15f;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashDir { get; private set; }
    public int dashCount = 2;
    public float dashCooldown = 5.0f;
    public float dashCooldownTimer;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    #region Component
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    private bool faceRight = true;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerAirState airState { get; private set; }
    public int faceDirection { get; private set; } = 1;
    public PlayerDashState dashState { get; private set; }
    public PlayerAttack_01 playerAttack_01 { get; private set; }
    #endregion
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        playerAttack_01 = new PlayerAttack_01(this, stateMachine, "Attack");
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);


    }
    private void Update()
    {
        stateMachine.currentState.Update();
        DashInput();
        Debug.Log(IsWallDetected());


    }

    //End  Animation
    public void EndAnimation() => stateMachine.currentState.AnimationFinishTrigger();
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(rb.velocity.x);

    }

    //Set velocity to zero
    public void SetVelocityZero() => rb.velocity = new Vector2(0, 0);

    #region Collision
    //Recognize wall and ground
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * faceDirection, wallCheckDistance, groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    //end recognize wall and ground
    #endregion
    private void Flip()
    {

        faceDirection *= -1;
        faceRight = !faceRight;
        transform.Rotate(0f, 180f, 0f);

    }

    //control character direction animation
    private void FlipController(float _xInput)
    {
        if (_xInput > 0f && !faceRight)
        {
            Flip();
        }
        else if (_xInput < 0f && faceRight)
        {
            Flip();
        }
    }

    // Dash Input control
    public void DashInput()
    {
        dashCooldownTimer -= Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button15)) && dashCount > 0)
        {
            --dashCount;
            dashCooldownTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
            {
                dashDir = faceDirection;
            }


            stateMachine.ChangeState(dashState);
        }
        if (dashCooldownTimer < 0 && dashCount < 2)
        {
            dashCount++;
            dashCooldownTimer = dashCooldown;
        }
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }


}
