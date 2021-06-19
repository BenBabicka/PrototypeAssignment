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
    [Header ("Waves")]
    [Space]
    [Space]

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

    int buttonIndex;

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
        goldAmount = 1000;
        player.SetActive(true);
        InGameUI.SetActive(true);
        StartUI.SetActive(false);
        deathCamera.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NewWave();
        startedGame = true;

    }

    void NewWave()
    {
        if (!startNewWave)
        {
            if(startedGame)
            {
                goldAmount += 200;
            }
            fade = true;
            waveText.text = "Wave" + "\n" + (wave + 1);
            wave++;
            foreach (EnemySpawnPoint spawners in FindObjectsOfType<EnemySpawnPoint>())
            {
                spawners.spawned = 0;
                spawners.amount += 5;
            }
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

        if (startedGame && !gameOver)
        {
            if (buttonIndex < 0)
            {
                buttonIndex = 1;
            }
            if (buttonIndex > 1)
            {
                buttonIndex = 0;
            }


            if (gp != null)
            {
                if (gp.startButton.wasPressedThisFrame)
                {
                    pauseMenu.SetActive(!pauseMenu.activeSelf);
                }

                if (gp.dpad.down.wasPressedThisFrame)
                {
                    buttonIndex += 1;
                }
                if (gp.dpad.up.wasPressedThisFrame)
                {
                    buttonIndex -= 1;
                }

                if (buttonIndex == 0)
                {
                    resumeText.color = selectedColour;
                    quitText.color = normalColour;
                    if (gp.buttonSouth.wasPressedThisFrame)
                    {

                        Resume();
                    }
                }
                if (buttonIndex == 1)
                {
                    resumeText.color = normalColour;
                    quitText.color = selectedColour;
                    if (gp.buttonSouth.wasPressedThisFrame)
                    {

                        BackToMainMenu();
                    }

                }
            }

            Keyboard kb = InputSystem.GetDevice<Keyboard>();
            if (kb != null)
            {
                if (kb.escapeKey.wasPressedThisFrame)
                {
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
                buttonIndex = 0;
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
        if (!startedGame)
        {
            if (gp != null)
            {
                if (gp.buttonSouth.wasPressedThisFrame)
                {
                    StartGame();
                }
            }
        }

        goldText.text = goldAmount.ToString();
        if (startedGame)
        {
            if (enemies.Count <= 0)
            {
                NewWave();
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
