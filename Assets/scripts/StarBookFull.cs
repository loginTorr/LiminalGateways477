using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBookFull : MonoBehaviour
{
    public GameObject emptyStarBook;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = emptyStarBook.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
