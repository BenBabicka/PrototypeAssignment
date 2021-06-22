using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //brrr stats
    public float movementSpeed;
    public float damage;
    public float timeTillDeath;
    Rigidbody rb;
    private void Start()
    {
       
        rb = gameObject.GetComponent<Rigidbody>();
        //die 
        Destroy(gameObject, timeTillDeath/10);
        //brrrrr
        rb.AddRelativeForce(Vector3.forward * movementSpeed, ForceMode.Impulse);

    }
   
    private void OnCollisionEnter(Collision collision)
    {
        //hit a baddie die and hurt them
        if(collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }





}
