using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTreeEnable : MonoBehaviour
{
    public Transform player;
    public GameObject enableText;
    public GameObject skillTreeMenu;
    public GameObject pauseMenu;
    public List<MonoBehaviour> scriptsToDisable;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        Gamepad gp = InputSystem.GetDevice<Gamepad>();
        if (skillTreeMenu.activeSelf == true)
        {

            FindObjectOfType<GameManager>().canPause = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach (var script in scriptsToDisable)
            {
                script.enabled = false;
            }
            
            if (kb != null)
            {
                if (kb.escapeKey.wasPressedThisFrame)
                {
                    FindObjectOfType<GameManager>().canPause = true;

                    FindObjectOfType<DeleteBuilding>().deleteMode = false;
                    FindObjectOfType<PlayerCombat>().destoryMode = false;
                    skillTreeMenu.SetActive(false);

                }
            }
            if (gp != null)
            {
                if (gp.buttonEast.wasPressedThisFrame)
                {
                    FindObjectOfType<GameManager>().canPause = true;

                    FindObjectOfType<DeleteBuilding>().deleteMode = false;
                    FindObjectOfType<PlayerCombat>().destoryMode = false;
                    skillTreeMenu.SetActive(false);


                }
            }
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            foreach (var script in scriptsToDisable)
            {
                script.enabled = true;
            }
        }
        if(!skillTreeMenu.activeSelf)
            {
            if (Vector3.Distance(player.position, transform.position) < 4)
            {
                if (kb != null)
                {
                    if (kb.fKey.wasPressedThisFrame)
                    {
                        skillTreeMenu.SetActive(!skillTreeMenu.activeSelf);
                    }
                }
                if (gp != null)
                {
                    if (gp.buttonSouth.wasPressedThisFrame)
                    {
                        FindObjectOfType<SelectedButton>().SkillTree();

                        skillTreeMenu.SetActive(true);
                    }
                }
                enableText.SetActive(true);

            }
            else
            {
                enableText.SetActive(false);
            }
        }
    }

     
}
