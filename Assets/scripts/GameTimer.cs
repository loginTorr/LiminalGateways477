using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {
    public Game Game;
    public float TimerSound = 60.0f;
    public float TimeLeft = 600.0f;
    void Start() { 
    }

    void Update() { 
        if (Game.curRoomState != GameState.StartRoom || Game.curRoomState != GameState.GameOver) { 
            TimeLeft -= Time.deltaTime;
            TimerSound -= Time.deltaTime;
        }

        if (TimerSound <= 0.0f) { PlayDong(); }
        if (TimeLeft <= 0.0f) { GameOver(); }
    }

    void PlayDong() {
        SoundManager.Instance.Play(SoundType.DONG);
        TimerSound = 60.0f;
    }

    void GameOver() {
        TimeLeft = 600.0f;
        print("game over");
    }
    
}
