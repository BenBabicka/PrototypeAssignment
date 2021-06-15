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

    void Start()
    {
        pauseMenu.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
        NewWave();
    }

    void NewWave()
    {
        if (!startNewWave)
        {
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
        if(fadeColour.a >= 1)
        {
            timer += Time.deltaTime;
        }
        if(timer >= stayUpTime)
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
        if (buttonIndex < 0)
        {
            buttonIndex = 1;
        }
        if (buttonIndex > 1)
        {
            buttonIndex = 0;
        }
        Gamepad gp = InputSystem.GetDevice<Gamepad>();


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

        if(pauseMenu.activeSelf == true)
        {
            if (kb != null)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            playerController.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            if (kb != null)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            playerController.enabled = true;
      
            buttonIndex = 0;
            Time.timeScale = 1;
        }
        #endregion

        if(enemies.Count <= 0)
        {
            NewWave();
        }
        else if(enemies.Count > 0)
        {
            startNewWave = false;
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
        SceneManager.LoadScene(0);
    }


    public void ResetPlayerPosition()
    {
        player.transform.position = playerResetPosition.position;
    }

}
