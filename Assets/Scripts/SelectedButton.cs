using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SelectedButton : MonoBehaviour
{
    public Button skillTreeButton;
    public Button pauseMenuButton;
    public Button GameOverButton;

public void SkillTree()
    {
        skillTreeButton.Select();
    }
    public void PauseMenu()
    {
        pauseMenuButton.Select();
    }

    public void GameOver()
    {
        GameOverButton.Select();
    }
}
