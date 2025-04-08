using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBook : MonoBehaviour
{
    public int count;

    public GameObject CompletedBook;
    public GameObject EmptyBook;
    public GameObject piece1;
    public GameObject piece2;

    public Vector3 bar;

    public StarBookState starBookState;



    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (starBookState)
        {
            case StarBookState.Empty:
                break;

            case StarBookState.Filling:
                if (count == 2)
                {
                    starBookState = StarBookState.Full;
                    count++;
                    ChangeGameObects();
                }
                else
                {
                    starBookState = StarBookState.Empty;
                }
                break;

            case StarBookState.Full:
                break;
        }

    }

    void ChangeGameObects()
    {
        CompletedBook.SetActive(true); 

        EmptyBook.SetActive(false); piece1.SetActive(false); piece2.SetActive(false);
    }

}
