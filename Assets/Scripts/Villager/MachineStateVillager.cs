using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStateVillager : MonoBehaviour
{
    public Rigidbody2D villagerBody2D;                  // RigidBody2D of the villager
    public BoxCollider2D[] boxColliders;                // Table of all BoxCollider2D of the villager
    public Animator villagerAnimator;                   // Animator of the villager

    public GameObject player;                           // Player Game Object

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

    void Start()
    {
        villagerBody2D = gameObject.GetComponent<Rigidbody2D>();            // Get RigidBody2D of the villager 
        boxColliders = gameObject.GetComponents<BoxCollider2D>();           // Find all colliders of the villager

        villagerAnimator = gameObject.GetComponent<Animator>();             // Get Animator attached to the villager

        player = GameObject.Find("Player");                                 // Keep player in memory

        STATE_MACHINE = new string[] { "", "walk", "change", "afraid" };    // Initialize state machine
        currentState = STATE_MACHINE[0];                                    // By default state of villager is empty

        moveSpeed = INITIAL_SPEED;                                          // Standard move speed of the villager
    }

    void Update()
    {
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

            horizontalMove *= (-1);                                                 // Invert horizontal move
            verticalMove *= (-1);                                                   // Invert vertical move

            newVelocity.x = horizontalMove * moveSpeed * 3 * (float)Math.Cos(angle);    // Affect X velocity
            newVelocity.y = verticalMove * moveSpeed * 3 * (float)Math.Sin(angle);      // Affect Y velocity

            villagerBody2D.velocity = newVelocity;                                  // Affect new velocity to Body2D

            currentState = STATE_MACHINE[1];                                        // Return to walk state
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
            villagerAnimator.SetBool("IsWalking", true);
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
        /* End Handling animations */
    }

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
        if (pCollision.otherCollider == boxColliders[0])
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
        else if (pCollision.otherCollider == boxColliders[1])
        {
            /* If the villager see the player in his field of view, then he runs ! */
            if (pCollision.gameObject == player)
            {
                currentState = STATE_MACHINE[3];
            }
            /* Else is just a wall so change only direction and keep mooving */
            else
            {
                horizontalMove *= (-1);
                verticalMove *= (-1);

                bCollision = true;
            }
        }
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        bCollision = false;   // Change boolean to false to stop change direction of the villager
    }
}
