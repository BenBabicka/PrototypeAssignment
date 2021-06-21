using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player;

    public Transform playerResetPosition;

    [Space]
    [Header("Waves")]
    [Space]
    [Space]
    public float waveStartTime = 10f;
    public float fadeTime;
    public float stayUpTime;
    public Text waveText;
    public Color fadeColour;
    public List<GameObject> enemies;
    bool fade;
    bool startNewWave;
    float timer;
    int wave;

    [Space]
    [Header("Pause Menu")]
    [Space]
    [Space]

    public GameObject pauseMenu;

    public PlayerController playerController;


    public Text resumeText;
    public Text quitText;

    public Color selectedColour = Color.gray;
    public Color normalColour = Color.white;

    [Space]
    [Header("Gold")]
    [Space]
    [Space]
    public int goldAmount = 1000;
    public Text goldText;


    [Space]
    [Header("Game States")]
    [Space]
    [Space]
    public GameObject deathCamera;
    public GameObject gameOverUI;
    public GameObject InGameUI;
    public GameObject StartUI;
    bool startedGame;
    bool gameOver;
    bool gameOverOnce;
    bool startedNewWave;
   public bool canPause;
    void Start()
    {
        pauseMenu.SetActive(false);
        StartUI.SetActive(true);
        InGameUI.SetActive(false);
        gameOverUI.SetActive(false);
        deathCamera.SetActive(true);
        playerController = FindObjectOfType<PlayerController>();
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        canPause = true;
        goldAmount = 1000;
        player.SetActive(true);
        InGameUI.SetActive(true);
        StartUI.SetActive(false);
        deathCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       Invoke( "NewWave", waveStartTime);

    }

    void NewWave()
    {

        if (!startNewWave)
        {
          
            startedGame = true;

            fade = true;
            waveText.text = "Wave" + "\n" + (wave + 1);
            wave++;
            foreach (EnemySpawnPoint spawners in FindObjectsOfType<EnemySpawnPoint>())
            {
                spawners.spawned = 0;
                spawners.amount += 5;
            }
            startedNewWave = false;
            startNewWave = true;
        }
    }

    void Update()
    {
        #region Wave text fade
        waveText.color = fadeColour;
        if (fadeColour.a >= 1)
        {
            timer += Time.deltaTime;
        }
        if (timer >= stayUpTime)
        {
            fade = false;
            timer = 0;
        }

        if (fade)
        {
            fadeColour.a += fadeTime * Time.deltaTime;
        }
        else
        {
            timer = 0;

            if (fadeColour.a > 0)
            {
                fadeColour.a -= fadeTime * Time.deltaTime;
            }
            else
            {
                fadeColour.a = 0;
            }
        }
        #endregion
        #region Pause menu
        Gamepad gp = InputSystem.GetDevice<Gamepad>();

        if (canPause && !gameOver)
        {
           if (gp != null)
            {
                if (gp.startButton.wasPressedThisFrame)
                {
                        FindObjectOfType<SelectedButton>().PauseMenu();
                    
                    pauseMenu.SetActive(!pauseMenu.activeSelf);
                }      
            }
          
            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            if (kb != null)
            {
                if (kb.escapeKey.wasPressedThisFrame)
                {
                   
                        FindObjectOfType<SelectedButton>().PauseMenu();
                    
                   pauseMenu.SetActive(!pauseMenu.activeSelf);
                }
            }

            if (pauseMenu.activeSelf == true)
            {
                if (kb != null)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                if (playerController.enabled)
                {
                    playerController.enabled = false;
                }
                Time.timeScale = 0;
            }
            else
            {
                if (kb != null)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                if (playerController)
                {
                    playerController.enabled = true;
                }
                Time.timeScale = 1;
            }
        }
        #endregion
        if (gameOver)
        {
            if (gp != null)
            {
                if (gp.buttonEast.wasPressedThisFrame)
                {
                    BackToMainMenu();
                }
                if (gp.buttonSouth.wasPressedThisFrame)
                {
                    Restart();
                }
            }

        }
       

        goldText.text = goldAmount.ToString();
        if (startedGame)
        {
            if (enemies.Count <= 0)
            {
                if (!startedNewWave)
                {
                    Invoke("NewWave", waveStartTime);
                    goldAmount += 200;
                    startedNewWave = true;
                }
            }

            else if (enemies.Count > 0)
            {
                startNewWave = false;
            }
        }
    }

    public void Resume()
    {
        resumeText.color = normalColour;
        quitText.color = normalColour;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        if (!gameOverOnce)
        {
            FindObjectOfType<SelectedButton>().GameOver();
            gameOverOnce = true;
        }
        gameOver = true;
        deathCamera.SetActive(true);
        InGameUI.SetActive(false);
        player.SetActive(false);
        player.tag = "Dead";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }


    public void ResetPlayerPosition()
    {
        player.transform.position = playerResetPosition.position;
    }

}
