using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float Timer = 0f;
    public bool alert = false;
    private Canvas enemyCanvas;
    private Image alertIndicator;

    public Vector3 UIoffset = new Vector3(0, 1, 0);
    private Camera mainCamera;

    public float patrolSpeed;
    public float alertSpeed;
    public float alertRange;
    private Transform playerBody;
    public bool runningLeft;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("runningLeft", true);
        Timer = -1f;
        alert = false;
        enemyCanvas = GetComponentInChildren<Canvas>();
        alertIndicator = enemyCanvas.transform.Find("patrol").GetComponent<Image>();
        UIoffset = new Vector3(0, 1.5f, 0);
        mainCamera = Camera.main;
        patrolSpeed = 0.002f;
        alertSpeed = 0.004f;
        runningLeft = false;
        alertRange = 4f;
        playerBody = GameObject.Find("Player").transform;
        //initialize UI for alert
        RectTransform rectTransform = alertIndicator.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0.05f * Screen.width, -0.2f * Screen.height);
        rectTransform.sizeDelta = new Vector2(0.02f * Screen.width, 0.02f * Screen.width);
        alertIndicator.color = Color.green;
    }


    bool checkPlayerInRange()
    {
        
        float x1 = transform.position.x;
        float x2 = playerBody.position.x;
        float diff = x1 - x2;
        if(diff > 0 && diff < alertRange && runningLeft ){
            return true;
        }
        if(diff < 0 && diff > -alertRange && !runningLeft){
            return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + UIoffset);
        alertIndicator.transform.position = screenPosition;
        alertIndicator.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);


        //check for player



        if (!alert)
        {
            Timer += Time.deltaTime;
            alert = checkPlayerInRange();
            if (Timer < 3f && Timer >= 0f)
            {

                transform.position += new Vector3(patrolSpeed, 0, 0);
                animator.SetBool("runningLeft", false);
                runningLeft = false;
            }
            else if (Timer > 3f && Timer < 6f)
            {
                transform.position += new Vector3(-patrolSpeed, 0, 0);
                animator.SetBool("runningLeft", true);
                runningLeft = true;
            }
            else if (Timer >= 6f)
            {
                Timer = 0f;
            }
            alertIndicator.color = Color.green;
        }
        else
        {
            alertIndicator.color = Color.red;
            if(transform.position.x - playerBody.position.x > 0){
                                transform.position += new Vector3(-alertSpeed, 0, 0);
                animator.SetBool("runningLeft", true);
                runningLeft = true;
            }
            else{
                                transform.position += new Vector3(alertSpeed, 0, 0);
                animator.SetBool("runningLeft", false);
                runningLeft = false;
            }
        }
    }
}
