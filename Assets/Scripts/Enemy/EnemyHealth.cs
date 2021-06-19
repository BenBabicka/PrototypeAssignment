using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Slider healthBar;

    [Header("Gold")]
    public int goldDropAmount;
    public int minGoldAmount = 0;
    public int maxGoldAmount = 20;
    [Header("XP")]
    public int xp;
    public int minXpAmount = 5;
    public int maxXpAmount = 40;
    Transform player;

    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    private void Start()
    {
        goldDropAmount = Random.Range(minGoldAmount, maxGoldAmount);
        xp = Random.Range(minXpAmount, maxXpAmount);
        healthBar.maxValue = health;
        player = gameObject.GetComponent<EnemyMovement>().player;
    }

    private void Update()
    {
        healthBar.transform.LookAt(player.GetComponentInChildren<Camera>().transform);
        healthBar.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<GameManager>().enemies.Remove(gameObject);
        FindObjectOfType<GameManager>().goldAmount += goldDropAmount;
        player.GetComponent<LevelManager>().xp += xp;
        Destroy(gameObject);
    }
}
