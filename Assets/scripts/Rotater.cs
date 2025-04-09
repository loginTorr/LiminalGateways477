using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public GameObject rotateController;

    public HandRotator HandRotatorScript;
    public MinuteRotator MinuteRotatorScript;

    public GameObject HandClock;
    public GameObject MinuteClock;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandClock.transform.Rotate(HandRotatorScript.m_Velocity * Time.deltaTime);
        MinuteClock.transform.Rotate(MinuteRotatorScript.m_Velocity * Time.deltaTime);

    }

}
