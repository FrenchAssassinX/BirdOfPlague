using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersKnight : MonoBehaviour
{
    public BoxCollider2D colliderKnight;

    void Start()
    {
        colliderKnight = this.gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        /* Avoiding to rotate villager when colliding with rounded collider */
        Quaternion newRotation = new Quaternion(0f, 0f, 0f, 0f);
        this.transform.localRotation = newRotation;

        /* Keep BoxCollider2D with the same position as the player */
        colliderKnight.transform.position = this.transform.position;
    }
}
