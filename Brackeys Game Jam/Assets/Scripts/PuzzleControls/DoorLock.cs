using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public LockedDoor door;
    public bool isLocked = true;
    public Material lockedColor, unlockedColor;

    private void Start()
    {
        if (isLocked)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    public void Unlock()
    {
        isLocked = false;
        gameObject.GetComponent<Renderer>().material = unlockedColor;
        door.CheckLocks();
    }

    public void Lock()
    {
        isLocked = true;
        gameObject.GetComponent<Renderer>().material = lockedColor;
        door.CheckLocks();
    }
}
