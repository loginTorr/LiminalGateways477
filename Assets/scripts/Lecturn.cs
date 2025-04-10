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
    public GameObject EndRoom;

    private XRSocketInteractor socket;




    void Awake()
    {
        // Grabs references, ensuring they're set before OnEnable
        socket = GetComponent<XRSocketInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
            StartScreen.SetActive(false); NoScene.SetActive(true);
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
