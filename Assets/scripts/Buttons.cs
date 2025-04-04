using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {
    public NumberPuzzle Puzzle;
    public int Number;
    public NumPuzzleState PuzzleState;

    void Start() {
    }

    void Update() {  
    }

    public void ButtonPress() {
        if (Puzzle.State != NumPuzzleState.THREE_FINAL) {
            Puzzle.Press(Number);
        }
    }
}
