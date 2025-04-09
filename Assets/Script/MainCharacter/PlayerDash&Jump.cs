using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Dash 参数
    public float dashForce = 10f; // 冲刺的力度
    public float dashDuration = 0.2f; // 冲刺的持续时间
    public float dashCooldown = 1f; // 冲刺的冷却时间
    private bool isDashing = false;
    private bool isDashCooldown = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    // Jump 参数
    public float jumpForce = 5f; // 跳跃时施加的力的大小
    public float jumpDuration = 0.5f; // 施加力的持续时间
    public float jumpCooldown = 1f; // 跳跃冷却时间（秒）
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private float lastJumpTime = -1f; // 记录上一次跳跃的时间

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleDash();
        HandleJump();
    }

    void HandleDash()
    {
        // 冷却时间计时
        if (isDashCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                isDashCooldown = false;
                dashCooldownTimer = 0f;
            }
        }

        // 按下冲刺键且不在冲刺或冷却状态
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isDashCooldown)
        {
            StartDash();
        }

        // 冲刺计时
        if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                StopDash();
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = 0f;

        // 获取玩家输入方向
        float horizontal = Input.GetAxis("Horizontal"); // A/D 或左右方向键
        float vertical = Input.GetAxis("Vertical");     // W/S 或上下方向键

        // 计算冲刺方向
        Vector3 dashDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // 如果玩家没有输入方向，默认向前冲刺
        if (dashDirection.magnitude < 0.1f)
        {
            dashDirection = transform.forward;
        }

        // 施加冲刺力
        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
    }

    void StopDash()
    {
        isDashing = false;
        isDashCooldown = true; // 进入冷却状态
        rb.velocity = Vector3.zero; // 停止冲刺后的速度
    }

    void HandleJump()
    {
        // 检测玩家是否按下跳跃键（默认是空格键），并且不处于跳跃状态
        // 同时检查是否在冷却时间内
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && CanJump())
        {
            StartJump();
        }

        // 如果在跳跃状态，持续施加力
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;

            // 计算力的大小（随时间衰减）
            float forceMultiplier = 1f - (jumpTimer / jumpDuration);
            rb.AddForce(Vector3.up * jumpForce * forceMultiplier, ForceMode.Force);

            // 跳跃时间结束
            if (jumpTimer >= jumpDuration)
            {
                StopJump();
            }
        }
    }

    bool CanJump()
    {
        // 检查当前时间是否超过了上一次跳跃时间加上冷却时间
        return Time.time >= lastJumpTime + jumpCooldown;
    }

    void StartJump()
    {
        isJumping = true;
        jumpTimer = 0f;
        lastJumpTime = Time.time;
    }

    void StopJump()
    {
        isJumping = false;
    }
}