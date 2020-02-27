using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecsPlayer : MonoBehaviour
{
    public float playerLifePoints;      // Life Points of the player

    void Start()
    {
        playerLifePoints = 2f;
    }

    
    void Update()
    {
        if (playerLifePoints <= 0f)
        {
            Debug.Log("**** GAME OVER ****");

            Destroy(gameObject);
        }
    }
}
