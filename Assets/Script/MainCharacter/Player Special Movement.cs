using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Dash parameters
    public float dashForce = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool isDashCooldown = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    // Jump parameters
    public float jumpForce = 30f;
    public float jumpDuration = 0.5f;
    public float jumpCooldown = 1f;
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private float lastJumpTime = -1f;

    // Ground detection parameters
    public float groundCheckDistance = 0.5f;
    public float slopeLimit = 45f;
    public float stepOffset = 0.3f;
    private bool isGrounded;
    private Vector3 groundNormal;

    // Movement parameters
    public float moveSpeed = 5f;
    public float airControl = 0.5f;
    private float verticalVelocity;

    private Rigidbody rb;
    private CapsuleCollider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        HandleGroundDetection();
     
     
        HandleMovement();
    }

    void HandleGroundDetection()
    {
        RaycastHit hit;
        Vector3 capsuleBottom = transform.position + col.center - Vector3.up * (col.height / 2 - col.radius);

        if (Physics.SphereCast(capsuleBottom, col.radius * 0.9f, Vector3.down, out hit, groundCheckDistance))
        {
            isGrounded = true;
            groundNormal = hit.normal;

            // Snap to ground surface
            if (!isJumping)
            {
                rb.position = Vector3.Lerp(rb.position, hit.point + Vector3.up * col.radius, 10f * Time.deltaTime);
            }
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Project movement direction onto ground plane
            Vector3 slopeDirection = Vector3.ProjectOnPlane(moveDirection, groundNormal).normalized;

            // Calculate slope angle
            float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);

            if (slopeAngle <= slopeLimit)
            {
                // Apply movement force
                float currentSpeed = isGrounded ? moveSpeed : moveSpeed * airControl;
                Vector3 targetVelocity = slopeDirection * currentSpeed;

                if (isGrounded)
                {
                    rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, 10f * Time.deltaTime);
                }
                else
                {
                    rb.AddForce(targetVelocity * 5f * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
        }

        // Step climbing
        if (isGrounded && moveDirection.magnitude > 0.1f)
        {
            Vector3 stepCheckOrigin = transform.position + Vector3.up * stepOffset;
            if (!Physics.Raycast(stepCheckOrigin, moveDirection, 0.5f))
            {
                RaycastHit stepHit;
                if (Physics.Raycast(stepCheckOrigin + moveDirection * 0.5f, Vector3.down, out stepHit, stepOffset * 2f))
                {
                    rb.position += Vector3.up * stepOffset;
                }
            }
        }
    }

    // 保持原有Dash和Jump代码不变，以下省略...
    // (Original dash and jump code remains unchanged here)
}