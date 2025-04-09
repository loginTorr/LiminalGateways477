using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinuteRotator : MonoBehaviour
{
    public Vector3 m_Velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_Velocity * Time.deltaTime);
    }
}
