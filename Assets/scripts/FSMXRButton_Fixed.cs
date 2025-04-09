using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class FSMXRButton_Fixed : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private float pressDistance = 0.1f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private AudioClip pressSound;
    [SerializeField] private AudioClip releaseSound;
    [SerializeField] private AudioClip invalidSound;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color pressedColor = Color.green;
    [SerializeField] private Color disabledColor = Color.gray;
    [SerializeField] private float hapticIntensity = 0.5f;
    [SerializeField] private float hapticDuration = 0.1f;

    [Header("Progress Indicator")]
    [SerializeField] private bool showProgressIndicator = true;
    [SerializeField] private GameObject progressIndicatorPrefab;
    [SerializeField] private Transform progressIndicatorParent;

    [Header("State Machine Integration")]
    [SerializeField] private FSMButtonPuzzleController_Fixed puzzleController;
    [SerializeField] private int buttonIndex;

    [Header("Events")]
    public UnityEvent onButtonPressed;
    public UnityEvent onValidPress;
    public UnityEvent onInvalidPress;

    private XRBaseInteractable interactable; 
    private Vector3 startPosition;
    private Vector3 pressedPosition;
    private bool isPressed = false;
    private AudioSource audioSource;
    private Renderer buttonRenderer;
    private Material buttonMaterial;
    private GameObject[] progressIndicators;
    private int lastProgressCount = 0;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>(); 
        audioSource = gameObject.AddComponent<AudioSource>();
        buttonRenderer = GetComponent<Renderer>();
        
        if (buttonRenderer != null)
        {
            buttonMaterial = new Material(buttonRenderer.material);
            buttonRenderer.material = buttonMaterial;
            SetButtonColor(normalColor);
        }
    }

    private void Start()
    {
        startPosition = transform.localPosition;
        pressedPosition = startPosition - new Vector3(0, pressDistance, 0);
        
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonGrabbed);
            interactable.selectExited.AddListener(OnButtonReleased);
        }
        else
        {
            Debug.LogError("XRBaseInteractable component not found on button!");
        }
        
        if (showProgressIndicator && puzzleController != null && progressIndicatorPrefab != null)
        {
            SetupProgressIndicators();
        }
    }
    
    private void Update()
    {
        if (showProgressIndicator && puzzleController != null && progressIndicators != null)
        {
            UpdateProgressIndicators();
        }
        
        if (puzzleController != null)
        {
            UpdateButtonVisuals();
        }
    }

    private void OnButtonGrabbed(SelectEnterEventArgs args)
    {
        if (puzzleController != null && 
            (puzzleController.State != FSMButtonPuzzleController_Fixed.PuzzleState.WaitingForInput ||
             puzzleController.State == FSMButtonPuzzleController_Fixed.PuzzleState.Solved))
        {
            PlayInvalidInteractionFeedback(args.interactorObject);
            return;
        }
        
        StartCoroutine(AnimateButtonPress());
        
        if (args != null && args.interactorObject is XRBaseInputInteractor controllerInteractor && 
            controllerInteractor.xrController != null)
        {
            controllerInteractor.xrController.SendHapticImpulse(hapticIntensity, hapticDuration);
        }
        
        if (pressSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(pressSound);
        }
        
        if (buttonMaterial != null)
        {
            SetButtonColor(pressedColor);
        }
        
        onButtonPressed.Invoke();
        onValidPress.Invoke();
    }

    private void OnButtonReleased(SelectExitEventArgs args)
    {
        if (puzzleController != null && 
            puzzleController.State != FSMButtonPuzzleController_Fixed.PuzzleState.Solved)
        {
            if (releaseSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(releaseSound);
            }
        }
        
        if (puzzleController != null && 
            puzzleController.State != FSMButtonPuzzleController_Fixed.PuzzleState.Solved)
        {
            SetButtonColor(normalColor);
        }
    }
    
    private void PlayInvalidInteractionFeedback(IXRInteractor interactor)
    {
        if (invalidSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(invalidSound);
        }
        
        if (interactor is XRBaseInputInteractor controllerInteractor && 
            controllerInteractor.xrController != null)
        {
            controllerInteractor.xrController.SendHapticImpulse(hapticIntensity * 0.5f, hapticDuration * 0.5f);
        }
        
        onInvalidPress.Invoke();
    }

    private IEnumerator AnimateButtonPress()
    {
        float elapsedTime = 0f;
        Vector3 currentPosition = transform.localPosition;
        
        while (elapsedTime < 0.1f)
        {
            transform.localPosition = Vector3.Lerp(currentPosition, pressedPosition, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = pressedPosition;
        isPressed = true;
        
        yield return new WaitForSeconds(0.1f);
        
        elapsedTime = 0f;
        while (elapsedTime < 0.2f)
        {
            transform.localPosition = Vector3.Lerp(pressedPosition, startPosition, elapsedTime / 0.2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = startPosition;
        isPressed = false;
    }
    
    private void SetupProgressIndicators()
    {
        if (progressIndicatorPrefab == null)
        {
            Debug.LogWarning("Progress indicator prefab not assigned!");
            return;
        }
        
        int requiredPresses = 1;
        if (puzzleController != null)
        {
            var buttonConfig = puzzleController.GetButtonConfig(buttonIndex);
            if (buttonConfig != null)
            {
                requiredPresses = buttonConfig.requiredPresses;
            }
        }
        
        if (progressIndicatorParent == null)
        {
            GameObject parent = new GameObject($"Button{buttonIndex}_ProgressIndicators");
            parent.transform.SetParent(transform.parent);
            parent.transform.position = transform.position + new Vector3(0, 0.1f, 0);
            progressIndicatorParent = parent.transform;
        }
        
        progressIndicators = new GameObject[requiredPresses];
        float spacing = 0.05f;
        float startOffset = -((requiredPresses - 1) * spacing) / 2f;
        
        for (int i = 0; i < requiredPresses; i++)
        {
            Vector3 position = progressIndicatorParent.position + new Vector3(startOffset + i * spacing, 0, 0);
            progressIndicators[i] = Instantiate(progressIndicatorPrefab, position, Quaternion.identity, progressIndicatorParent);
            progressIndicators[i].name = $"ProgressIndicator_{i+1}";
            
            Renderer indicatorRenderer = progressIndicators[i].GetComponent<Renderer>();
            if (indicatorRenderer != null)
            {
                indicatorRenderer.material.color = Color.gray;
            }
        }
    }
    
    private void UpdateProgressIndicators()
    {
        if (progressIndicators == null || progressIndicators.Length == 0)
            return;
            
        int currentPresses = 0;
        if (puzzleController != null)
        {
            var buttonConfig = puzzleController.GetButtonConfig(buttonIndex);
            if (buttonConfig != null)
            {
                currentPresses = buttonConfig.currentPresses;
            }
        }
        
        if (currentPresses != lastProgressCount)
        {
            lastProgressCount = currentPresses;
            
            for (int i = 0; i < progressIndicators.Length; i++)
            {
                if (progressIndicators[i] != null)
                {
                    Renderer indicatorRenderer = progressIndicators[i].GetComponent<Renderer>();
                    if (indicatorRenderer != null)
                    {
                        indicatorRenderer.material.color = (i < currentPresses) ? Color.green : Color.gray;
                    }
                }
            }
        }
    }
    
    private void UpdateButtonVisuals()
    {
        if (puzzleController == null) return;
        
        switch (puzzleController.State)
        {
            case FSMButtonPuzzleController_Fixed.PuzzleState.Solved:
                SetButtonColor(Color.green);
                break;
                
            case FSMButtonPuzzleController_Fixed.PuzzleState.Failed:
                SetButtonColor(Color.red);
                break;
                
            case FSMButtonPuzzleController_Fixed.PuzzleState.WaitingForInput:
                SetButtonColor(normalColor);
                break;
                
            case FSMButtonPuzzleController_Fixed.PuzzleState.Processing:
            case FSMButtonPuzzleController_Fixed.PuzzleState.Evaluating:
            case FSMButtonPuzzleController_Fixed.PuzzleState.Resetting:
                SetButtonColor(Color.Lerp(normalColor, disabledColor, 0.5f));
                break;
        }
    }
    
    private void SetButtonColor(Color color)
    {
        if (buttonMaterial != null)
        {
            buttonMaterial.color = color;
        }
    }

    private void OnDestroy()
    {
        if (buttonMaterial != null)
        {
            Destroy(buttonMaterial);
        }
        
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnButtonGrabbed);
            interactable.selectExited.RemoveListener(OnButtonReleased);
        }
    }
}