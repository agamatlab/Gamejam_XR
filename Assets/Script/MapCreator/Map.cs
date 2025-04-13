using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int[,] map;
    // Start is called before the first frame update
    void Start()
    {

        //the bottom left corner of map is 0,0,0
        //0 is empty, 1 is grass, 2 is land
        map = new int[,]
                {
            { 0, 0,0,0,0,0,0,0,0,0 },
            { 0, 0,0,0,0,0,0,0,0,0 },
            { 1, 1,1,0,1,1,1,1,1,1 },
            { 2, 2,2,0,2,2,2,2,2,2 },
                };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
