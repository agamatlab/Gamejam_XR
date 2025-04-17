using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private float movementSpeed = 10;
    Animator playerAnimator;
    PlayerAnimator playerAniamationScript;
    private Canvas balanceIndicatorCanvas;
    private Image balanceIndicatorLeft;
    private Image balanceIndicatorLeftMid;
    private Image balanceIndicatorMid;
    private Image balanceIndicatorRightMid;

    private Image balanceIndicatorRight;
    public int balancePoint;

    public bool isHoldingRight;

    public bool facingLeft;
    public Slider healthbar;
    private Camera mainCamera;
    public Vector3 offset = new Vector3(0, 1, 0);
    public float health = 100;
    public bool hasCollided;
    public enemyAnimator enemyAnimatorScript;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerAniamationScript = GetComponentInChildren<PlayerAnimator>();
        balanceIndicatorCanvas = GetComponentInChildren<Canvas>();
        balanceIndicatorLeft = balanceIndicatorCanvas.transform.Find("left").GetComponent<Image>();
        balanceIndicatorLeftMid = balanceIndicatorCanvas.transform.Find("leftmid").GetComponent<Image>();

        balanceIndicatorMid = balanceIndicatorCanvas.transform.Find("mid").GetComponent<Image>();

        balanceIndicatorRightMid = balanceIndicatorCanvas.transform.Find("rightmid").GetComponent<Image>();

        balanceIndicatorRight = balanceIndicatorCanvas.transform.Find("right").GetComponent<Image>();
        healthbar = balanceIndicatorCanvas.transform.Find("healthbar").GetComponent<Slider>();
        balancePoint = 4;
        isHoldingRight = true;
        facingLeft = false;
        initBalanceIndicator();
        offset = new Vector3(0, 1, 0);
        mainCamera = Camera.main;
        health = 100;
    }
    void initBalanceIndicator()
    {
        RectTransform rectTransform1 = balanceIndicatorLeft.GetComponent<RectTransform>();
        rectTransform1.anchoredPosition = new Vector2(0.05f * Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform2 = balanceIndicatorLeftMid.GetComponent<RectTransform>();
        rectTransform2.anchoredPosition = new Vector2(0.1f * Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform3 = balanceIndicatorMid.GetComponent<RectTransform>();
        rectTransform3.anchoredPosition = new Vector2(0.15f * Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform4 = balanceIndicatorRightMid.GetComponent<RectTransform>();
        rectTransform4.anchoredPosition = new Vector2(0.2f * Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform5 = balanceIndicatorRight.GetComponent<RectTransform>();
        rectTransform5.anchoredPosition = new Vector2(0.25f * Screen.width, -0.2f * Screen.height);

        rectTransform1.sizeDelta = rectTransform2.sizeDelta = rectTransform3.sizeDelta = rectTransform4.sizeDelta = rectTransform5.sizeDelta = new Vector2(0.04f * Screen.width, 0.04f * Screen.width);

        balanceIndicatorMid.color = Color.red;
    }

    void updateBalancePointUI()
    {
        balanceIndicatorLeft.color = (balancePoint == 2) ? Color.red : Color.white;
        balanceIndicatorLeftMid.color = (balancePoint == 3) ? Color.red : Color.white;
        balanceIndicatorMid.color = (balancePoint == 4) ? Color.red : Color.white;
        balanceIndicatorRightMid.color = (balancePoint == 5) ? Color.red : Color.white;
        balanceIndicatorRight.color = (balancePoint == 6) ? Color.red : Color.white;
    }

    Transform FindParentByName(Transform child, string name)
    {
        if (child == null)
            return null;


        if (child.name == name)
            return child;


        return FindParentByName(child.parent, name);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasCollided)
        {
            return;
        }

        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            Transform swordEnemy = FindParentByName(other.gameObject.transform, "swordenemy");
            enemyAnimatorScript = swordEnemy.GetComponent<enemyAnimator>();
            if (enemyAnimatorScript.attack)
            {
                health -= 40;
                hasCollided = true;
            }
        }

    }
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            hasCollided = false;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + offset);
        healthbar.transform.position = screenPosition;
        healthbar.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        healthbar.value = health;

        if (Input.GetMouseButtonDown(0) && !playerAniamationScript.isSwinging && isHoldingRight)
        {
            rb.velocity = Vector3.zero;

        }
        else if (playerAniamationScript.isSwinging)
        {
            rb.velocity = Vector3.zero;
        }
        else if (horizontal != 0f)
        {

            rb.velocity = new Vector3(horizontal, 0, 0) * movementSpeed;
            if (horizontal > 0)
            {
                facingLeft = false;
            }
            else
            {
                facingLeft = true;
            }
        }
        else
        {

            rb.velocity = Vector3.zero;
        }
        if (balancePoint >= 7)
        {
            //isHoldingRight = false;
            balancePoint = 4;
        }
        rb.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
        updateBalancePointUI();

    }
}