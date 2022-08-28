using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDoor : MonoBehaviour
{
    public int level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (level == GameManager.instance.currentLevel)
            {
                GameManager.instance.GetCurrentLevel().CharacterOut(other.gameObject);
            }
        }
    }
}
