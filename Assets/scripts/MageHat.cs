using System.Collections;
using System.Collections.Generic;
using SerializableCallback;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MageHat : MonoBehaviour {
    public GameObject Clock;
    private XRSocketInteractor Socket;
    public GameObject Hat;

    void Awake() {
        Socket = GetComponent<XRSocketInteractor>();
    }

    void Start() {
        Clock.SetActive(false);
    }

    void Update() {  
    }

    private void OnSocketSelectEntered(SelectEnterEventArgs args) {
        GameObject Hat = args.interactableObject.transform.gameObject;

        if (Hat.name.Contains("Mage Hat")) { Clock.SetActive(true); }
        else { Clock.SetActive(false); }
    }

    private void OnSocketSelectExited(SelectExitEventArgs args) {
        GameObject Hat = args.interactableObject.transform.gameObject;

        if (Hat.name.Contains("Mage Hat")) { Clock.SetActive(false); }
    }

    private void OnEnable() {
        if (Socket != null) {
            Socket.selectEntered.AddListener(OnSocketSelectEntered);
            Socket.selectExited.AddListener(OnSocketSelectExited);
        }
    }

    private void OnDisable() {
        if (Socket != null) {
            Socket.selectEntered.RemoveListener(OnSocketSelectEntered);
            Socket.selectExited.RemoveListener(OnSocketSelectExited);
        }
    }
}
