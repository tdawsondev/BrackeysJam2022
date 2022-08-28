using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerInteraction playerInteraction;
    public GameObject selectedObject;

    public Transform PickUpPoint;
    public bool holdingObject = false;
    private PickupInteractable currentHeldObject;
    private bool activated = false;
    private void Start()
    {
        if (!activated)
        {
            DeActivate();
        }
        holdingObject = false;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        PlayerMovement.inputDisabled = false;
        playerInteraction.Enable();
        selectedObject.SetActive(true);
        activated = true;
    }
    public void DeActivate()
    {
        PlayerMovement.inputDisabled = true;
        PlayerMovement.CancelInvoke();
        selectedObject.SetActive(false);
        playerInteraction.Disable();
        activated = false;
        

    }
    public void PickUpObject(PickupInteractable obj)
    {
        holdingObject = true;
        obj.transform.SetParent(PickUpPoint);
        obj.transform.localPosition = Vector3.zero;
        obj.SetDisable();
        Rigidbody rb = obj.transform.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        obj.objectHeld = true;
        currentHeldObject = obj;
        Player.Instance.playerInteraction.RemoveInteract(obj);
    }
    public void DropObject()
    {
        holdingObject = false;
        currentHeldObject.SetEnable();
        currentHeldObject.ResetParentTransform();
        currentHeldObject.objectHeld = false;
        Rigidbody rb = currentHeldObject.transform.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        currentHeldObject = null;

    }

    public void OnDrop()
    {
        if (activated)
        {
            if (holdingObject)
            {
                DropObject();
                Player.Instance.playerInteraction.CheckCloset();
            }
        }
    }
}
