using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class ColorPuzzle : MonoBehaviour
{
    public int count;
    public ColorPuzzleState colorPuzzleState;

    public GameObject RightStarPiece;
    public GameObject emptyBookcase;
    public GameObject completedBookcase;
    public GameObject rBook;
    public GameObject gBook;
    public GameObject bBook;

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
                    ChangeGameObjects();
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

    void ChangeGameObjects()
    {
        //enable completed bookcase so players can't re-activate puzzle, spawn empty star book
        completedBookcase.SetActive(true); RightStarPiece.SetActive(true);

        //disable puzzle objects
        emptyBookcase.SetActive(false); rBook.SetActive(false); gBook.SetActive(false); bBook.SetActive(false);

    }
}

