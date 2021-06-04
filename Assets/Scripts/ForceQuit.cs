using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceQuit : MonoBehaviour
{
    public InputMaster controlls;
    bool quit;

    private void Awake()
    {
        controlls = new InputMaster();
        controlls.Player.forceQuit.performed += ctx => quit = true;
        controlls.Player.forceQuit.canceled += ctx => quit = false;
    }
    private void OnEnable()
    {
        controlls.Enable();
    }
    void Update()
    {
        if (quit)
        {
            Application.Quit();
        }
    }
}
