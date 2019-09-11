using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour {

    [Header("Runtime")]
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private string jumpInputName;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float moveSmoothing;
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float groundedBuffer;
    public bool canQueueJump;

    [SerializeField] private float checkPointSetRate = 30f;
    private Vector3 respawnPos;
    private bool canSetCheckpoint;

    private Vector3 moveVector;
    private Vector3 move;
    private float moveSpeed;
    private float moveSpeedLerp;
    private bool isGrounded = false;
    private bool isSprinting = false;
    private bool canJump = false;
    private bool queueJump = false;
    private bool lastFrameJump = false;
    private float coyoteTimeCur;
    private Rigidbody rb;

    public RigidbodyInterpolation interpolation;
    public GameObject playerCamera;
    public Transform groundCheck;
    public LayerMask ground;

    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        coyoteTimeCur = coyoteTime;
        respawnPos = transform.localPosition;
    }
	
	void Update ()
    {
        PlayerMovement();
        rb.MovePosition(rb.position + move);
    }

    private void FixedUpdate()
    {

    }

    private void PlayerMovement()
    {
        float hInput = Input.GetAxis(horizontalInputName);
        float vInput = Input.GetAxis(verticalInputName);
        bool jInput = Input.GetButtonDown(jumpInputName);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundedBuffer, ground, QueryTriggerInteraction.Ignore);

        if (isGrounded)
        {
            if (!lastFrameJump)
            {
                canJump = true;
            }
            coyoteTimeCur = coyoteTime;
            if (rb.velocity.y <= 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }   
        }
        else if (coyoteTimeCur > 0)
        {
            coyoteTimeCur -= Time.deltaTime * 1000;
        }

        if (coyoteTimeCur <= 0 && coyoteTimeCur != coyoteTime)
        {
            canJump = false;
        }

        if (rb.velocity.y <= 0 && jInput && canQueueJump)
        {
            queueJump = true;   
        }

        lastFrameJump = false;
        if ((jInput || queueJump) && canJump)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            canJump = false;
            lastFrameJump = true;
            queueJump = false;
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        moveSpeed = isSprinting ? sprintSpeed : walkSpeed;
        moveSpeedLerp = Mathf.Lerp(moveSpeedLerp, moveSpeed, moveSmoothing * Time.deltaTime);

        Vector3 moveX = hInput * transform.right;
        Vector3 moveZ = vInput * transform.forward;

        moveVector = Vector3.ClampMagnitude(moveX + moveZ, 1);
        move = moveVector * moveSpeed * Time.deltaTime;
    }

    public void Respawn()
    {
        transform.localPosition = respawnPos;
        var stats = FindObjectOfType<PlayerStats>();
        stats.hpCurrent = stats.hpMax;
        stats.isDead = false;
    }
}