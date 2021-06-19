using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100f;

    public float timeSinceLastDamage = 2f;
    public float healRate = 20f;
    public bool dead;

    float lowHealth;
    float timer;
    bool heal;

    public Slider healthSlider;
    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;

    }

    void Update()
    {
        healthSlider.value = health;
        if (!dead)
        {
            if (lowHealth == health)
            {
                timer += Time.deltaTime;

            }
            else
            {
                timer = 0;
            }

            if (timer >= timeSinceLastDamage)
            {
                heal = true;
            }
            if (heal)
            {
                RegenerateHealth();
            }
        }
    }

    void RegenerateHealth()
    {
        health += healRate * Time.deltaTime;
        if(health >= maxHealth)
        {
            health = maxHealth;
            heal = false;
        }
    }

    public void TakeDamage(float damage)
    {
        heal = false;

        health = health - damage;
        lowHealth = health;
        timer = 0;
        if (health <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
            dead = true;
        }
    }

}
