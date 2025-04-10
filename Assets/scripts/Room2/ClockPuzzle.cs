using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ClockPuzzle : MonoBehaviour
{
    public bool isMinuteRotating;
    public bool isHourRotating;
    public int curMinuteNum;
    public int curHourNum;

    public GameObject minuteHand;
    public GameObject hourHand;

    public ClockNumbers clockNumberScript;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMinute(int number)
    {
        clockNumberScript.LastMinuteNumber = number;
        Debug.Log("Minute Num == " + clockNumberScript.LastMinuteNumber);
    }

    public void SetHour(int number)
    {
        clockNumberScript.LastHourNumber = number;
        Debug.Log("Hour Num == " + clockNumberScript.LastHourNumber);
    }


    public void MinuteRotating() { isMinuteRotating = true; Debug.Log("RotatingMin"); }

    public void MinuteDoneRotating() { isMinuteRotating = false; Debug.Log("Done RotatingMin"); }

    public void HourRotating() { isHourRotating = true; Debug.Log("RotatingHour"); }

    public void HourDoneRotating() { isHourRotating = false; Debug.Log("Done RotatingHour"); }

}