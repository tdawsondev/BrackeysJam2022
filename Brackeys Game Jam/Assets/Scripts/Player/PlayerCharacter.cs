using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerMovement PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        PlayerMovement.enabled = true;
    }
    public void DeActivate()
    {
        PlayerMovement.enabled = false;
    }
}
