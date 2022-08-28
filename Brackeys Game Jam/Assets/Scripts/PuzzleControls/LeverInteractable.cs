using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractable : Interactable
{
    private LeverPuzzle puzzle;

    public override void Interact()
    {
        puzzle.ToggleLever();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        puzzle = GetComponent<LeverPuzzle>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
