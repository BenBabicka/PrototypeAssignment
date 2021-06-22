using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //healthy stats
    public float health;
    public float maxHealth = 100f;

    //regen and dead stats
    public float timeSinceLastDamage = 2f;
    public float healRate = 20f;
    public bool dead;
    //helping the regen acctually work
    float lowHealth;
    float timer;
    bool heal;
    //slidey
    public Slider healthSlider;
    void Start()
    {//set stats
        health = maxHealth;
        healthSlider.maxValue = maxHealth;

    }

    void Update()
    {
        healthSlider.value = health;
        //check if already dead
        if (!dead)
        {
            //if hurty health is same as normal healthy timer go brrr
            if (lowHealth == health)
            {
                timer += Time.deltaTime;

            }
            else //OR ELSE
            {
                timer = 0;
            }
            //if brrr is more then ouchy time
            if (timer >= timeSinceLastDamage)
            {
                //i need some milk
                heal = true;
            }
            if (heal)
            {
                //getting some milk
                RegenerateHealth();
            }
        }
    }

    //heal the player
    void RegenerateHealth()
    {
        //omg i feel so good
        health += healRate * Time.deltaTime;
        //can i have more? (no)
        if(health >= maxHealth)
        {
            health = maxHealth;
            heal = false;
        }
    }

    //player get hurty
    public void TakeDamage(float damage)
    {
        //no heal for you
        heal = false;
        //bye health
        health = health - damage;
        //set the low health
        lowHealth = health;
        //brrr timer 0
        timer = 0;
        //you dead?
        if (health <= 0)
        {

            FindObjectOfType<GameManager>().GameOver();
            dead = true;
        }
    }

}
