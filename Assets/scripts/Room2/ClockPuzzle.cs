using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool isMinuteRotating;
    public bool isHourRotating;
    public int curMinuteNum;
    public int curHourNum;

    public GameObject minuteHand;
    public GameObject hourHand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (isMinuteRotating == false & minuteHand.name.Contains("MinuteHand"))
        {
            //curNum = object.name
            //set curMinuteNum == curNum
            Debug.Log("Minute Num == ");
        }

        if (isHourRotating == false & hourHand.name.Contains("HourHand"))
        {
            //curNum = object.name
            //set curMinuteNum == curNum
            Debug.Log("Hour Num == ");
        }

        else
        {
            //getLastCurNum
        }
    }

    public void MinuteRotating()
    {
        isMinuteRotating = true;
        Debug.Log("RotatingMin");
    }

    public void MinuteDoneRotating()
    {
        isMinuteRotating = false; Debug.Log("Done RotatingMin");

    }


}