using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{

    public List<GameObject> enemys;
    public GameObject Boss;


    public float amount;
    
    [HideInInspector]
    public float spawned;

    public float spawnRate;
    float timer;

    private BoxCollider spawnBounds;

    public bool bossRound;
    public bool bossSpawner;
    public bool bossedSpawned;
    void Start()
    {
        spawnBounds = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!bossRound)
        { 
        timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                if (spawned < amount)
                {
                    float xPos = Random.Range((spawnBounds.size.x * -0.5f), (spawnBounds.size.x * 0.5f)) + spawnBounds.gameObject.transform.position.x;
                    float zPos = Random.Range((spawnBounds.size.z * -0.5f), (spawnBounds.size.z * 0.5f)) + spawnBounds.gameObject.transform.position.z;

                    Vector3 spawnPos = new Vector3(xPos, transform.position.y, zPos);

                    GameObject go = Instantiate(enemys[Random.Range(0, enemys.Count)], spawnPos, Quaternion.identity) as GameObject;
                    FindObjectOfType<GameManager>().enemies.Add(go);
                    spawned++;
                    timer = 0;
                }
            }
        }
        else
        {
            if (bossSpawner)
            {
                if (!bossedSpawned)
                {
                    float xPos = Random.Range((spawnBounds.size.x * -0.5f), (spawnBounds.size.x * 0.5f)) + spawnBounds.gameObject.transform.position.x;
                    float zPos = Random.Range((spawnBounds.size.z * -0.5f), (spawnBounds.size.z * 0.5f)) + spawnBounds.gameObject.transform.position.z;

                    Vector3 spawnPos = new Vector3(xPos, transform.position.y, zPos);

                    GameObject go = Instantiate(Boss, spawnPos, Quaternion.identity) as GameObject;
                    FindObjectOfType<GameManager>().enemies.Add(go);
                    bossedSpawned = true;
                }
            }
        }
    }
}
