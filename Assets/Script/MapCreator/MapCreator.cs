using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject masterEnemy;
    public int numberOfClones = 2;
    // Start is called before the first frame update
    void Start()
    {
        masterEnemy = GameObject.Find("Banana Man");
        for (int i = 0; i < numberOfClones; i++)
            {

                GameObject clone = Instantiate(masterEnemy);
                clone.transform.position += new Vector3(i*1, 0, 0);

            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
