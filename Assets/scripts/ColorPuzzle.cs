using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class ColorPuzzle : MonoBehaviour
{
    public int count;
    public ColorPuzzleState colorPuzzleState;

    public GameObject starBook;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (colorPuzzleState)
        {
            case ColorPuzzleState.Unsolved:
                break;

            case ColorPuzzleState.Solving:
                if (count == 3)
                {
                    print("puzzle solved1");
                    colorPuzzleState = ColorPuzzleState.Solved;
                    count++;
                    starBook.SetActive(true);

                }
                else
                {
                    colorPuzzleState = ColorPuzzleState.Unsolved;
                }
                    break;

            case ColorPuzzleState.Solved:
                break;
        }

    }

}

