using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { Room1, Room2 };
public enum lecturnState { inactive, CheckBook, NatureBook, VideoBook, StarBook };
public enum colorPuzzle { inactive, empty, RedBook, PinkBook, GreenBook, YellowBook };

public class Game : MonoBehaviour
{
    public GameState curRoom;

    // Start is called before the first frame update
    void Start()
    {
        curRoom = GameState.Room1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (curRoom)
        {
            case GameState.Room1:
                print("Room1");
                break;

            case GameState.Room2:
                print("Room2");
                break;
        }
    }
}
