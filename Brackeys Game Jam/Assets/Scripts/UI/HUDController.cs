using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    #region Awake

    public static HUDController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one HUDController Object");
        }
        instance = this;
    }

    #endregion

    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] TextMeshProUGUI levelText;


    // Start is called before the first frame update
    void Start()
    {
        HideInteractionMessage();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HideInteractionMessage()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void ShowInteractionMessage(Interactable i)
    {
        interactionText.text = i.message;
        interactionText.gameObject.SetActive(true);
    }

    public void SetLevelText()
    {
        levelText.text = "Level " + (GameManager.instance.currentLevel + 1);
    }


}
