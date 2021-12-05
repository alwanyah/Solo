using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 150;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public void TakeDamage(float amout)
    {
        health -= amout;

        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        player.transform.position = respawnPoint.transform.position;
    }
}
