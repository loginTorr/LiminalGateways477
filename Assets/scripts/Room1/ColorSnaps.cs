using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ColorSnaps : MonoBehaviour
{
    private XRSocketInteractor socket;
    
    public ColorPuzzle ColorPuzzleScript;


    // Start is called before the first frame update

    void Awake()
    {
        // Grabs references, ensuring they're set before OnEnable
        socket = GetComponent<XRSocketInteractor>();

    }

    void Start()
    {
        ColorPuzzleScript = FindObjectOfType<ColorPuzzle>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSocketSelectEntered(SelectEnterEventArgs args)
    {
        // Retrieve the snapped object's GameObject.
        GameObject book = args.interactableObject.transform.gameObject;

        if (book.name.Contains("BlueBook") & this.gameObject.name.Contains("BookCaseSnap1"))
        {
            ColorPuzzleScript.count++; //print(ColorPuzzleScript.count);
            ColorPuzzleScript.colorPuzzleState = ColorPuzzleState.Solving;
        }

        if (book.name.Contains("RedBook") & this.gameObject.name.Contains("BookCaseSnap2"))
        {
            ColorPuzzleScript.count++; //print(ColorPuzzleScript.count);
            ColorPuzzleScript.colorPuzzleState = ColorPuzzleState.Solving;

        }

        if (book.name.Contains("GreenBook") & this.gameObject.name.Contains("BookCaseSnap3"))
        {
            ColorPuzzleScript.count++; //print(ColorPuzzleScript.count);
            ColorPuzzleScript.colorPuzzleState = ColorPuzzleState.Solving;

        }

    }

    private void OnSocketSelectExited(SelectExitEventArgs args)
    {
        // Retrieve the snapped object's GameObject.
        GameObject book = args.interactableObject.transform.gameObject;

        if (book.name.Contains("BlueBook"))
        {
            ColorPuzzleScript.count--; //print(ColorPuzzleScript.count);
        }

        if (book.name.Contains("RedBook"))
        {
            ColorPuzzleScript.count--; //print(ColorPuzzleScript.count);
        }

        if (book.name.Contains("GreenBook"))
        {
            ColorPuzzleScript.count--; //print(ColorPuzzleScript.count);
        }

        if (ColorPuzzleScript.count <= 0)
        {
            ColorPuzzleScript.count = 0;
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

