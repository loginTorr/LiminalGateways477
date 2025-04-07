using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class StarSockets : MonoBehaviour
{
    private XRSocketInteractor socket;

    public StarBook StarBookScript;

    void Awake()
    {
        // Grabs references, ensuring they're set before OnEnable
        socket = GetComponent<XRSocketInteractor>();

    }

    // Start is called before the first frame update
    void Start()
    {
        StarBookScript = FindObjectOfType<StarBook>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnSocketSelectEntered(SelectEnterEventArgs args)
    {
        // Retrieve the snapped object's GameObject.
        GameObject starPiece = args.interactableObject.transform.gameObject;

        if (starPiece.name.Contains("RightStarPiece"))
        {
            StarBookScript.count++;
            StarBookScript.starBookState = StarBookState.Filling;
        }

        if (starPiece.name.Contains("LeftStarPiece"))
        {
            StarBookScript.count++;
            StarBookScript.starBookState = StarBookState.Filling;
        }
    }

    private void OnSocketSelectExited(SelectExitEventArgs args)
    {
        // Retrieve the snapped object's GameObject.
        GameObject starPiece = args.interactableObject.transform.gameObject;

        if (starPiece.name.Contains("RightStarPiece"))
        {
            StarBookScript.count--;
        }

        if (starPiece.name.Contains("LeftStarPiece"))
        {
            StarBookScript.count--;
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
