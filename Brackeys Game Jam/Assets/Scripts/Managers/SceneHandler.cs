using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    #region Singleton

    public static SceneHandler instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one SceneManager Object");
        }
        instance = this;
    }
    #endregion

    public Animator transtition;
    /// <summary>
    /// Loads Scene at level postion
    /// </summary>
    /// <param name="buildIndex">Index of scene to load</param>
    /// <param name="level">Index of level to start on</param>
    public void LoadLevel(int buildIndex, int level = 0)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        StartCoroutine(LoadLevel_Routine(buildIndex));
    }
    public void LoadScene(int buildIndex)
    {
        LoadLevel(buildIndex, 0);
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex, GameManager.instance.currentLevel);
    }
    public void RestartScene()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadLevel_Routine(int buildIndex)
    {
        transtition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1.2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
