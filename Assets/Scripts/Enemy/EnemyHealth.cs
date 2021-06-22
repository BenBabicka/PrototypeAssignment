using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //healthy stats
    public float health;
    public Slider healthBar;

    //goldy stats
    [Header("Gold")]
    public int goldDropAmount;
    public int minGoldAmount = 0;
    public int maxGoldAmount = 20;
    //xpy stats
    [Header("XP")]
    public int xp;
    public int minXpAmount = 5;
    public int maxXpAmount = 40;
    //just player
    Transform player;

    // void where the enemy will take damage when called
    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    private void Start()
    {
        //set the stats
        goldDropAmount = Random.Range(minGoldAmount, maxGoldAmount);
        xp = Random.Range(minXpAmount, maxXpAmount);
        healthBar.maxValue = health;
        //finding nemo
        player = gameObject.GetComponent<EnemyMovement>().player;
    }

    private void Update()
    {
        //look at the player
        healthBar.transform.LookAt(player.GetComponentInChildren<Camera>().transform);
        healthBar.value = health;
        //if enemy is dead, die
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //they where so young
        FindObjectOfType<GameManager>().enemies.Remove(gameObject);
        //ooooo gold
        FindObjectOfType<GameManager>().goldAmount += goldDropAmount;
        //omg xp too
        player.GetComponent<LevelManager>().xp += xp;
        //bye bye 
        Destroy(gameObject);
    }
}
