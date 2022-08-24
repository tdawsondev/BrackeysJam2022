using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Singleton/Awake/Input
    public static Player Instance { get; private set; } // singleton variable.
    private PlayerInputActions inputActions; // input
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one player object.");
        }
        Instance = this; // set singleton on awake
        inputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        inputActions.Player.Switch.performed += OnSwitch;
        inputActions.Player.Switch.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Switch.performed -= OnSwitch;
        inputActions.Disable();

    }

    #endregion

    private int charCount = 1; // currentCharacter is 1 or 2.
    public PlayerInteraction playerInteraction;
    public PlayerCharacter currentCharacter = null;
    [SerializeField] PlayerCharacter character1;
    [SerializeField] PlayerCharacter character2;
    private void Start()
    {
        currentCharacter = character1;
        character1.Activate();
    }

    public void OnSwitch(InputAction.CallbackContext context)
    {
        if(charCount == 1)
        {
            charCount = 2;
            character1.DeActivate();
            character2.Activate();
            currentCharacter = character2;
        }
        else
        {
            charCount = 1;
            character2.DeActivate();
            character1.Activate();
            currentCharacter = character1;
        }
    } 
    
}
