using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlternativePathway : MonoBehaviour
{
    float timer;
    public float switchTime = 2f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > switchTime)
        {
            gameObject.GetComponent<NavMeshObstacle>().enabled = !gameObject.GetComponent<NavMeshObstacle>().enabled;
            timer = 0;
        }
    }
}
