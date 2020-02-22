using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D body2D;                  // Body 2D of the sprite
    public Animator animator;                   // animator attach to player

    private float horizontalMove = 0f;          // Float to handling horizontal moves of the sprite
    private float verticalMove = 0f;            // Float to handling vertical moves of the sprite
    public float moveSpeed = 4f;                // Move speed multiply by horizontalMoves or verticalMoves to create velocity 

    private bool bSpriteFacingRight = true;     // Boolean to flip sprite on the good direction
    public bool bIsBitting = false;             // Boolean to detect where player is bitting

    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();   // Get RigigBody2D attached to the player
        animator = GetComponent<Animator>();    // Get animator attached to the player
    }
    
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");       // Get x moves with inputs
        verticalMove = Input.GetAxis("Vertical");           // Get y moves with inputs

        /* Input for bitting villagers */
        if (Input.GetKeyDown(KeyCode.P))
        {
            bIsBitting = true;
        }
        else
        {
            bIsBitting = false;
        }

        /* Start Animator settings */
        /* Running animation */
        if (Mathf.Abs(horizontalMove) > 0 || Mathf.Abs(verticalMove) > 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        /* Bitting animation */
        if (bIsBitting)
        {
            animator.SetBool("IsBitting", true);
        }
        else
        {
            animator.SetBool("IsBitting", false);
        }
        /* End Animator settings */

        /* Flipping sprite */
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
        Vector2 newVelocity = body2D.velocity;              // Get actual velocity of the body

        newVelocity.x = horizontalMove * moveSpeed;         // Horizontal move
        newVelocity.y = verticalMove * moveSpeed;           // Vertical move

        body2D.velocity = newVelocity;                      // Give to the body the new velocity
    }

    private void FlipSprite()
    {
        bSpriteFacingRight = !bSpriteFacingRight;           // Flip facing

        /* Multiply by -1 the x player scale */
        Vector3 scaling = transform.localScale;             // Get local scale of the transform
        scaling.x *= -1;                                    // Invert direction
        transform.localScale = scaling;                     // Affect new direction to the local scale of the transform
    }
}
