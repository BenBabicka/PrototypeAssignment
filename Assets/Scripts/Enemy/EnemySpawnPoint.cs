using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    //type of enemys
    public List<GameObject> enemys;
    public GameObject Boss;

    //the amount
    public float amount;
    
    //how many have spanwed
    [HideInInspector]
    public float spawned;

    //spawn rate and brrr time
    public float spawnRate;
    float timer;
    //spawn area
    private BoxCollider spawnBounds;
    //boss round
    public bool bossRound;
    public bool bossSpawner;
    public bool bossedSpawned;
    void Start()
    {
        //ahhhhh
        spawnBounds = transform.GetComponent<BoxCollider>();
    }

    void Update()
    {
        //no boss
        if(!bossRound)
        { 
        timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                //spawn many at random spot in box
                if (spawned < amount)
                {
                    float xPos = Random.Range((spawnBounds.size.x * -0.5f), (spawnBounds.size.x * 0.5f)) + spawnBounds.gameObject.transform.position.x;
                    float zPos = Random.Range((spawnBounds.size.z * -0.5f), (spawnBounds.size.z * 0.5f)) + spawnBounds.gameObject.transform.position.z;
                    //spawning
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
                {//boss spawn
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
