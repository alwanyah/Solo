using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform ThePlayer;

    //shot
    public float range = 20f;
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    /*public GameObject bulletPref;
    public Transform firePoint;*/
    
    void Update()
    {
        if (ThePlayer == null)
            return;
        
        float dist = Vector3.Distance(transform.position, ThePlayer.transform.position);
        if(dist < 20)
        {
            StopEnemy();

        } else {
            GoToTarget();
        }

        //rotate to player
        Vector3 dir = ThePlayer.position - transform.position;
        Quaternion lookRotaion = Quaternion.LookRotation(dir);
        Vector3 rotaion = Quaternion.Lerp(partToRotate.rotation, lookRotaion, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotaion.y, 0f);

        /*if(fireCountdown <= 0f)
        {
            //Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;*/
    }

    /*void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPref, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(ThePlayer);
        }
    }*/

    private void GoToTarget()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        gameObject.GetComponent<NavMeshAgent>().SetDestination(ThePlayer.transform.position);
    }

    private void StopEnemy()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }
}
