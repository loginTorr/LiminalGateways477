using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2Ambient : MonoBehaviour {
    public float SoundInterval = 30.0f;

    void Start() { 
    }

    void Update() { 
        SoundInterval -= Time.deltaTime;

        if (SoundInterval <= 0.0f){ PlayRoom2Ambience(); }
    }

    void PlayRoom2Ambience() {
        SoundManager.Instance.Play(SoundType.ROOM2AMBIENT);
        SoundInterval = 120.0f;
    }
}
