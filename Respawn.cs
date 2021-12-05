using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform RespawnPoint;

    private void OnCollisionEnter3D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
            col.transform.position = RespawnPoint.position;
    }
}
