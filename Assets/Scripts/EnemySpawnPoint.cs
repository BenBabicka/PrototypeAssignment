using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public GameObject enemy;

    public float amount;
    
    [HideInInspector]
    public float spawned;

    public float spawnRate;
    float timer;

    private BoxCollider spawnBounds;

    void Start()
    {
        spawnBounds = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnRate)
        {
            if(spawned < amount)
            {
                float xPos = Random.Range((spawnBounds.size.x * -0.5f), (spawnBounds.size.x * 0.5f)) + spawnBounds.gameObject.transform.position.x;
                float zPos = Random.Range((spawnBounds.size.z * -0.5f), (spawnBounds.size.z * 0.5f)) + spawnBounds.gameObject.transform.position.z;

                Vector3 spawnPos = new Vector3(xPos, transform.position.y, zPos);

                GameObject go = Instantiate(enemy, spawnPos, Quaternion.identity) as GameObject;
                FindObjectOfType<GameManager>().enemies.Add(go);
                spawned++;
                timer = 0;
            }
        }
    }
}
