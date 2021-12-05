using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float maxDamage = 120f;
    public float minDamage = 45;
    public float range = 30f;

    //:)
    public float timeBtwShot = 1f;
    public float startTimeBtwShots = 2f;
    public GameObject bulletPref;
    public Transform firePoint;
    public GameObject projectile;

    private Transform player;
    private PlayerHealth playerHealth;
    private float scaledDamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();

        scaledDamage = maxDamage - minDamage;

        //:)
        timeBtwShot = startTimeBtwShots;
    }

    void Update()
    {

        //:)
        if(timeBtwShot <= 0)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPref, firePoint.position, firePoint.rotation);
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShot = startTimeBtwShots;
            Shoot();

        } else
        {
            timeBtwShot -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Debug.Log("HE SHOT ME");

        float fractionalDistance = (range - Vector3.Distance(transform.position, player.position)) / range;
        float damage = scaledDamage * fractionalDistance + minDamage;
        playerHealth.TakeDamage(damage);
    }
}
