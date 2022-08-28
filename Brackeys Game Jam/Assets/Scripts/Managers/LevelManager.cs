using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform C1StartPoint, C2StartPoint;
    public int level;
    public GameObject Vcam;

    public bool completed = false;
    public int charactersOut = 0;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        charactersOut = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreUnloadLevel()
    {
        Vcam.SetActive(false);
    }

    public void LoadLevel()
    {
        PlayerCharacter c1 = Player.Instance.character1;
        PlayerCharacter c2 = Player.Instance.character2;
        c1.gameObject.SetActive(true);
        c2.gameObject.SetActive(true);
        c1.PlayerMovement.Teleport(C1StartPoint.position);
        c2.PlayerMovement.Teleport(C2StartPoint.position);
        ActivateCamera();
        Player.Instance.SetToCharacter1();


    }

    public void ActivateCamera()
    {
        Vcam.SetActive (true);
    }

    public void LevelComplete()
    {
        if (!completed)
        {
            completed = true;
            
            //yield return new WaitForSecondsRealtime(0.2f);
            GameManager.instance.CompletedArea();
        }

    }

    public void CharacterOut(GameObject character)
    {
        charactersOut++;
        character.SetActive(false);
        if (charactersOut >= 2)
        {
            player.canSwitch = true;
            LevelComplete();
        }
        else
        {
            player.Switch();
            player.canSwitch = false;
        }
    }
}
