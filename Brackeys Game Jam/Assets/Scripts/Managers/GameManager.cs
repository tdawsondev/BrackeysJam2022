using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Awake

    public static GameManager instance;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one GameManager Object");
        }
        instance = this;
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {

        inputActions.Player.RestartLevel.performed += RestartLevelInput;
        inputActions.Player.RestartScene.performed += RestartScene;
        inputActions.Player.Pause.performed += PauseGame;
        inputActions.Player.LoadLevel.performed += LoadTestLevel;

        inputActions.Player.Pause.Enable();
        inputActions.Player.RestartLevel.Enable();
        inputActions.Player.RestartScene.Enable();
        inputActions.Player.LoadLevel.Enable();

    }
    private void OnDisable()
    {
        inputActions.Player.RestartScene.performed -= RestartScene;
        inputActions.Player.RestartLevel.performed -= RestartLevelInput;
        inputActions.Player.Pause.performed -= PauseGame;
        inputActions.Player.LoadLevel.performed -= LoadTestLevel;

        inputActions.Disable();
    }

    #endregion

    public List<LevelManager> Levels = new List<LevelManager>();
    public int currentLevel = 0;

    private bool pasued = false;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;
    public TextMeshProUGUI gameOverMessage;
    public int levelLoadTest = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        LoadLevel(currentLevel);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        victoryMenu.SetActive(false);
        AudioManager.instance.Stop("MainTheme");
        if(!AudioManager.instance.SongIsPlaying("LevelTheme"))
            AudioManager.instance.Play("LevelTheme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompletedArea()
    {
        NextLevel();
    }
    public LevelManager GetCurrentLevel()
    {
        return Levels[currentLevel];
    }
    public void RestartLevelInput(InputAction.CallbackContext context)
    {
        SceneHandler.instance.RestartLevel();
    }
    public void RestartLevel()
    {
        SceneHandler.instance.RestartLevel();
    }
    public void RestartScene(InputAction.CallbackContext context)
    {
        RestartScene();
    }
    public void RestartScene()
    {
        SceneHandler.instance.RestartScene();
    }
    public void PauseGame(InputAction.CallbackContext context)
    {
        PauseGame();
    }
    public void LoadTestLevel(InputAction.CallbackContext context)
    {
        Levels[currentLevel].PreUnloadLevel();
        currentLevel = levelLoadTest;
        LoadLevel(currentLevel);
    }
    public void PauseGame()
    {
        if (!pasued)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            pasued = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            pasued = false;
        }
    }

    public void LoadLevel(int index)
    {
        currentLevel = index;
        Levels[index].LoadLevel();
        HUDController.instance.SetLevelText();

    }
    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel >= Levels.Count)
        {
            victoryMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Levels[currentLevel - 1].PreUnloadLevel();
            LoadLevel(currentLevel);
        }
    }

    public void GameOver(string message)
    {
        gameOverMenu.SetActive(true);
        gameOverMessage.text = message;
        Time.timeScale = 0f;
    }
}
