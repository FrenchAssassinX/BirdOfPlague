using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStateVillager : MonoBehaviour
{
    public GameObject emote;

    public Rigidbody2D villagerBody2D;                  // RigidBody2D of the villager
    public Animator villagerAnimator;                   // Animator of the villager

    public GameObject player;                           // Player Game Object

    public GameObject prefabDeadEffect;                 // Prefab to play dead effect on sprite dead

    public Transform castPoint;                         // Transform to give a view to villager
    public float viewSight = 3f;                        // Distance of view of the villager

    public string[] STATE_MACHINE;                      // State machine for villager
    public string currentState;                         // Value to verify current state of villager

    private float horizontalMove = 0.1f;                // Float to handling horizontal moves of the sprite
    private float verticalMove = 0.1f;                  // Float to handling vertical moves of the sprite
    public float moveSpeed;                             // Move speed multiply by horizontalMoves or verticalMoves to create velocity 
    public float INITIAL_SPEED = 2f;                    // Standard speed of the villager

    bool bCollision = false;                            // Boolean to detect collision
    private bool bSpriteFacingRight = true;             // Boolean to flip sprite on the good direction

    private bool bIsAfraid = false;                     // Boolean to change animation of villager when he's afraid by the vampire
    private bool bIsWalking= false;                     // 
    private bool bIsIdle = false;                       // 
    private bool bIsDead = false;                       // 
    public bool bIsContamined = false;                 // 

    private float afraidSpeed = 10f;

    public float lifePoints = 1f;                      // Life points of the villager

    void Start()
    {
        emote = transform.GetChild(1).gameObject;

        villagerBody2D = gameObject.GetComponent<Rigidbody2D>();                                    // Get RigidBody2D of the villager 
        villagerAnimator = gameObject.GetComponent<Animator>();                                     // Get Animator attached to the villager

        player = GameObject.Find("Player");                                                         // Keep player in memory

        castPoint = transform.GetChild(0).gameObject.transform;                                     // Find castPoint on scene

        STATE_MACHINE = new string[] { "", "walk", "change", "afraid", "dead", "contamined" };      // Initialize state machine
        currentState = STATE_MACHINE[0];                                                            // By default state of villager is empty

        moveSpeed = INITIAL_SPEED;                                                                  // Standard move speed of the villager

        emote.SetActive(false);
    }

    void Update()
    {
        float actualLifePoints = lifePoints;

        if (lifePoints <= 0 && !bIsContamined)
        {
            bIsDead = true;
            currentState = STATE_MACHINE[4];
        }

        /* If villager see the player, start running from him ! */
        if (CanSeePlayer(viewSight) && !bIsContamined)
        {
            RunFromPlayer();
        }

        /* Flipping sprite */
        if (horizontalMove > 0 && bSpriteFacingRight)
        {
            FlipSprite();
        }
        else if (horizontalMove < 0 && !bSpriteFacingRight)
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
            bIsAfraid = false;                          // Is afraid boolean passed to false for animation
            bIsWalking = true;
            bIsIdle = false;

            moveSpeed = INITIAL_SPEED;

            /* If collision is detected */
            if (bCollision)
            {
                bIsIdle = true;

                /* Stop the villager by setting velocity to 0 */
                Vector2 newVelocity = new Vector2();
                newVelocity.x = 0;
                newVelocity.y = 0;
                villagerBody2D.velocity = newVelocity;

                currentState = STATE_MACHINE[2];            // Change state of the villager to change direction
            }
        }
        else if (currentState == STATE_MACHINE[2])      // Change direction state
        {
            bIsAfraid = false;                                                      // Is afraid boolean passed to false for animation
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

            villagerBody2D.velocity = newVelocity;                                  // Affect new velocity to Body2D

            currentState = STATE_MACHINE[1];                                        // Change state to walk
        }
        else if (currentState == STATE_MACHINE[3])      // Afraid state
        {
            bIsAfraid = true;                                                       // Is afraid boolean passed to true for animation
            bIsWalking = false;
            bIsIdle = false;

            System.Random random = new System.Random();                             // Create new random

            /* Generating random X and Y direction for the player */
            float directionX = (gameObject.transform.position.x) - (random.Next(1, 129));
            float directionY = (gameObject.transform.position.y) - (random.Next(1, 129));

            double angle = Math.Atan2(directionX, directionY);                      // Calculating an angle with the X and Y direction

            Vector2 newVelocity = new Vector2();                                    // Vector2 for new velocity

            /* Handling moves of the villager based on the position of the player */
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

            newVelocity.x = horizontalMove * moveSpeed * afraidSpeed * (float)Math.Cos(angle);    // Affect X velocity
            newVelocity.y = verticalMove * moveSpeed * afraidSpeed * (float)Math.Sin(angle);      // Affect Y velocity

            villagerBody2D.velocity = newVelocity;                                  // Affect new velocity to Body2D

            Invoke("StopRunFromPlayer", 5);
            currentState = STATE_MACHINE[1];                                        // Return to walk state
        }
        else if (currentState == STATE_MACHINE[4])  // Dead state
        {
            Vector2 newVelocity = new Vector2();                                    // Vector2 for new velocity

            newVelocity.x = 0;                                                      // Affect X velocity
            newVelocity.y = 0;                                                      // Affect Y velocity

            villagerBody2D.velocity = newVelocity;                                  // Affect new velocity to Body2D

            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (currentState == STATE_MACHINE[5])
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            currentState = STATE_MACHINE[0];
        }
        /* End Handling State Machine */

        /* Handling animations */
        if (bIsAfraid)
        {
            villagerAnimator.SetBool("IsAfraid", true);
        }
        else
        {
            villagerAnimator.SetBool("IsAfraid", false);
        }

        if (bIsWalking)
        {
            if (bIsContamined)
            {
                villagerAnimator.SetBool("IsContamined", true);
            }
            else
            {
                villagerAnimator.SetBool("IsWalking", true);
            }
        }
        else
        {
            villagerAnimator.SetBool("IsWalking", false);
        }

        if (bIsIdle)
        {
            villagerAnimator.SetBool("IsIdle", true);
        }
        else
        {
            villagerAnimator.SetBool("IsIdle", false);
        }

        if (bIsDead)
        {
            villagerAnimator.SetBool("IsDead", true);
        }
        else
        {
            villagerAnimator.SetBool("IsDead", false);
        }
        /* End Handling animations */
    }

    public void Contamined()
    {
        bIsContamined = true;
        currentState = STATE_MACHINE[5];
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
    private void RunFromPlayer()
    {
        currentState = STATE_MACHINE[3];
        emote.SetActive(true);
    }

    /* Function to stop running from player */
    private void StopRunFromPlayer()
    {
        currentState = STATE_MACHINE[2];                                        // Return to walk state
        emote.SetActive(false);
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

            bCollision = true;      // Change boolean to true to change direction of the villager
        }
        else
        {
            Physics2D.IgnoreCollision(pCollision.collider, pCollision.otherCollider);   // Ignoring collision with player
        }    
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        bCollision = false;   // Change boolean to false to stop change direction of the villager
    }
}
