using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private float movementSpeed = 10;
    Animator playerAnimator;
    PlayerAnimator playerAniamationScript;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerAniamationScript = GetComponentInChildren<PlayerAnimator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");


        if (playerAniamationScript.isSwinging)
        {
            rb.velocity = Vector3.zero;
        }
        else if (horizontal != 0f )
        {

            rb.velocity = new Vector3(horizontal, 0, 0) * movementSpeed;
        }
        else
        {

            rb.velocity = Vector3.zero;
        }
    }
}