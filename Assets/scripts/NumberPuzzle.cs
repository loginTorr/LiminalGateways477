using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = NumPuzzleState;

private enum NumPuzzleState {
    IDLE,
    ONE,
    TWO,
    THREE_FINAL,
    ERROR,
}


public class NumberPuzzle : MonoBehaviour {
    public State State { get; private set; }
    private Numbers LastNumber;

    void Start() {
        LastNumber = Numbers.NONE
        State = State.IDLE
    }

    void Update() { 
        if (LastNumber == Numbers.NONE) {
            return;
        }

        switch (State) {
            case State.IDLE:
            if (LastNumber == Numbers.ONE) { ChangeState(State.ONE); }
            else { ChangeState(State.ERROR); }
            break;

            case State.ONE:
            if (LastNumber == Numbers.TWO) { ChangeState(State.TWO); }
            else { ChangeState(State.ERROR); }
            break;

            case State.TWO:
            if (LastNumber == Numbers.THREE_FINAL) { ChangeState(State.THREE_FINAL); }
            else { ChangeState(State.ERROR); }
            break;

            case State.ERROR:
            ChangeState(State.IDLE);
            break;
        }
        LastNumber = Numbers.NONE;
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
                print("correct");
                break;

                case State.THREE_FINAL:
                PuzzleComplete.SetActive(true);
                break;

                case State.ERROR:
                print("wrong");
                ChangeState(State.IDLE);
                break;

            }
        }
    }

    public void Press(Numbers Number) {
    LastNumber = Number;
    }
}
