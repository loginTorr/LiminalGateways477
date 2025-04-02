using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { Room1, Room2, changeRoom, changingRoom };
public enum lecturnState { inactive, CheckBook, NatureBook, VideoBook, StarBook };


public class Game : MonoBehaviour
{
    public GameState curRoomState;
    public GameObject Room1Object;
    public GameObject Room2Object;

    private GameObject curRoomObject;

    // Start is called before the first frame update
    void Start()
    {
        curRoomState = GameState.Room1;
        curRoomObject = Room1Object;
    }

    // Update is called once per frame
    void Update()
    {
        switch (curRoomState)
        {
            case GameState.Room1:
                print("Room1");
                break;

            case GameState.Room2:
                print("Room2");
                break;

            case GameState.changeRoom:
                switchRoom();
                curRoomState = GameState.changingRoom;
                break;

            case GameState.changingRoom:
                break;
        }
    }

    void switchRoom()
    {
        if (curRoomObject.name.Contains("Room1"))
        {
            Room1Object.SetActive(false);
            Room2Object.SetActive(true);
            curRoomObject = Room2Object;
            curRoomState = GameState.Room2;
        }

        if (curRoomObject.name.Contains("Room2"))
        {
            Room2Object.SetActive(false);
            Room1Object.SetActive(true);
            curRoomObject = Room1Object;
            curRoomState = GameState.Room1;
        }
    }
}
