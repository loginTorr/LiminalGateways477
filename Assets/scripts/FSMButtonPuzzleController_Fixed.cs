using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class FSMButtonPuzzleController_Fixed : MonoBehaviour
{
    // Finite State Machine states for the puzzle
    public enum PuzzleState
    {
        WaitingForInput,    // Initial state, waiting for player to interact
        Processing,         // Button press is being processed (prevents rapid input)
        Evaluating,         // Checking if the puzzle is solved
        Solved,             // Puzzle has been solved
        Failed,             // Incorrect attempt was made
        Resetting           // Resetting to initial state
    }
    
    [System.Serializable]
    public class ButtonConfig
    {
        public XRBaseInteractable buttonInteractable; 
        [Tooltip("The number of times this button needs to be pressed to solve the puzzle")]
        public int requiredPresses;
        public int currentPresses;
        public bool isCorrect => currentPresses == requiredPresses;
    }

    [Header("Button Configuration")]
    [SerializeField] private ButtonConfig[] buttons = new ButtonConfig[4];
    
    [Header("Puzzle Settings")]
    [SerializeField] private bool resetOnIncorrect = true;
    [SerializeField] private float buttonCooldown = 0.5f;
    [SerializeField] private float stateTransitionDelay = 0.2f;
    [SerializeField] private bool showDebugInfo = true;

    [Header("Events")]
    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleFailed;
    public UnityEvent onPuzzleReset;
    public UnityEvent onButtonPressed;

    // FSM current state
    private PuzzleState currentState = PuzzleState.WaitingForInput;
    private Dictionary<XRBaseInteractable, int> buttonIndexMap = new Dictionary<XRBaseInteractable, int>(); 
    private bool[] cooldowns = new bool[4];
    
    // Properties for external state checking
    public PuzzleState State => currentState;
    public bool IsSolved => currentState == PuzzleState.Solved;

    void Start()
    {
        InitializeButtons();
        TransitionToState(PuzzleState.WaitingForInput);
    }
    
    void Update()
    {
        switch (currentState)
        {
            case PuzzleState.WaitingForInput:
                break;
                
            case PuzzleState.Processing:
                break;
                
            case PuzzleState.Evaluating:
                break;
                
            case PuzzleState.Solved:
                break;
                
            case PuzzleState.Failed:
                break;
                
            case PuzzleState.Resetting:
                break;
        }
    }
    
    private void InitializeButtons()
{
    for (int i = 0; i < buttons.Length; i++)
    {
        ButtonConfig config = buttons[i];
        
        if (config.buttonInteractable != null)
        {
            int capturedIndex = i;
            
            buttonIndexMap[config.buttonInteractable] = capturedIndex;
            
            config.buttonInteractable.selectEntered.AddListener(
                new UnityEngine.Events.UnityAction<SelectEnterEventArgs>(
                    (args) => { HandleButtonActivated(capturedIndex); }
                )
            );
        }
        else
        {
            Debug.LogError($"Button {i} has no interactable assigned!");
        }
        
        config.currentPresses = 0;
    }
}

    private void HandleButtonActivated(int buttonIndex)
    {
        if (currentState == PuzzleState.WaitingForInput && !cooldowns[buttonIndex])
        {
            TransitionToState(PuzzleState.Processing);
            StartCoroutine(ProcessButtonPress(buttonIndex));
        }
    }
    
    private IEnumerator ProcessButtonPress(int buttonIndex)
    {
        StartCoroutine(ButtonCooldown(buttonIndex));
        
        buttons[buttonIndex].currentPresses++;
        
        onButtonPressed.Invoke();
        
        yield return new WaitForSeconds(stateTransitionDelay);
        
        TransitionToState(PuzzleState.Evaluating);
        StartCoroutine(EvaluatePuzzle());
    }
    
    private IEnumerator EvaluatePuzzle()
    {
        yield return new WaitForSeconds(stateTransitionDelay);
        
        bool allCorrect = true;
        bool anyOverPressed = false;
        
        foreach (ButtonConfig config in buttons)
        {
            if (!config.isCorrect)
            {
                allCorrect = false;
            }
            
            if (config.currentPresses > config.requiredPresses)
            {
                anyOverPressed = true;
            }
        }
        
        if (allCorrect)
        {
            TransitionToState(PuzzleState.Solved);
            onPuzzleSolved.Invoke();
        }
        else if (anyOverPressed && resetOnIncorrect)
        {
            TransitionToState(PuzzleState.Failed);
            onPuzzleFailed.Invoke();
            StartCoroutine(HandleFailedState());
        }
        else
        {
            TransitionToState(PuzzleState.WaitingForInput);
        }
    }
    
    private IEnumerator HandleFailedState()
    {
        yield return new WaitForSeconds(1.0f); 
        
        TransitionToState(PuzzleState.Resetting);
        StartCoroutine(ResetPuzzle());
    }
    
    private IEnumerator ResetPuzzle()
    {
        foreach (ButtonConfig config in buttons)
        {
            config.currentPresses = 0;
        }
        
        yield return new WaitForSeconds(stateTransitionDelay);
        
        onPuzzleReset.Invoke();
        
        TransitionToState(PuzzleState.WaitingForInput);
    }
    
    private IEnumerator ButtonCooldown(int buttonIndex)
    {
        cooldowns[buttonIndex] = true;
        yield return new WaitForSeconds(buttonCooldown);
        cooldowns[buttonIndex] = false;
    }
    
    private void TransitionToState(PuzzleState newState)
    {
        if (showDebugInfo)
        {
            Debug.Log($"Puzzle state transition: {currentState} -> {newState}");
        }
        
        switch (currentState)
        {
            case PuzzleState.WaitingForInput:
                break;
                
            case PuzzleState.Processing:
                break;
                
            case PuzzleState.Evaluating:
                break;
                
            case PuzzleState.Solved:
                break;
                
            case PuzzleState.Failed:
                break;
                
            case PuzzleState.Resetting:
                break;
        }
        
        currentState = newState;
        
        switch (newState)
        {
            case PuzzleState.WaitingForInput:
                break;
                
            case PuzzleState.Processing:
                break;
                
            case PuzzleState.Evaluating:
                break;
                
            case PuzzleState.Solved:
                break;
                
            case PuzzleState.Failed:
                break;
                
            case PuzzleState.Resetting:
                break;
        }
    }
    
    public void ManualReset()
    {
        if (currentState != PuzzleState.Resetting && currentState != PuzzleState.Solved)
        {
            TransitionToState(PuzzleState.Resetting);
            StartCoroutine(ResetPuzzle());
        }
    }
    
    public float GetButtonProgress(int buttonIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= buttons.Length)
            return 0f;
            
        return (float)buttons[buttonIndex].currentPresses / buttons[buttonIndex].requiredPresses;
    }
    
    public ButtonConfig GetButtonConfig(int buttonIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= buttons.Length)
            return null;
            
        return buttons[buttonIndex];
    }
    
    
}