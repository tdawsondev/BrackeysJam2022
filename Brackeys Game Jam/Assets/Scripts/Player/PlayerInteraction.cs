using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    #region Awake

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Interact.performed += OnInteract;
        inputActions.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Disable();

    }
    #endregion
    private List<Interactable> interactables = new List<Interactable>();
    private Interactable closestInteractable;
    private Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    public void AddInteract(Interactable i)
    {
        interactables.Add(i);
        CheckCloset();
    }
    public void RemoveInteract(Interactable i)
    {
        interactables.Remove(i);
        i.outline.enabled = false;
        // Hud Stuff here -----
        CheckCloset();
    }

    private void CheckCloset()
    {
        Interactable closest = null;
        foreach (Interactable i in interactables)
        {
            i.outline.enabled = false;
            if (closest == null)
            {
                closest = i;
                continue;
            }
            if (Vector3.Distance(player.currentCharacter.transform.position, closest.transform.position) > Vector3.Distance(player.currentCharacter.transform.position, i.transform.position))
            {
                closest = i;
            }
        }
        if (closest != null)
        {
            closest.outline.enabled = true;
            // hud stuff here----
            closestInteractable = closest;
        }
    }

    private void Update()
    {
        if (interactables.Count >= 2) // if in range of 2 interactable objects, constantly check which object is closer
        {
            CheckCloset();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (interactables.Count == 0)
        {
            closestInteractable = null;
        }

        if (closestInteractable != null)
        {
            closestInteractable.Interact();
        }
        CheckCloset();
    }
}
