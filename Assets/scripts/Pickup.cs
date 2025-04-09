using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pickup : MonoBehaviour {
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;

    private void Awake() {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnPickup);
    }

    private void OnDestroy() {
        interactable.selectEntered.RemoveListener(OnPickup);
    }

    private void OnPickup(SelectEnterEventArgs args) {
        SoundManager.Instance.Play(SoundType.PICKUP);
    }


    void Start() {  
    }

    void Update() { 
    }
}
