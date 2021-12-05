using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
     public Transform target;    
     public Transform projectile;
     
     //xtra
     public Transform firePoint;
     public GameObject bulletPref;

    
     public float maximumLookDistance = 30;
     public float maximumAttackDistance  = 10;
     public float minimumDistanceFromPlayer = 2;
 
     public float rotationDamping = 2;
     public float shotInterval = 1;
     private float shotTime = 0;
 
     public void Update()
        {
            var distance = Vector3.Distance(target.position, transform.position);

            if (distance <= maximumLookDistance)
            {
                LookAtTarget();

                //Check distance and time
                if (distance <= maximumAttackDistance && (Time.time - shotTime) > shotInterval)
                {
                    //xtra
                    GameObject bulletGO = (GameObject)Instantiate(bulletPref, firePoint.position, firePoint.rotation);
                    
                    Shoot();
                }
            }
        }


        public void LookAtTarget()
        {
            var dir = target.position - transform.position;
            dir.y = 0;
            var rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }


        public void Shoot()
        {
        //Reset the time when we shoot
            Debug.Log("HE SHOT ME");
            shotTime = Time.time;
            Instantiate(projectile, transform.position + (target.position - transform.position).normalized, Quaternion.LookRotation(target.position - transform.position));
        }
}
