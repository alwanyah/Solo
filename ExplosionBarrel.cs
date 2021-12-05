using UnityEngine;

public class ExpolsionBarrel : MonoBehaviour
{
    public ParticleSystem explosion;
    public float radius = 5f;
    public float force = 700f;


    void OnMouseDown()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        Destroy(gameObject);

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Target tar = nearbyObject.GetComponent<Target>();
            if (tar != null)
            {
                tar.Die();
            }

            Destructable dest = nearbyObject.GetComponent<Destructable>();
            if (dest != null)
            {
                dest.OnMouseDown();
            }

            /*PlayerHealth life = nearbyObject.GetComponent<PlayerHealth>();
            if (life != null)
            {
                life.Die();
            }*/
        }   
    }
}
