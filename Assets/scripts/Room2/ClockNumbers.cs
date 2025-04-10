using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using State = ClockPuzzleState;


public enum ClockPuzzleState
{
    IDLE,
    ONE_CORRECT,
    TWO_CORRECT,
    THREE_FINAL,
    ERROR,
}

public class ClockNumbers : MonoBehaviour
{
    public int MinuteNumber;
    public int HourNumber;
    public int LastMinuteNumber;
    public int LastHourNumber;

    public ClockPuzzle clockPuzzleScript;
    public State State { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        LastMinuteNumber = 0;
        LastHourNumber = 0;
        State = State.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case State.IDLE:
                if (LastHourNumber == 4 & LastMinuteNumber == 20) { ChangeState(State.ONE_CORRECT); }
                else { ChangeState(State.ERROR); }
                break;

            case State.ONE_CORRECT:
                if (LastHourNumber == 2 & LastMinuteNumber == 0) { ChangeState(State.TWO_CORRECT); }
                else { ChangeState(State.ERROR); }
                break;

            case State.TWO_CORRECT:
                if (LastHourNumber == 10 & LastMinuteNumber == 15) { ChangeState(State.THREE_FINAL); }
                else { ChangeState(State.ERROR); }
                break;

            case State.ERROR:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject hand = other.gameObject;

        if (clockPuzzleScript.isMinuteRotating == false && hand.name.Contains("MinuteHand"))
        {
            clockPuzzleScript.SetMinute(MinuteNumber);
        }
        else if (clockPuzzleScript.isHourRotating == false && hand.name.Contains("HourHand"))
        {
            clockPuzzleScript.SetHour(HourNumber);
        }
    }

    private void ChangeState(State NewState)
    {
        if (State != NewState)
        {
            State = NewState;

            switch (NewState)
            {
                case State.IDLE:
                    // does nothing
                    break;

                case State.ONE_CORRECT:
                case State.TWO_CORRECT:
                    print("correct");
                    break;

                case State.THREE_FINAL:
                    // spawn in the object
                    print("puzzle complete");
                    //SoundManager.Instance.Play(SoundType.PUZZLECOMPLETE);
                    break;

                case State.ERROR:
                    print("wrong");
                    ChangeState(State.IDLE);
                    break;

            }
        }
    }

}
