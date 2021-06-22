using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //bye
    public void Quit()
    {
        Application.Quit();
    }
    //hello
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    
}

