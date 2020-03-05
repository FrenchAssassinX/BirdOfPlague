using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator playerAnimator;     // Animator of the player

    public Transform attackPoint;       // Specific transform for the attack
    public float attackRange;           // Radius of the attack
    public LayerMask attackLayer;       // Layer of the attack

    public bool bIsBitting;            // Boolean to detect where player is bitting

    private float bittingDamage;        // Quantity of damages caused by a bite of the player     

    public GameObject target;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();                      // Get animator of the player

        attackPoint = transform.GetChild(0).gameObject.transform;       // Get transform for attack point
        attackRange = 0.2f;                                             // Set attack radius

        bIsBitting = false;                                             // By default set boolean to false

        bittingDamage = 1f;
    }

    void Update()
    {
        /* Input for bitting villagers */
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bIsBitting = true;      // Pass boolean to true
            Attack();               // Attack 
        }
        else
        {
            bIsBitting = false;     // Pass boolean to false
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayer);     // Get all colliders hitting by the circle of attack point

        /* Foreach function to handling every collisions with Attack circle */
        foreach (Collider2D collider in hits)
        {
            /* If game object attached to the collider is a villager then proceed */
            if (collider.gameObject.tag.Equals("Villager"))
            {
                target = collider.gameObject;

                collider.gameObject.GetComponent<SpriteRenderer>().color = Color.clear;

                collider.gameObject.GetComponent<MachineStateVillager>().lifePoints -= bittingDamage;

                Invoke("ResetColor", 0.1f);
            }
        }
    }

    private void ResetColor()
    {
        target.GetComponent<SpriteRenderer>().color = Color.white;
    }

    /* Function to display Attack circle */
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
