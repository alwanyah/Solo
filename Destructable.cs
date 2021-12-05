using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject destroyedVersion;
    public ParticleSystem priceEffect;

    public void OnMouseDown()
    {
        GameObject impactGO = Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(impactGO, 2f);
        priceEffect.Play();
    }
}
