using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnemy : MonoBehaviour
{
    public GameObject objectToClone;
    public bool master = true;

    public int numberOfClones = 5;
    private CloneEnemy cloneClone;
    // Start is called before the first frame update
    void Start()
    {
        objectToClone = transform.gameObject;
        if (master)
        {
            for (int i = 0; i < numberOfClones; i++)
            {

                GameObject clone = Instantiate(objectToClone);
                clone.transform.position += new Vector3(1, 0, 0);
                cloneClone = clone.GetComponent<CloneEnemy>();
                cloneClone.master = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
