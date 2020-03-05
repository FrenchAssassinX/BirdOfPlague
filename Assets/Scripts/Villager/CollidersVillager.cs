using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersVillager : MonoBehaviour
{
    public BoxCollider2D colliderVillager;

    void Start()
    {
        colliderVillager = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        /* Avoiding to rotate villager when colliding with rounded collider */
        Quaternion newRotation = new Quaternion(0f, 0f, 0f, 0f);
        transform.localRotation = newRotation;

        /* Keep BoxCollider2D with the same position as the player */
        colliderVillager.transform.position = this.transform.position;
    }
}
