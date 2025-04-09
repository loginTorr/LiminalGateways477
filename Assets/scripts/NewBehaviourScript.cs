using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 1;
        Invoke("Timer", 60f);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Timer", 60f);
        if (count == 1)
        {
            StartCoroutine(Timer());
        }
        count = 0;
    }

    IEnumerator Timer()
    {

        yield return new WaitForSeconds(0.1f);
    }
}
