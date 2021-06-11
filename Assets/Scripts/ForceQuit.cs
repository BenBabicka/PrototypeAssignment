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
