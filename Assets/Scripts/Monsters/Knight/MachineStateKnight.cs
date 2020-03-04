using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStateKnight : MonoBehaviour
{
    public GameObject supriseEmote;                     // Game object to display emote

    public Rigidbody2D knightBody2D;                    // RigidBody2D of the knight
    public Animator knightAnimator;                     // Animator of the knight

    public GameObject player;                           // Player Game Object

    public Transform castPoint;                         // Transform to give a view to knight
    public float viewSight = 3.5f;                      // Distance of view of the knight
    private bool bIsAgro = false;
    private bool bIsSearching = false;

    public string[] STATE_MACHINE;                      // State machine for knight
    public string currentState;                         // Value to verify current state of knight

    public float horizontalMove = 0.1f;                 // Float to handling horizontal moves of the sprite
    public float verticalMove = 0.1f;                   // Float to handling vertical moves of the sprite
    public float moveSpeed;                             // Move speed multiply by horizontalMoves or verticalMoves to create velocity 
    public float INITIAL_SPEED = 2f;                    // Standard speed of the knight

    bool bCollision = false;                            // Boolean to detect collision
    private bool bSpriteFacingRight = true;             // Boolean to flip sprite on the good direction

    public bool bIsAttacking = false;                  // Boolean to change animation of knight when he's afraid by the vampire
    private bool bIsWalking = false;                    // 
    private bool bIsIdle = false;                       // 

    void Start()
    {
        supriseEmote = transform.GetChild(2).gameObject;
        supriseEmote.SetActive(false);

        knightBody2D = gameObject.GetComponent<Rigidbody2D>();            // Get RigidBody2D of the knight 
        knightAnimator = gameObject.GetComponent<Animator>();             // Get Animator attached to the knight

        player = GameObject.Find("Player");                                 // Keep player in memory

        castPoint = transform.GetChild(0).gameObject.transform;             // Find castPoint on scene

        STATE_MACHINE = new string[] { "", "walk", "change", "attack" };    // Initialize state machine
        currentState = STATE_MACHINE[0];                                    // By default state of knight is empty

        moveSpeed = INITIAL_SPEED;                                          // Standard move speed of the knight
    }

    void Update()
    {
        /* If knight see the player, start running from him ! */
        if (CanSeePlayer(viewSight))
        {
            bIsAgro = true;
            supriseEmote.SetActive(true);
        }
        else
        {
            supriseEmote.SetActive(false);

            if (bIsAgro)
            {
                if (!bIsSearching)
                {
                    bIsSearching = true;
                    Invoke("StopAgroPlayer", 5);
                }
            }
        }

        if (bIsAgro)
        {
            AgroPlayer();
        }

        /* Flipping sprite */
        if (knightBody2D.velocity.x > 0 && !bSpriteFacingRight)
        {
            FlipSprite();
        }
        else if (knightBody2D.velocity.x < 0 && bSpriteFacingRight)
        {
            FlipSprite();
        }

        /* Handling machine state */
        if (currentState == STATE_MACHINE[0])           // No state by default
        {
            // Change direction
            currentState = STATE_MACHINE[2];
        }
        else if (currentState == STATE_MACHINE[1])      // Walk state
        {
            bIsAttacking = false;                          // Is afraid boolean passed to false for animation
            bIsWalking = true;
            bIsIdle = false;

            moveSpeed = INITIAL_SPEED;

            /* If collision is detected */
            if (bCollision)
            {
                bIsIdle = true;

                /* Stop the knight by setting velocity to 0 */
                Vector2 newVelocity = new Vector2();
                newVelocity.x = 0;
                newVelocity.y = 0;
                knightBody2D.velocity = newVelocity;

                currentState = STATE_MACHINE[2];            // Change state of the knight to change direction
            }
        }
        else if (currentState == STATE_MACHINE[2])      // Change direction state
        {
            bIsAttacking = false;                                                      // Is afraid boolean passed to false for animation
            bIsWalking = true;
            bIsIdle = false;

            System.Random random = new System.Random();                             // Create new random

            /* Generating random X and Y direction for the player */
            float directionX = (gameObject.transform.position.x) - (random.Next(1, 129));
            float directionY = (gameObject.transform.position.y) - (random.Next(1, 129));

            double angle = Math.Atan2(directionX, directionY);                      // Calculating an angle with the X and Y direction

            Vector2 newVelocity = new Vector2();                                    // Vector2 for new velocity
            moveSpeed = INITIAL_SPEED;
            newVelocity.x = horizontalMove * moveSpeed * (float)Math.Cos(angle);    // Affect X velocity
            newVelocity.y = verticalMove * moveSpeed * (float)Math.Sin(angle);      // Affect Y velocity

            knightBody2D.velocity = newVelocity;                                  // Affect new velocity to Body2D

            currentState = STATE_MACHINE[1];                                        // Change state to walk
        }
        else if (currentState == STATE_MACHINE[3])      // Attack state
        {
            if (player != null)
            {
                /* Handling moves of the knight based on the position of the player */
                if (transform.position.x < player.transform.position.x)
                {
                    horizontalMove = 0.1f;
                }
                else if (transform.position.x > player.transform.position.x)
                {
                    horizontalMove = -0.1f;
                }

                if (transform.position.y < player.transform.position.y)
                {
                    verticalMove = 0.1f;
                }
                else if (transform.position.y > player.transform.position.y)
                {
                    verticalMove = -0.1f;
                }
                /* End handling moves */

                Vector2 newVelocity = new Vector2();                // Vector2 for new velocity

                newVelocity.x = horizontalMove * moveSpeed * 3;     // Affect X velocity
                newVelocity.y = verticalMove * moveSpeed * 3;       // Affect Y velocity

                knightBody2D.velocity = newVelocity;                // Affect new velocity to Body2D

                if (Vector2.Distance(player.transform.position, transform.position) < 0.5f &&
                        gameObject.GetComponent<AttackKnight>().canAttack)
                {
                    Vector2 stopVelocity = new Vector2();           // Vector2 for new velocity

                    stopVelocity.x = 0;                             // Affect X velocity
                    stopVelocity.y = 0;                             // Affect Y velocity

                    knightBody2D.velocity = stopVelocity;           // Affect new velocity to Body2D

                    bIsAttacking = true;
                }
            }
        }
        /* End Handling State Machine */

        /* Handling animations */
        if (bIsAttacking)
        {
            knightAnimator.SetBool("IsAttacking", true);
        }
        else
        {
            knightAnimator.SetBool("IsAttacking", false);
        }

        if (bIsWalking)
        {
            knightAnimator.SetBool("IsWalking", true);
        }
        else
        {
            knightAnimator.SetBool("IsWalking", false);
        }

        if (bIsIdle)
        {
            knightAnimator.SetBool("IsIdle", true);
        }
        else
        {
            knightAnimator.SetBool("IsIdle", false);
        }
        /* End Handling animations */
    }

    private bool CanSeePlayer(float pDistance)
    {
        bool bSeePlayer = false;
        float castDistance = pDistance;

        if (!bSpriteFacingRight)
        {
            castDistance = -pDistance;
        }

        Vector2 endPosition = castPoint.position + Vector3.right * castDistance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPosition, 1 << LayerMask.NameToLayer("Default"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name.Equals("Player"))
            {
                bSeePlayer = true;
            }
            else
            {
                bSeePlayer = false;
            }

            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPosition, Color.blue);
        }

        return bSeePlayer;
    }

    /* Function to start running from player */
    private void AgroPlayer()
    {
        currentState = STATE_MACHINE[3];
    }

    /* Function to stop running from player */
    private void StopAgroPlayer()
    {
        bIsAgro = false;
        bIsSearching = false;
        currentState = STATE_MACHINE[2];                                        // Return to change direction state
    }

    /* Function to flip sprite */
    private void FlipSprite()
    {
        bSpriteFacingRight = !bSpriteFacingRight;           // Flip facing

        /* Multiply by -1 the x player scale */
        Vector3 scaling = transform.localScale;             // Get local scale of the transform
        scaling.x *= -1;                                    // Invert direction
        transform.localScale = scaling;                     // Affect new direction to the local scale of the transform
    }

    /* Function to detect collision with walls */
    void OnCollisionEnter2D(Collision2D pCollision)
    {
        /* If the colliding game object is not the player, then proceed */
        if (!pCollision.transform.gameObject.name.Equals("Player"))
        {
            /* Inverse move direction */
            horizontalMove *= (-1);
            verticalMove *= (-1);

            bCollision = true;      // Change boolean to true to change direction of the knight
        }
        else
        {
            Physics2D.IgnoreCollision(pCollision.collider, pCollision.otherCollider);   // Ignoring collision with player
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        bCollision = false;   // Change boolean to false to stop change direction of the knight
    }
}
