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

    public lecturnState curBook;

    public GameObject NoScene;
    public GameObject NatureScene;
    public GameObject VideoScene;

    private XRSocketInteractor socket;




    void Awake()
    {
        // Grabs references, ensuring they're set before OnEnable
        socket = GetComponent<XRSocketInteractor>();

    }

    // Start is called before the first frame update
    void Start()
    {
        curBook = lecturnState.inactive;
        gameScript = FindObjectOfType<Game>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (curBook)
        {
            //switches the lecturn states to off when Room2 is entered
            case lecturnState.inactive:
                //switches to empty state when Room1 entered
                break;

            //continously calls a function to check for the books, switches to specific book state when conditions met
            case lecturnState.CheckBook:
                break;

            case lecturnState.NatureBook:
                break;

            case lecturnState.VideoBook:
                break;

            case lecturnState.StarBook:
                gameScript.curRoomState = GameState.changeRoom;
                curBook = lecturnState.inactive;
                break;
        }
        
    }

    private void OnSocketSelectEntered(SelectEnterEventArgs args)
    { 
        // Retrieve the snapped object's GameObject.
        GameObject book = args.interactableObject.transform.gameObject;

        if (book.name.Contains("NatureBook"))
        {
            NoScene.SetActive(false); NatureScene.SetActive(true);
        }
      
        if (book.name.Contains("VideoBook"))
        {
            NoScene.SetActive(false); VideoScene.SetActive(true);
        }

        if (book.name.Contains("StarBook"))
        {
            curBook = lecturnState.StarBook;
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
            curBook = lecturnState.inactive;
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
