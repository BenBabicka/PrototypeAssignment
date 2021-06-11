using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    float attackTime;
    public float damage;

    private void Start()
    {
        attackTime = gameObject.GetComponentInParent<Building>().attackRate;
        damage = gameObject.GetComponentInParent<Building>().damage;
    }

    private void Update()
    {
        attackTime -= Time.deltaTime;
        if (attackTime < 0)
        {
            Collider[] boxs = Physics.OverlapBox(transform.transform.position, gameObject.GetComponent<BoxCollider>().bounds.size / 2);
            foreach (var col in boxs)
            {
                if (col.tag == "Enemy")
                {
                    col.GetComponent<EnemyHealth>().TakeDamage(damage);
                    attackTime = gameObject.GetComponentInParent<Building>().attackRate;
                }

            }
        }
    }


}
