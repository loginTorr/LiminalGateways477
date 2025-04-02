using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class ColorPuzzle : MonoBehaviour
{
    public int count;


    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 3)
        {
            print("puzzle solved1");
        }

    }

}

