using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthbar;
    public Transform enemyTransform;
    public Vector3 offset = new Vector3(0, 1, 0);
    private Camera mainCamera;
    public EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject enemy = GameObject.Find("Banana Man");
        enemyTransform = transform.parent.parent;
        GameObject enemy =enemyTransform.gameObject;
        Transform body = enemy.transform.Find("Body");
        enemyController = body.GetComponent<EnemyController>();

        healthbar = GetComponent<Slider>();
        offset = new Vector3(0, 1, 0);
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(enemyTransform.position + offset);


        healthbar.transform.position = screenPosition;


        healthbar.transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
        healthbar.value = (float)(enemyController.health);
    }
}
