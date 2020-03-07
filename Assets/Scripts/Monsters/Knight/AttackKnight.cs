using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKnight : MonoBehaviour
{
    private Animator knightAnimator;        // Animator of the knight

    public Transform attackPoint;           // Specific transform for the attack
    public float attackRange;               // Radius of the attack
    public LayerMask attackLayer;           // Layer to find collisions with attack

    private float knightDamage;             // Damages caused by an attack of the knight

    public float TIMER_ATTACK_VALUE = 100f;
    public float timerAttack;
    public bool canAttack;

    void Start()
    {
        knightAnimator = gameObject.GetComponent<Animator>();   // Get animator attached to the knight

        attackPoint = transform.GetChild(1).gameObject.transform;   // Get attack point attached to the knight
        attackRange = 0.2f;

        knightDamage = 1f;                                      // Initial damages caused by the knight

        timerAttack = TIMER_ATTACK_VALUE;
        canAttack = true;
    }

    void Update()
    {
        if (!canAttack)
        {
            HandlingTimerAttack();
        }
    }

    public void Attack()
    {
        canAttack = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, attackLayer);

        FindObjectOfType<AudioManager>().Play("Sword");

        foreach (Collider2D collider in hits)
        {
            if (collider.gameObject.name.Equals("Player"))
            {
                collider.gameObject.GetComponent<SpecsPlayer>().playerLifePoints -= knightDamage;
                FindObjectOfType<AudioManager>().Play("PlayerHurt");

                gameObject.GetComponent<MachineStateKnight>().bIsAttacking = false;
                knightAnimator.SetBool("IsAttacking", gameObject.GetComponent<MachineStateKnight>().bIsAttacking);
                gameObject.GetComponent<MachineStateKnight>().currentState = gameObject.GetComponent<MachineStateKnight>().STATE_MACHINE[2];
                break;
            }
        }
    }

    private void HandlingTimerAttack()
    {
        timerAttack--;

        if (timerAttack <= 0)
        {
            canAttack = true;
            timerAttack = TIMER_ATTACK_VALUE;
        }
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
