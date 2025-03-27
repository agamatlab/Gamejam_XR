using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerAnimator : MonoBehaviour
{

    float direction = 0;
    [SerializeField]
    private float animationSpeed = 50;
    float animationConstant;

    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animationConstant = 1 / animationSpeed;
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        playerAnimator.SetFloat("direction", direction);

        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal > 0)
        {
            direction += animationConstant;
        }
        else if(horizontal < 0)
        {

            direction -= animationConstant;
        }
        else
        {
            if(direction < -0.1 || direction > 0.1)
            {
                direction -= (direction/direction)* animationConstant;
            }
        }

        direction = Mathf.Clamp(direction, -1, 1);

        if (horizontal != 0)
        {
            print(horizontal);
            if(horizontal < 0)
            {
                playerAnimator.SetBool("facingLeft", true);
            }
            else
            {
                playerAnimator.SetBool("facingLeft", false);
            }
            playerAnimator.SetBool("running", true);
        }
        else
        {
            playerAnimator.SetBool("running", false);

        }

        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.SetBool("swing1", true);
        }
        else if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("swing normal"))
        {
            playerAnimator.SetBool("swing1", false);

        }



    }
}
