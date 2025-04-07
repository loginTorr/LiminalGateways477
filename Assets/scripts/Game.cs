using System.Collections;
using System.Collections.Generic;
using Tripolygon.UModelerX.Runtime.MessagePack.Resolvers;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { Room1, Room2, changeRoom };
public enum ColorPuzzleState { Unsolved, Solving, Solved }

public enum StarBookState { Empty, Filling, Full}

public class Game : MonoBehaviour
{
    public GameState curRoomState;

    public GameObject Room1Object;
    public GameObject Room2Object;

    public int roomNum;

    // Start is called before the first frame update
    void Start()
    {
        roomNum = 1;
        curRoomState = GameState.Room1;
    }

    // Update is called once per frame
    void Update()
    {
        //GameState Loop
        switch (curRoomState)
        {
            case GameState.Room1:
                //print("Room1");
                break;
     
            case GameState.Room2:
                //print("Room2");
                break;

            case GameState.changeRoom:
                switchRoom();
                break;

        }

    }

    public void switchRoom()
    {
        if (roomNum == 1)
        {
            Room1Object.SetActive(false);
            Room2Object.SetActive(true);
            roomNum = 2;

        }

        else if (roomNum == 2)
        {
            Room2Object.SetActive(false);
            Room1Object.SetActive(true);
            roomNum = 1;

        }
    }
}
