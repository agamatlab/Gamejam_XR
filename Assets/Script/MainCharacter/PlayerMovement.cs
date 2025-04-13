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
        
        balancePoint = 4;
        isHoldingRight = true;
        facingLeft = false;
        initBalanceIndicator();

    }
    void initBalanceIndicator()
    {
        RectTransform rectTransform1 = balanceIndicatorLeft.GetComponent<RectTransform>();
        rectTransform1.anchoredPosition = new Vector2(0.05f*Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform2 = balanceIndicatorLeftMid.GetComponent<RectTransform>();
        rectTransform2.anchoredPosition = new Vector2(0.1f*Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform3 = balanceIndicatorMid.GetComponent<RectTransform>();
        rectTransform3.anchoredPosition = new Vector2(0.15f*Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform4 = balanceIndicatorRightMid.GetComponent<RectTransform>();
        rectTransform4.anchoredPosition = new Vector2(0.2f*Screen.width, -0.2f * Screen.height);


        RectTransform rectTransform5 = balanceIndicatorRight.GetComponent<RectTransform>();
        rectTransform5.anchoredPosition = new Vector2(0.25f*Screen.width, -0.2f * Screen.height);

        rectTransform1.sizeDelta =rectTransform2.sizeDelta =rectTransform3.sizeDelta =rectTransform4.sizeDelta =rectTransform5.sizeDelta = new Vector2(0.04f*Screen.width,0.04f*Screen.width);

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
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        
        if (Input.GetMouseButtonDown(0) && !playerAniamationScript.isSwinging &&  isHoldingRight)
        {
            rb.velocity = Vector3.zero;
            
        }
        else if(playerAniamationScript.isSwinging){
            rb.velocity = Vector3.zero;
        }
        else if (horizontal != 0f)
        {

            rb.velocity = new Vector3(horizontal, 0, 0) * movementSpeed;
            if(horizontal > 0){
                facingLeft = false;
            }else{
                facingLeft = true;
            }
        }
        else
        {

            rb.velocity = Vector3.zero;
        }
        if(balancePoint >= 7){
            //isHoldingRight = false;
            balancePoint = 4;
        }
        rb.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
        updateBalancePointUI();

    }
}