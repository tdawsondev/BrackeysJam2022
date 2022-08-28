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
    public bool interactionsEnabled = false;

    private void Start()
    {
        player = Player.Instance;
        Disable();
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
        if(HUDController.instance)
            HUDController.instance.HideInteractionMessage();
        CheckCloset();
    }

    public void CheckCloset()
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
            if (Vector3.Distance(transform.position, closest.transform.position) > Vector3.Distance(transform.position, i.transform.position))
            {
                closest = i;
            }
        }
        if (interactionsEnabled && closest != null && !Player.Instance.currentCharacter.holdingObject)
        {
            closest.outline.enabled = true;
            if(HUDController.instance)
                HUDController.instance.ShowInteractionMessage(closest);
            
        }
        closestInteractable = closest;
    }

    private void Update()
    {
        if (interactionsEnabled && interactables.Count >= 2) // if in range of 2 interactable objects, constantly check which object is closer
        {
            CheckCloset();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (interactionsEnabled)
        {
            if (!Player.Instance.currentCharacter.holdingObject)
            {
                if (closestInteractable != null)
                {
                    closestInteractable.Interact();
                }
                CheckCloset();
            }
            else
            {
                Player.Instance.currentCharacter.OnDrop();
            }
        }
    }

    public void Disable()
    {
        interactionsEnabled = false;
        if(closestInteractable != null)
        {
            closestInteractable.outline.enabled = false;
            if (HUDController.instance)
                HUDController.instance.HideInteractionMessage();
        }
    }
    public void Enable()
    {
        interactionsEnabled = true;
        if(interactables.Count > 0)
        {
            CheckCloset();
        }
        Player.Instance.playerInteraction = this;
    }
}
