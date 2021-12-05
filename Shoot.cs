using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 200f;
    public float rückstoß = 30f;
    public float fireRate = 15f;

    //Reloading
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;

    public Camera fpsCam;
    public ParticleSystem SchussEffekt;
    public GameObject hitEffekt;
    private float nextTimeToFire = 0f;
    AudioSource shootingSound;

    void Start()
    {
        shootingSound = GetComponent<AudioSource>();

        //Reloading
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        //Reloading
        if (isReloading)
            return;

        //Reloading
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            shootingSound.Play();

            nextTimeToFire = Time.time + 1f / fireRate;
            Feuer();
        }
    }

    //Reloading
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);


        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Feuer()
    {
        SchussEffekt.Play();

        //Reloading
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * rückstoß);
                }

                GameObject impactGO = Instantiate(hitEffekt, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
            }
        }
    }

}
