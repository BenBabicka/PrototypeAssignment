using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreHealth : MonoBehaviour
{
    public float coreHealth = 500;
    public Slider coreHealthBar;
    public Slider coreHealthBarPlayerUI;

    private void Start()
    {
        coreHealthBar.maxValue = coreHealth;
        coreHealthBarPlayerUI.maxValue = coreHealth;
    }

    private void Update()
    {
        if (coreHealth <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        coreHealthBar.value = coreHealth;
        coreHealthBarPlayerUI.value = coreHealth;

        if (Camera.main)
        {
            coreHealthBar.transform.parent.transform.LookAt(Camera.main.transform);
        }
    }
    public void TakeDamage(float damage)
    {
        coreHealth = coreHealth - damage;
    }
}
