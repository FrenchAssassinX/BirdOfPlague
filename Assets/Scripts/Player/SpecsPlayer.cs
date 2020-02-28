using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecsPlayer : MonoBehaviour
{
    public float playerLifePoints;      // Life Points of the player
    private Animator playerAnimator;    // Animator of the player

    void Start()
    {
        playerLifePoints = 2f;
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (playerLifePoints <= 0f)
        {
            Debug.Log("**** GAME OVER ****");

            playerAnimator.SetBool("IsDead", true);
        }
    }

    public void KillPlayer()
    {
        Destroy(gameObject);
    }
}
