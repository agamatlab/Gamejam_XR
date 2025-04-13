using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool alive;
    public int health;
    private PlayerAnimator playerAnimator;
    public GameObject enemy;
    public GameObject player;
    public bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        //enemy = GameObject.Find("Banana Man");
        player =GameObject.FindWithTag("Player");
        Transform exportedKnight = player.transform.Find("exported knight");
        playerAnimator = exportedKnight.GetComponent<PlayerAnimator>();
        alive = true;
        health = 100;
        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            alive = false;
            Destroy(enemy);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasCollided)
        {
            return;
        }

        if (playerAnimator.isSwinging && (other.gameObject.CompareTag("weaponR") || other.gameObject.CompareTag("weaponL")))
        {
            health -= 40;
            hasCollided = true;
        }

    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("weaponR") || other.gameObject.CompareTag("weaponL")){
        hasCollided = false;}
    }
}
