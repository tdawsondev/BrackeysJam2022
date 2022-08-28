using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePuzzle : MonoBehaviour
{
    [SerializeField] UnityEvent onComplete = null;

    public virtual void OnComplete()
    {
        if (onComplete != null)
        {
            onComplete.Invoke();
        }

    }

}
