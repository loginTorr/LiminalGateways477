using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = NumPuzzleState;

public enum NumPuzzleState {
    IDLE,
    ONE,
    TWO,
    THREE,
    FOUR,
    ERROR,
}

public class NumberPuzzle : MonoBehaviour {
    public State State { get; private set; }
    void Start() {
    }

    void Update() {
    }
}
