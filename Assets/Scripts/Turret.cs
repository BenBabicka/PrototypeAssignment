using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //X axis
    public GameObject barrel;
    //Y axis
    public GameObject turret;

  public  Transform target;
    public GameObject[] enemies;

    public float rangeDistance;

    public float damping = 2f;



    public GameObject projectile;
    public Transform firePoint;

    public float range;

    float attackRate;
    float damage;
    float time;
    private void Start()
    {
        attackRate = gameObject.GetComponentInParent<Building>().attackRate;
        damage = gameObject.GetComponentInParent<Building>().damage;
    }

    void ClosestEnemy()
    {
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currectPos = transform.position;
        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, currectPos);
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
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        ClosestEnemy();
        if(target)
        {
            if (Vector3.Distance(transform.position, target.position) < rangeDistance)
            {
               
                    if (turret)
                    {
                        var lookPos = target.position - turret.transform.position;
                        lookPos.y = 0;
                        var rotation = Quaternion.LookRotation(lookPos);
                        turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, rotation, Time.deltaTime * damping);
                    }
                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.transform.forward, out hit, 300f))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        time += Time.deltaTime;
                        if (time > attackRate)
                        {

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
        Gizmos.DrawRay(firePoint.position, firePoint.transform.forward);
    }
}
