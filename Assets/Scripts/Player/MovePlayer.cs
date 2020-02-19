using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody2D body2D;              // Body 2D of the sprite
    float horizontalMove = 0f;              // Float to handling horizontal moves of the sprite
    float verticalMove = 0f;                // Float to handling vertical moves of the sprite
    public float moveSpeed = 10f;          // Move speed multiply by horizontalMoves or verticalMoves to create velocity 

    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");       // Get x moves with inputs
        verticalMove = Input.GetAxis("Vertical");           // Get y moves with inputs
    }

    private void FixedUpdate()
    {
        Vector2 newVelocity = body2D.velocity;              // Get actual velocity of the body

        newVelocity.x = horizontalMove * moveSpeed;         // Horizontal move
        newVelocity.y = verticalMove * moveSpeed;           // Vertical move

        body2D.velocity = newVelocity;                      // Give to the body the new velocity
    }
}
