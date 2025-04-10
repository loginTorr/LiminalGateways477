using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Ambient : MonoBehaviour {
    public float SoundInterval = 30.0f;

    void Start() { 
    }

    void Update() { 
        SoundInterval -= Time.deltaTime;

        if (SoundInterval <= 0.0f){ PlayRoom1Ambience(); }
    }

    void PlayRoom1Ambience() {
        SoundManager.Instance.Play(SoundType.ROOM1AMBIENT);
        SoundInterval = 30.0f;
    }
}
