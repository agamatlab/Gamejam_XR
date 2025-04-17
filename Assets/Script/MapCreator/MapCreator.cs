using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject masterEnemy;
    public GameObject player;
    public GameObject grass;
    public GameObject land;
    public int numberOfEnemy = 2;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        masterEnemy = GameObject.Find("swordenemy");
        grass = GameObject.Find("grass");
        land = GameObject.Find("Cube");
        grass.transform.localScale = new Vector3(1, 1, 1);
        land.transform.localScale = new Vector3(1, 1, 1);
        grass.transform.position = new Vector3(0, -3, 0);//lets hide it from the screen
        land.transform.position = new Vector3(0, -4, 0);
        
        player.transform.position = new Vector3(5, 2.2f, 0);
        masterEnemy.transform.position = new Vector3(8, 1.5f, 0);
        
        for (int i = 0; i < numberOfEnemy; i++)
        {

            GameObject clone = Instantiate(masterEnemy);
            clone.transform.position += new Vector3((i + 1) * 5, 0, 0);

        }
        
        int [,] mapArray = new int[,]
                {
            { 0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0 },
            { 0, 0,0,0,0,0,0,0,0,0, 0, 0,0,0,0,0,0,0,0,0},
            { 1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1 },
            { 2, 2,2,2,2,2,2,2,2,2,0, 0,0,0,0,0,0,0,0,0 },
                };
        int rows = mapArray.GetLength(0);
        int cols = mapArray.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (mapArray[i,j] == 1){
                    GameObject clone = Instantiate(grass);
                    clone.transform.position = new Vector3(j, rows - i -1, 0);
                }
                                if (mapArray[i,j] == 2){
                    GameObject clone = Instantiate(land);
                    clone.transform.position = new Vector3(j, rows - i -1, 0);
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
