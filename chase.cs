using System.Collections;
using UnityEngine;

public class chase : MonoBehaviour
{
    public Transform player;
    public static Animator anim;

    public int LookDistance = 30;
    public int FollowDistance = 25;
    public int AttackDistance = 5;
    public float rotateSpeed = 0.1f;
    public float Speed = 0.1f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Vector3.Distance(player.position, this.transform.position) < LookDistance)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(direction), rotateSpeed);

            anim.SetBool("isIdle", false);
            if(direction.magnitude > FollowDistance)
            {
                this.transform.Translate(0, 0, Speed);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
            else if(direction.magnitude < AttackDistance)
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
}
