using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatePuzzle : BasePuzzle
{
    //public bool on = false;
    private Animator animator;
    [SerializeField] UnityEvent onPlateUp = null;

    public int objectCount = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        objectCount = 0;
    }

    public override void OnComplete()
    {
        animator.SetTrigger("Toggle");
        AudioManager.instance.Play("PessurePlate_Pressed");
        base.OnComplete();
    }

    public void OnPlateUp()
    {
        animator.SetTrigger("Toggle");
        AudioManager.instance.Play("PessurePlate_Released");
        onPlateUp.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
       // De//bug.Log("Triggered " + other.name);
        if (other.isTrigger)
        {
            return;
        }
        if (other.tag == "Player" || other.tag == "Draggable")
        {
            if (objectCount == 0)
            {
                OnComplete(); // will need some way to detect more than one object on top
            }
            objectCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        if (other.tag == "Player" || other.tag == "Draggable")
        {
            objectCount--;
            if (objectCount == 0)
            {
                OnPlateUp();
            }

        }
    }
}
