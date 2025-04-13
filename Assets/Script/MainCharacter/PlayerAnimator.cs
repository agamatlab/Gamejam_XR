using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    float direction = 0;

    [SerializeField]
    private float animationSpeed = 50;

    float animationConstant;
    public bool isSwinging;

    Animator playerAnimator;
    PlayerMovement playerMovementScript;

    public GameObject StrongAttackMiniGame;

    // Used for attacks and combos


    public float comboTimerLimit = 2f;
    private float comboTimer;
    public float attackCooldownLimit;
    private float attackCooldown = 0; 
    private int currentCombo = 0;

    // Start is called before the first frame update
    void Start()
    {
        animationConstant = 1 / animationSpeed;
        playerAnimator = GetComponent<Animator>();
        playerMovementScript = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = 0;
        transform.position = transformPosition;

        // Canceling all previously  set animations
        playerAnimator.SetBool("swing1", false);
        playerAnimator.SetBool("swing2", false);
        playerAnimator.SetBool("swing3", false);


        if(attackCooldown > 0)attackCooldown -= Time.deltaTime;
        if(comboTimer > 0)comboTimer -= Time.deltaTime;

        if (comboTimer <= 0)
        {
            currentCombo = 0;
        }
        //Debug.Log(comboTimer + " " + currentCombo);


        isSwinging = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("swing normal");

        if (Input.GetMouseButtonDown(0) && !isSwinging && (playerMovementScript.balancePoint == 6 || playerMovementScript.isHoldingRight))
        {
            if(currentCombo == 0 && attackCooldown <= 0)
            {
                playerAnimator.SetBool("swing1", true);
                attackCooldown = attackCooldownLimit;
                comboTimer = comboTimerLimit;
                currentCombo++;
                Debug.Log("first attack");
            }
            else if(currentCombo == 1 && attackCooldown <= 0)
            {
                playerAnimator.SetBool("swing2", true);
                attackCooldown = attackCooldownLimit;
                comboTimer = comboTimerLimit;
                currentCombo++;

                Debug.Log("second attack");
                StrongAttackMiniGame.SetActive(true);
                // Here I will spawn a slider and if the player presses the button again in a certain time frame, it will trigger the third attack

            }
            else if (currentCombo == 2 && attackCooldown <= 0)
            {
                bool onTime = StrongAttackMiniGame.GetComponent<ClockSwingCursor>().IsCursorInTargetArea();

                if (onTime)
                {

                    playerAnimator.SetBool("swing3", true);
                    attackCooldown = attackCooldownLimit;
                    currentCombo = 0;

                    Debug.Log("third attack");
                }
                else
                {
                    Debug.Log("You are such a failure");
                }
                StrongAttackMiniGame.SetActive(false);

            }
            else
            {
                return;
            }

            Vector2 mousePosition = Input.mousePosition;

            float screenWidth = Screen.width;
            playerAnimator.SetFloat("directionBinary", 1);
            playerMovementScript.balancePoint +=1;
            if (mousePosition.x < screenWidth / 2)
            {
                playerAnimator.SetFloat("directionBinary", 0);
                playerMovementScript.facingLeft = true;
            }
            else
            {
                playerAnimator.SetFloat("directionBinary", 1);
                playerMovementScript.facingLeft = false;

            }

        }


        playerAnimator.SetFloat("directionRange", direction);

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            direction += animationConstant;
        }
        else if (horizontal < 0)
        {

            direction -= animationConstant;
        }
        else
        {
            if (direction < -0.1 || direction > 0.1)
            {
                direction -= (direction / direction) * animationConstant;
            }
        }

        direction = Mathf.Clamp(direction, -1, 1);
        playerAnimator.SetBool("facingLeft", playerMovementScript.facingLeft);
        if (horizontal != 0)
        {

            playerAnimator.SetBool("running", true);
        }
        else
        {
            playerAnimator.SetBool("running", false);

        }





    }
}
