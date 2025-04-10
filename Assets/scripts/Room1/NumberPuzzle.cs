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
    FIVE_FINAL,
    ERROR,
}


public class NumberPuzzle : MonoBehaviour {
    public State State { get; private set; }
    public GameObject leftStarPiece;
    public int LastNumber;

    void Start() {
        LastNumber = 0;
        State = State.IDLE;
    }

    void Update() { 
        if (LastNumber == 0) {
            return;
        }

        switch (State) {
            case State.IDLE:
            if (LastNumber == 1) { ChangeState(State.ONE); }
            else { ChangeState(State.ERROR); }
            break;

            case State.ONE:
            if (LastNumber == 2) { ChangeState(State.TWO); }
            else { ChangeState(State.ERROR); }
            break;

            case State.TWO:
            if (LastNumber == 3) { ChangeState(State.THREE); }
            else { ChangeState(State.ERROR); }
            break;

            case State.THREE:
            if (LastNumber == 4) { ChangeState(State.FOUR); }
            else { ChangeState(State.ERROR); }
            break;

            case State.FOUR:
            if (LastNumber == 5) { ChangeState(State.FIVE_FINAL); }
            else { ChangeState(State.ERROR); }
            break;

            case State.ERROR:
            ChangeState(State.IDLE);
            break;
        }
        LastNumber = 0;
    }

    private void ChangeState(State NewState) {
        if (State != NewState) {
            State = NewState;
            
            switch (NewState) {
                case State.IDLE:
                // does nothing
                break;

                case State.ONE:
                case State.TWO:
                case State.THREE:
                case State.FOUR:
                print("correct");
                break;

                case State.FIVE_FINAL:
                // spawn in the object
                print("puzzle complete");
                SoundManager.Instance.Play(SoundType.PUZZLECOMPLETE);
                leftStarPiece.SetActive(true);
                break;

                case State.ERROR:
                print("wrong");
                ChangeState(State.IDLE);
                break;

            }
        }
    }

    public void Press(int Number) {
        LastNumber = Number;
    }
}
