using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePNJ : MonoBehaviour
{
    public Rigidbody2D pnjBody2D;               // Body2D of PNJ

    private float horizontalMove = 10f;        // Float to handling horizontal moves of the sprite
    private float verticalMove = 10f;          // Float to handling vertical moves of the sprite
    public float moveSpeed = 4f;                // Move speed multiply by horizontalMoves or verticalMoves to create velocity

    private bool bSpriteFacingRight = true;     // Boolean to flip sprite on the good direction

    /* Move property */
    public bool bStartTimerMove = false;        // Boolean to start timer for move
    public float TIMER_MOVE_VALUE = 3f;         // Default value for the timer
    public float timerMove;                     // Timer to specify how many time spent during a move

    /* New move property */
    public bool bStartTimerNewMove;             // Boolean to start timer of new move
    public float TIMER_NEWMOVE_VALUE = 50f;     // Default value for the timer
    public float timerNewMove;                  // Timer to specify how many time spent during each moves of the pnj

    void Start()
    {
        pnjBody2D = GetComponent<Rigidbody2D>();

        timerMove = TIMER_MOVE_VALUE;
        timerNewMove = TIMER_NEWMOVE_VALUE;
    }
    
    void Update()
    {
        if (horizontalMove < 0 && bSpriteFacingRight)
        {
            FlipSprite();
        }
        else if (horizontalMove > 0 && !bSpriteFacingRight)
        {
            FlipSprite();
        }
    }

    private void FixedUpdate()
    {
        if (timerNewMove > 0)
        {
            timerNewMove--;
        }
        else
        {
            AutomaticMoves();
            Debug.Log("Move PNJ !");
            timerNewMove = TIMER_NEWMOVE_VALUE;
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

    private void AutomaticMoves()
    {
        if (timerMove > 0)
        {
            Vector2 newVelocity = pnjBody2D.velocity;           // Get actual velocity of the body

            System.Random random = new System.Random();         // New random

            int directionX = random.Next(1, 3);                 // Generate random x move: 1 for left, 2 for right
            int directionY = random.Next(1, 3);                 // Generate random y move: 1 for top, 2 for down

            if (directionX.Equals(1))
            {
                newVelocity.x = -horizontalMove * moveSpeed;

                if (directionY.Equals(1))
                {
                    // Move Top Left
                    newVelocity.y = -verticalMove * moveSpeed;
                }
                else if (directionY.Equals(2))
                {
                    // Move Down Left
                    newVelocity.y = verticalMove * moveSpeed;
                }
            }
            else if (directionX.Equals(2))
            {
                newVelocity.x = horizontalMove * moveSpeed;

                if (directionY.Equals(1))
                {
                    // Move Top Right
                    newVelocity.y = -verticalMove * moveSpeed;
                }
                else if (directionY.Equals(2))
                {
                    // Move Down Right
                    newVelocity.y = verticalMove * moveSpeed;
                }
            }

            pnjBody2D.velocity = newVelocity;                   // Affect new velocity to the body

            timerMove--;
        }
        else
        {
            timerMove = TIMER_MOVE_VALUE;
        }
    }
}
