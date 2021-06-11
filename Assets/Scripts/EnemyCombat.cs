using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackRate = 1f;
    public float damage = 2f;
    [HideInInspector]
    public GameObject player;
    float timer;
    void Start()
    {
        player = gameObject.GetComponent<EnemyMovement>().player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            if (timer > attackRate)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
                timer = 0;
            }
        }
    }
}
