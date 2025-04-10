using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SoundType {
    PICKUP,
    PUZZLECOMPLETE,
    ROOM1AMBIENT,
    ROOM2AMBIENT,
    TELEPORT,
    DONG,
}

public class SoundCollection {
    private AudioClip[] clips;
    private int lastClipIndex;

    public SoundCollection(params string[] clipNames) {
        this.clips = new AudioClip[clipNames.Length];
        for (int i = 0; i < clips.Length; i++) {
            clips[i] = Resources.Load<AudioClip>(clipNames[i]);
            if (clips[i] == null) {
                Debug.Log($"can't find audio clip {clipNames[i]}");
            }
        }
        lastClipIndex = -1;
    }

    public AudioClip GetRandClip() {
        if (clips.Length == 0) {
            Debug.Log("No clips to give");
            return null;
        }
        else if (clips.Length == 1) {
            return clips[0];
        }
        else {
            int index = lastClipIndex;
            while (index == lastClipIndex) {
                index = Random.Range(0, clips.Length);
            }
            lastClipIndex = index;
            return clips[index];
        }
    }

}

public class SoundManager : MonoBehaviour {
    public float mainVolume = 1.0f;
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;

    public static SoundManager Instance { get; private set; }

    // unity life cycle
    private void Awake() {
        Instance = this;
        audioSrc = GetComponent<AudioSource>();
        sounds = new() {
            { SoundType.PICKUP, new("Pickup1") },
            { SoundType.PUZZLECOMPLETE, new("Magic") },
            { SoundType.ROOM1AMBIENT, new("Room1Ambient") },
            { SoundType.ROOM2AMBIENT, new("Room2Ambient") },
            { SoundType.TELEPORT, new("Teleport") },
            { SoundType.DONG, new("Dong") },
        };
    }

    public void Play(SoundType type, AudioSource audioSrc = null) {
        if (sounds.ContainsKey(type)) {
            audioSrc ??= this.audioSrc;
            audioSrc.volume = Random.Range(0.70f, 1.0f) * mainVolume;
            audioSrc.pitch = Random.Range(0.75f, 1.25f);
            audioSrc.clip = sounds[type].GetRandClip();
            audioSrc.Play();
        }
    }
}