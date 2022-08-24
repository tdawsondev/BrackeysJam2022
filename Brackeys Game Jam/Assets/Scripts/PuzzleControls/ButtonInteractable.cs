using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable
{
    private ButtonPuzzle puzzle;

    public override void Interact()
    {
        puzzle.OnComplete();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        puzzle = GetComponent<ButtonPuzzle>();
    }

}
