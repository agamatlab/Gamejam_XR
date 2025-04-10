using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float Timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("runningLeft", true);
        Timer = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        //transform.position += new Vector3(0.005f,0,0);
        if (Timer < 3f && Timer >= 0f)
        {
            
            transform.position += new Vector3(0.002f,0,0);
            animator.SetBool("runningLeft", false);
        }
        else if (Timer > 3f && Timer<6f)
        {
        transform.position += new Vector3(-0.002f,0,0);
            animator.SetBool("runningLeft", true);
        }
        else if (Timer >=6f){
            Timer = 0f;
        }
    }
}
