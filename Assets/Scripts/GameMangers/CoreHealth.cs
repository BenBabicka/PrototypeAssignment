using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreHealth : MonoBehaviour
{
    //core health and visuals
    public float coreHealth = 500;
    public Slider coreHealthBar;
    public Slider coreHealthBarPlayerUI;

    private void Start()
    {
        //set stats
        coreHealthBar.maxValue = coreHealth;
        coreHealthBarPlayerUI.maxValue = coreHealth;
    }

    private void Update()
    {
     // check if core dead
        if (coreHealth <= 0)
        {
            //find nemo and game over
            FindObjectOfType<GameManager>().GameOver();
        }
        coreHealthBar.value = coreHealth;
        coreHealthBarPlayerUI.value = coreHealth;
        //look at player
        if (Camera.main)
        {
            coreHealthBar.transform.parent.transform.LookAt(Camera.main.transform);
        }
    }
    //take that core
    public void TakeDamage(float damage)
    {
        coreHealth = coreHealth - damage;
    }
}
