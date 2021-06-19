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
            if (Vector3.Distance(transform.position, player.position) < attackRange)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.SetDestination(core.position);
            }
        }
        else
        {
            nav.SetDestination(core.position);
        }
    }
}
