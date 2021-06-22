using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackRate = 1f;
    public float damage = 2f;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject core;
    float timer;

    public Transform target;
    void Start()
    {
        if (gameObject.GetComponent<EnemyMovement>().player)
        {
            player = gameObject.GetComponent<EnemyMovement>().player.gameObject;
        }
        core = gameObject.GetComponent<EnemyMovement>().core.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (target)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                if (timer > attackRate)
                {
                    player.GetComponent<PlayerHealth>().TakeDamage(damage);
                    timer = 0;
                }
            }
        }
        else
        {

            if (Vector3.Distance(transform.position, core.transform.position) < attackRange || Vector3.Distance(transform.position, core.transform.position) < 6)
            {
                if (timer > attackRate)
                {
                    core.GetComponent<CoreHealth>().TakeDamage(damage);
                    timer = 0;
                }
            }
        }
    }
}
