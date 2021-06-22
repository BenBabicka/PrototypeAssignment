using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{

    //attacking variables
    public float attackRange = 2f;
    public float attackRate = 1f;
    public float damage = 2f;
    //sneaky snakes
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public GameObject core;
    //brrr timer
    float timer;
    //target spotted
    public Transform target;
    void Start()
    {//finding nemo
        if (gameObject.GetComponent<EnemyMovement>().player)
        {
            player = gameObject.GetComponent<EnemyMovement>().player.gameObject;
        }
        core = gameObject.GetComponent<EnemyMovement>().core.gameObject;
    }

    // Update is called once per frame
    void Update()
    {//brrrrr
        timer += Time.deltaTime;
        if (target)
        {//if the target is not null (AKA IS PLAYER) do this, MAKE SURE IN RANGE
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {//brrr biger then attackrate
                if (timer > attackRate)
                {//ouch mode
                    player.GetComponent<PlayerHealth>().TakeDamage(damage);
                    timer = 0;
                }
            }
        }
        else
        {
            //if no target / player check distance to core thing
            if (Vector3.Distance(transform.position, core.transform.position) < attackRange || Vector3.Distance(transform.position, core.transform.position) < 6)
            {   //brrr big good
                if (timer > attackRate)
                {   //not ouch to you but ouch to the core
                    core.GetComponent<CoreHealth>().TakeDamage(damage);
                    timer = 0;
                }
            }
        }
    }
}
