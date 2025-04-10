using System.Collections;
using System.Collections.Generic;
using Tripolygon.UModelerX.Runtime;
using Tripolygon.UModelerX.Runtime.MessagePack.Resolvers;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { StartRoom, StartGame, Room1, Room2, changeRoom, GameOver };
public enum ColorPuzzleState { Unsolved, Solving, Solved }
public enum StarBookState { Empty, Filling, Full}

public class Game : MonoBehaviour
{
    public GameState curRoomState;

    public GameObject StartRoom;
    public GameObject Room1Object;
    public GameObject Room2Object;


    public GameObject CompletedBook;
    public GameObject EmptyBook;

    public GameObject LooseScreen;
    public GameObject NoScreen;



    public int roomNum;

    // Start is called before the first frame update
    void Start()
    {
        roomNum = 0;
        curRoomState = GameState.StartRoom;
    }

    // Update is called once per frame
    void Update()
    {
        //GameState Loop
        switch (curRoomState)
        {
            case GameState.StartRoom:
                break;

             case GameState.StartGame:
                break;

            case GameState.Room1:
                //print("Room1");
                break;
     
            case GameState.Room2:
                //print("Room2");
                break;

            case GameState.changeRoom:
                switchRoom();
                break;

            case GameState.GameOver:
                break;

        }

    }

    public void switchRoom()
    {
        if (roomNum == 0)
        {
            curRoomState = GameState.Room1;
            StartRoom.SetActive(false); 
            Room1Object.SetActive(true);
            roomNum = 1;
        }

        else if (roomNum == 1)
        {
            curRoomState = GameState.Room2;
            Room1Object.SetActive(false);
            Room2Object.SetActive(true);
            roomNum = 2;
        }

        else if (roomNum == 2)
        {
            curRoomState = GameState.Room1;
            Room2Object.SetActive(false);
            Room1Object.SetActive(true);
            roomNum = 1;
        }

        else if (roomNum == 3)
        {
            curRoomState = GameState.StartRoom;

            StartRoom.SetActive(true);
            Room1Object.SetActive(false); Room2Object.SetActive(false);
            roomNum = 0;
        }

        else if (roomNum == 4)
        {
            curRoomState = GameState.StartRoom;

            StartRoom.SetActive(true);
            Room1Object.SetActive(false); Room2Object.SetActive(false);
            EmptyBook.SetActive(true); CompletedBook.SetActive(false);
            LooseScreen.SetActive(true); NoScreen.SetActive(false);
            roomNum = 0;

        }

    }
}
