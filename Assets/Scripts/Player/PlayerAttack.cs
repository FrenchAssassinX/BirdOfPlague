using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnimator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask attackLayer;

    public bool bIsBitting;            // Boolean to detect where player is bitting

    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        attackPoint = transform.GetChild(0).gameObject.transform;
        attackRange = 0.2f;

        bIsBitting = false;
    }

    void Update()
    {
        /* Input for bitting villagers */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bIsBitting = true;
            Attack();
        }
        else
        {
            bIsBitting = false;
        }


        /* Bitting animation */
        if (bIsBitting)
        {
            playerAnimator.SetBool("IsBitting", true);
        }
        else
        {
            playerAnimator.SetBool("IsBitting", false);
        }
        /* End Animator settings */
    }

    private void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);

        foreach (Collider2D collider in hits)
        {
            if (collider.gameObject.tag.Equals("Villager"))
            {
                Debug.Log("We hit a villager !");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
