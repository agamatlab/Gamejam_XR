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

        initBalanceIndicator();

    }
    void initBalanceIndicator()
    {
        RectTransform rectTransform1 = balanceIndicatorLeft.GetComponent<RectTransform>();
        rectTransform1.anchoredPosition = new Vector2(0, -500);
        RectTransform rectTransform2 = balanceIndicatorLeftMid.GetComponent<RectTransform>();
        rectTransform2.anchoredPosition = new Vector2(150, -500);
        RectTransform rectTransform3 = balanceIndicatorMid.GetComponent<RectTransform>();
        rectTransform3.anchoredPosition = new Vector2(300, -500);
        RectTransform rectTransform4 = balanceIndicatorRightMid.GetComponent<RectTransform>();
        rectTransform4.anchoredPosition = new Vector2(450, -500);
        RectTransform rectTransform5 = balanceIndicatorRight.GetComponent<RectTransform>();
        rectTransform5.anchoredPosition = new Vector2(600, -500);
        balanceIndicatorMid.color = Color.red;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");


        if (playerAniamationScript.isSwinging)
        {
            rb.velocity = Vector3.zero;
        }
        else if (horizontal != 0f)
        {

            rb.velocity = new Vector3(horizontal, 0, 0) * movementSpeed;
        }
        else
        {

            rb.velocity = Vector3.zero;
        }
    }
}