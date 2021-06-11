using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        if(gp.IsActuated())
        {
            if(gp.buttonSouth.wasPressedThisFrame)
            {
                LoadScene();
            }
        }
    }
}

