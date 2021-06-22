using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //X axis
    public GameObject barrel;
    //Y axis
    public GameObject turret;

    //target baddies
    public  Transform target;
    public GameObject[] enemies;

    //power ranger
    public float rangeDistance;
    //smoothing 
    public float damping = 2f;


    //prefab and fire spot
    public GameObject projectile;
    public Transform firePoint;
    //bullet life. (Im sorry sir, looks like you have only 5 seconds to live... never mind 4.. you get the picture)
    public float range;
    //hiding things.
    float attackRate;
    float damage;
    float time;
    private void Start()
    {//set the values 
        attackRate = gameObject.GetComponentInParent<Building>().attackRate;
        damage = gameObject.GetComponentInParent<Building>().damage;
        rangeDistance = gameObject.GetComponentInParent<Building>().range;
    }
    //find the cloest baddie
    void ClosestEnemy()
    {
        //start with the starting values
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currectPos = transform.position;
        //do a list
        foreach(GameObject enemy in enemies)
        {//do math of each enemy to the player
            float distance = Vector3.Distance(enemy.transform.position, currectPos);
            
            //smallest one WINS
            if(distance < minDistance)
            {
                closestEnemy = enemy.transform;
                target = closestEnemy;
                minDistance = distance;
            }
        }
    }


    void Update()
    {
        //FIND EVERY ENEMY ALL THE TIME
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //NOW FIND THE CLOSEST ONE
        ClosestEnemy();
        //VALUE STUFF
        attackRate = gameObject.GetComponentInParent<Building>().attackRate;
        damage = gameObject.GetComponentInParent<Building>().damage;
        rangeDistance = gameObject.GetComponentInParent<Building>().range;
        if (target)
        {
            //back from soccer just to let you know 6-6 tie did ok
            if (Vector3.Distance(transform.position, target.position) < rangeDistance)
            {
                //check if in range of baddie
               
                    if (turret)
                    {//look at me. IM THE CAPTAIN NOW. (Look at the enemy)
                        var lookPos = target.position - turret.transform.position;
                        lookPos.y = 0;
                        var rotation = Quaternion.LookRotation(lookPos);
                        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, Time.deltaTime * damping);
                    }
                    //shooting
                //check if the turret can see the enemy
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.transform.forward, out hit, 300f))
                {//it can?
                    if (hit.transform.tag == "Enemy")
                    {
                        //brrr go up
                        time += Time.deltaTime;
                        if (time > attackRate)
                        {
                            //pew pew
                            GameObject Go = Instantiate(projectile, firePoint.position, firePoint.rotation) as GameObject;
                            Go.GetComponent<Projectile>().timeTillDeath = range;
                            Go.GetComponent<Projectile>().damage = damage;
                            time = 0;


                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //visual lazer
        Gizmos.DrawRay(firePoint.position, firePoint.transform.forward);
    }
}
