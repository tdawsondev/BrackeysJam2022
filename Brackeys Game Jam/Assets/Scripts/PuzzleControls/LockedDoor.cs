using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] List<DoorLock> locks = new List<DoorLock>();
    private Animator animator;
    public bool open = false;

    public void CheckLocks()
    {
        bool shouldOpen = true;
        foreach (DoorLock l in locks)
        {
            if (l.isLocked)
            {
                shouldOpen = false;
            }
        }
        if (!open && shouldOpen)
        {
            Open();
        }
        if (open && !shouldOpen)
        {
            Close();
        }
    }

    public void Open()
    {
        animator.SetBool("Open", true);
        open = true;
    }
    public void Close()
    {
        animator.SetBool("Open", false);
        open = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (open)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
