// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public enum soundType {
//     CLICK,
//     SOLVED,
// }

// public class soundCollection {
//     private audioClip[] clips;
//     private int lastClipIndex;

//     public soundCollection(params string[] clipNames) {
//         this.clips = new audioCLip[clipNames.Length];
//         for (int i = 0; i < clips.Length; i++) {
//             clips[i] = Resources.Load<audioClip>("solved");
//             if (clips[1] == null) {
//                 Debug.Log($"cant find audio clip {clipNames[1]}");
//             }
//         }
//         lastClipIndex = -1;
//     }

//     public audioClip GetRandClip() {
//         if (clips.Length == 0) {
//             Debug.Log("no clips to give");
//             return null;
//         }
//         else if (clips.Length == 1) {
//             return clips[0];
//         }
//         else {
//             int index = lastClipIndex
//             while (index == lastClipIndex) {
//                 index = Random.Range(0, clips.Length);
//             }
//             lastClipIndex = index;
//             return clips[index];
//         }
//     }
// }



// public class soundManager : MonoBehaviour {
//     public float mainVolume = 1.0f;
//     private Dictionary<soundType, soundCollection> sounds;
//     private audioSource audioSrc;

//     public static soundManager Instance { get; private set; }

//     // unity life cycle
//     private void Awake() {
//         instance = this;
//         audioSrc = GetComponent<audioSource>();
//         sounds = new() {
//             { soundType.CLICK, new("clicks/click_1", "clicks/click_2", "clicks/click_3", 
//             "clicks/click_4") }
//             { soundType.SOLVED, new("solved") }
//         };
//     }

//     public void Play(soundType type, audioSource audioSrc = null) {
//         if (sounds.ContainsKey(type)) {
//             audioSrc ??= this.audioSrc;
//             audioSrc.volume = Random.Range(0.70f, 1.0f) * mainVolume;
//             audioSrc.pitch = Random.Range(0.75f, 1.25f);
//             audioSrc.clip = sounds[type].GetRandClip();
//             audioSrc.Play();
//         }
//     }

//     // Start is called before the first frame update
//     void Start() {
//     }

//     // Update is called once per frame
//     void Update() {
//     }
// }
