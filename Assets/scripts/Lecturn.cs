using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Lecturn : MonoBehaviour
{
    private lecturnState curBook;
    public Game gameScript;
    private XRBaseInteractable interactable;




    // Start is called before the first frame update
    void Start()
    {
        curBook = lecturnState.inactive;
        gameScript = FindObjectOfType<Game>(); //grabs objects from game script
        interactable = GetComponent<XRBaseInteractable>();
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
                ifBook();
                break;

            case lecturnState.NatureBook:
                break;

            case lecturnState.VideoBook:
                break;

            case lecturnState.StarBook:
                gameScript.curRoom = GameState.Room2;
                curBook = lecturnState.inactive;
                break;
        }
        
    }

    public void ifBook()
    {
        Debug.Log("Works!");
        curBook = lecturnState.StarBook;
    }


}
