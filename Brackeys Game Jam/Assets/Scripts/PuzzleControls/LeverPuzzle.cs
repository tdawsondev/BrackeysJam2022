using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverPuzzle : BasePuzzle
{
    public bool on = false;
    private Animator animator;
    [SerializeField] UnityEvent onLeverOff = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (on)
        {
            OnComplete();
        }
    }

    public override void OnComplete()
    {
        animator.SetTrigger("Toggle");
        base.OnComplete();
    }

    public void OnLeverOff()
    {
        animator.SetTrigger("Toggle");        
        onLeverOff.Invoke();
    }

    public void ToggleLever()
    {
        if (on)
        {
            on = false;
            AudioManager.instance.Play("Lever_Off");
            OnLeverOff();
        }
        else
        {
            on = true;
            AudioManager.instance.Play("Lever_On");
            OnComplete();
        }
    }
}
