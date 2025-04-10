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

    private bool minuteCorrect;
    private bool hourCorrect;


    // Start is called before the first frame update
    void Start()
    {
        LastMinuteNumber = 0;
        LastHourNumber = 0;
        State = State.IDLE;
        minuteCorrect = false;
        hourCorrect = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (MinuteNumber != LastMinuteNumber || HourNumber != LastHourNumber)
        {
            switch (State)
            {
                case State.IDLE:
                    if (HourNumber == 4) hourCorrect = true;
                    if (MinuteNumber == 20) minuteCorrect = true;

                    if (hourCorrect && minuteCorrect)
                    {
                        Debug.Log("FirstRight");
                        ChangeState(State.ONE_CORRECT);
                        hourCorrect = false; minuteCorrect = false;
                    }
                    break;

                case State.ONE_CORRECT:
                    if (HourNumber == 2) hourCorrect = true; else { hourCorrect = false; }
                    if (MinuteNumber == 0) minuteCorrect = true; else { minuteCorrect = false; }

                    if (hourCorrect && minuteCorrect)
                    {
                        Debug.Log("SecondRight");

                        ChangeState(State.TWO_CORRECT);
                        hourCorrect = false; minuteCorrect = false;

                    }
                    break;

                case State.TWO_CORRECT:
                    if (HourNumber == 10) hourCorrect = true; else { hourCorrect = false; }
                    if (MinuteNumber == 15) minuteCorrect = true; else { minuteCorrect = false; }

                    if (hourCorrect && minuteCorrect)
                    {
                        Debug.Log("ThirdRight");
                        ChangeState(State.THREE_FINAL);
                    }
                    break;

                case State.ERROR:
                    if (MinuteNumber == 0 && HourNumber == 0)
                    {
                        ChangeState(State.IDLE);
                    }
                    break;
            }
        }
        LastMinuteNumber = MinuteNumber;
        LastHourNumber = HourNumber;
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

    private void ResetCorrectFlags()
    {
        minuteCorrect = false;
        hourCorrect = false;
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
                    ResetCorrectFlags();
                    break;

                case State.ONE_CORRECT:
                case State.TWO_CORRECT:
                    ResetCorrectFlags();
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
