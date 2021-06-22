using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //player and core transforms
    public Transform core;
    public Transform player;
    //how far can you hit
    public float attackRange = 10f;
    //ai
    private NavMeshAgent nav;

    void Start()
    {
        //finding nemo and friends
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
        //check if the player tag is set to playe
        if (player.tag == "Player")
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, player.position - transform.position);
            if (Physics.Raycast(transform.position, player.position - transform.position, out hit, attackRange))
            {
                if (hit.transform.tag == "Player")
                {
                    //move to player

                    gameObject.GetComponent<EnemyCombat>().target = player;
                    nav.SetDestination(player.position);
                }
                else
                {
                    //move to core
                    gameObject.GetComponent<EnemyCombat>().target = null;

                    nav.SetDestination(core.position);
                }
            }
            else
            {
                //move to corey
                gameObject.GetComponent<EnemyCombat>().target = null;

                nav.SetDestination(core.position);
            }
        }
        else
        {
            //move core
            gameObject.GetComponent<EnemyCombat>().target = null;

            nav.SetDestination(core.position);
        }
    }
}
