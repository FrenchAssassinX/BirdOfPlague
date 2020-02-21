using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineStateVillager : MonoBehaviour
{
    public Rigidbody2D villagerBody2D;

    public string[] STATE_MACHINE;                      // State machine for villager
    public string currentState;                         // Value to verify current state of villager

    private float horizontalMove = 0.3f;                // Float to handling horizontal moves of the sprite
    private float verticalMove = 0.3f;                  // Float to handling vertical moves of the sprite
    public float moveSpeed = 2f;                        // Move speed multiply by horizontalMoves or verticalMoves to create velocity 

    bool bCollision = false;                            // Boolean to detect collision
    private bool bSpriteFacingRight = true;             // Boolean to flip sprite on the good direction

    void Start()
    {
        villagerBody2D = this.gameObject.GetComponent<Rigidbody2D>();   // Get RigidBody2D of the villager 

        STATE_MACHINE = new string[] { "", "walk", "change" };          // Initialize state machine

        currentState = STATE_MACHINE[0];                                // By default state of villager is empty
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
        if (currentState == STATE_MACHINE[0])
        {
            // Change direction
            currentState = STATE_MACHINE[2];
        }
        else if (currentState == STATE_MACHINE[1])
        {
            /* If collision is detected */
            if (bCollision)
            {
                /* Stop the villager by setting velocity to 0 */
                Vector2 newVelocity = new Vector2();
                newVelocity.x = 0;
                newVelocity.y = 0;
                villagerBody2D.velocity = newVelocity;

                currentState = STATE_MACHINE[2];            // Change state of the villager to change direction
            }
        }
        else if (currentState == STATE_MACHINE[2])
        {
            System.Random random = new System.Random();                             // Create new random

            /* Generating random X and Y direction for the player */
            float directionX = (this.gameObject.transform.position.x) - (random.Next(1, 129));
            float directionY = (this.gameObject.transform.position.y) - (random.Next(1, 129));

            double angle= Math.Atan2(directionX, directionY);                       // Calculating an angle with the X and Y direction

            Vector2 newVelocity = new Vector2();                                    // Vector2 for new velocity
            newVelocity.x = horizontalMove * moveSpeed * (float)Math.Cos(angle);    // Affect X velocity
            newVelocity.y = verticalMove * moveSpeed * (float)Math.Sin(angle);      // Affect Y velocity

            villagerBody2D.velocity = newVelocity;                                  // Affect new velocity to Body2D

            currentState = STATE_MACHINE[1];                                        // Change state to walk
        }
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
        /* Inverse move direction */
        if (pCollision.transform.position.x >= this.transform.position.x)
        {
            horizontalMove *= (-1);
        }
        else if (pCollision.transform.position.x < this.transform.position.x)
        {
            horizontalMove *= (-1);
        }

        if (pCollision.transform.position.y >= this.transform.position.y)
        {
            verticalMove *= (-1);
        }
        else if (pCollision.transform.position.y < this.transform.position.y)
        {
            verticalMove *= (-1);
        }

        bCollision = true;      // Change boolean to true to change direction of the villager
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        bCollision = false;   // Change boolean to false to stop change direction of the villager
    }
}
