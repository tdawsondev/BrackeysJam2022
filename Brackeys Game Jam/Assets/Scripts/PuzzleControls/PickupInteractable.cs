using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable
{
    private Transform oldParent = null;

    public bool objectHeld = false;
    public override void Interact()
    {
        if (!objectHeld)
        {
            PlayerCharacter c = Player.Instance.currentCharacter;

            oldParent = transform.parent;
            c.PickUpObject(this);
        }
    }

    public void ResetParentTransform()
    {
        transform.SetParent(oldParent);
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        oldParent = transform.parent;
        objectHeld = false;
    }
}
