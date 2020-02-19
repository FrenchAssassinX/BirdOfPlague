using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersPlayer : MonoBehaviour
{
    public BoxCollider2D playerCollider;

    void Start()
    {
        playerCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        /* Avoiding to rotate player when colliding with rounded collider */
        Quaternion newRotation = new Quaternion(0f, 0f, 0f, 0f);
        this.transform.localRotation = newRotation;

        /* Keep BoxCollider2D with the same position as the player */
        playerCollider.transform.position = this.transform.position;
    }
}
