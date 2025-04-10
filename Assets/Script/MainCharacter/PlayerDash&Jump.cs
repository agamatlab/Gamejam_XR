using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Dash ����
    public float dashForce = 10f; // ��̵�����
    public float dashDuration = 0.2f; // ��̵ĳ���ʱ��
    public float dashCooldown = 1f; // ��̵���ȴʱ��
    private bool isDashing = false;
    private bool isDashCooldown = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    // Jump ����
    public float jumpForce = 5f; // ��Ծʱʩ�ӵ����Ĵ�С
    public float jumpDuration = 0.5f; // ʩ�����ĳ���ʱ��
    public float jumpCooldown = 1f; // ��Ծ��ȴʱ�䣨�룩
    private bool isJumping = false;
    private float jumpTimer = 0f;
    private float lastJumpTime = -1f; // ��¼��һ����Ծ��ʱ��

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
        // ��ȴʱ���ʱ
        if (isDashCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                isDashCooldown = false;
                dashCooldownTimer = 0f;
            }
        }

        // ���³�̼��Ҳ��ڳ�̻���ȴ״̬
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isDashCooldown)
        {
            StartDash();
        }

        // ��̼�ʱ
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

        // ��ȡ������뷽��
        float horizontal = Input.GetAxis("Horizontal"); // A/D �����ҷ����
        float vertical = Input.GetAxis("Vertical");     // W/S �����·����

        // �����̷���
        Vector3 dashDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // ������û�����뷽��Ĭ����ǰ���
        if (dashDirection.magnitude < 0.1f)
        {
            dashDirection = transform.forward;
        }

        // ʩ�ӳ����
        rb.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
    }

    void StopDash()
    {
        isDashing = false;
        isDashCooldown = true; // ������ȴ״̬
        rb.velocity = Vector3.zero; // ֹͣ��̺���ٶ�
    }

    void HandleJump()
    {
        // �������Ƿ�����Ծ����Ĭ���ǿո���������Ҳ�������Ծ״̬
        // ͬʱ����Ƿ�����ȴʱ����
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && CanJump())
        {
            StartJump();
        }

        // �������Ծ״̬������ʩ����
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;

            // �������Ĵ�С����ʱ��˥����
            float forceMultiplier = 1f - (jumpTimer / jumpDuration);
            rb.AddForce(Vector3.up * jumpForce * forceMultiplier, ForceMode.Force);

            // ��Ծʱ�����
            if (jumpTimer >= jumpDuration)
            {
                StopJump();
            }
        }
    }

    bool CanJump()
    {
        // ��鵱ǰʱ���Ƿ񳬹�����һ����Ծʱ�������ȴʱ��
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