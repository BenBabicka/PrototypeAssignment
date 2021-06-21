using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform core;
    public Transform player;

    public float attackRange = 10f;

    private NavMeshAgent nav;

    void Start()
    {
        core = GameObject.Find("Core").transform;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.tag == "Player")
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, player.position - transform.position);
            if (Physics.Raycast(transform.position, player.position - transform.position, out hit, attackRange))
            {
                if (hit.transform.tag == "Player")
                {

                    gameObject.GetComponent<EnemyCombat>().target = player;
                    nav.SetDestination(player.position);
                }
                else
                {
                    gameObject.GetComponent<EnemyCombat>().target = null;

                    nav.SetDestination(core.position);
                }
            }
            else
            {
                gameObject.GetComponent<EnemyCombat>().target = null;

                nav.SetDestination(core.position);
            }
        }
        else
        {
            gameObject.GetComponent<EnemyCombat>().target = null;

            nav.SetDestination(core.position);
        }
    }
}
