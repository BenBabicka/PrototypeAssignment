using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<GameManager>().enemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
