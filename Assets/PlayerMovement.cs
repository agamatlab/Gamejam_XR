using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private float movementSpeed = 10;

    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("swing normal"))
        {
            rb.velocity = Vector3.zero;
        }
        else if (horizontal != 0f)
        {
            rb.velocity = new Vector3(1,0,0)*horizontal*movementSpeed;
        }

    }
}
