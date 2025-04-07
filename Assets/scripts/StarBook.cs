using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBook : MonoBehaviour
{
    public int count;

    public GameObject CompletedBook;
    public GameObject EmptyBook;

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
                    print("socketed!");
                    CompletedBook.SetActive(true); EmptyBook.SetActive(false);
                    starBookState = StarBookState.Full;
                    count++;

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

}
