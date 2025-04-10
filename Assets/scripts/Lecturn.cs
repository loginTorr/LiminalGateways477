using System.Collections;
using System.Collections.Generic;
using SerializableCallback;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class Lecturn : MonoBehaviour
{

    public Game gameScript;

    public GameObject StartScreen;
    public GameObject NoScene;
    public GameObject NatureScene;
    public GameObject VideoScene;
    public GameObject GameOverScreen;
    public GameObject WinScreen;

    public GameObject EmptyBook;
    public GameObject CompletedBook;
    public GameObject WinBook;

    private XRSocketInteractor socket;


    private Vector3 EmptyBookInitPos;

    void Awake()
    {
        // Grabs references, ensuring they're set before OnEnable
        socket = GetComponent<XRSocketInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EmptyBookInitPos = EmptyBook.transform.position;
        gameScript = FindObjectOfType<Game>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSocketSelectEntered(SelectEnterEventArgs args)
    { 
        // Retrieve the snapped object's GameObject.
        GameObject book = args.interactableObject.transform.gameObject;

        if (book.name.Contains("EmptyStarBook") & gameScript.curRoomState == GameState.StartRoom)
        {
            StartScreen.SetActive(false); WinScreen.SetActive(false); GameOverScreen.SetActive(false); NoScene.SetActive(true);
            SoundManager.Instance.Play(SoundType.TELEPORT);
            gameScript.switchRoom();
        }

        if (book.name.Contains("NatureBook"))
        {
            NoScene.SetActive(false); NatureScene.SetActive(true);
        }
      
        if (book.name.Contains("VideoBook"))
        {
            NoScene.SetActive(false); VideoScene.SetActive(true);
        }

        if (book.name.Contains("StarBookFull"))
        {
            SoundManager.Instance.Play(SoundType.TELEPORT);
            gameScript.switchRoom();
        }

        if (book.name.Contains("EmptyStarBook") & gameScript.curRoomState == GameState.GameOver)
        {
            gameScript.roomNum = 0;
            GameOverScreen.SetActive(false); NoScene.SetActive(true);
            SoundManager.Instance.Play(SoundType.TELEPORT);
            gameScript.switchRoom();
        }

        if (book.name.Contains("WinBook"))
        {
            WinScreen.SetActive(true); NoScene.SetActive(false);
            EmptyBook.SetActive(true); CompletedBook.SetActive(false); WinBook.SetActive(false);
            EmptyBook.transform.position = EmptyBookInitPos;
            gameScript.roomNum = 3;
            gameScript.switchRoom();
        }

    }

    private void OnSocketSelectExited(SelectExitEventArgs args)
    {
        // Retrieve the snapped object's GameObject.
        GameObject book = args.interactableObject.transform.gameObject;

        if (book.name.Contains("NatureBook"))
        {
            NatureScene.SetActive(false); NoScene.SetActive(true);
        }

        if (book.name.Contains("VideoBook"))
        {
            VideoScene.SetActive(false); NoScene.SetActive(true);
        }

        if (book.name.Contains("StarBook"))
        {
            return;
        }
    }

    private void OnEnable()
    {
        // Subscribe to the socket's event.
        if (socket != null)
        {
            socket.selectEntered.AddListener(OnSocketSelectEntered);
            socket.selectExited.AddListener(OnSocketSelectExited);
        }

    }

    private void OnDisable()
    {
        // Unsubscribe when the object is disabled.
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnSocketSelectEntered);
            socket.selectExited.RemoveListener(OnSocketSelectExited);
        }
    }

}
