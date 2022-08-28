using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : BasePuzzle
{
    public override void OnComplete()
    {
        AudioManager.instance.Play("Button_Press");
        base.OnComplete();
    }
}
