using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private GameObject player;                  // Player game object
    private GameObject gameOverManager;
    private GameObject gameOverPanel;

    private bool bIsMusicPlayed;

    void Start()
    {
        player = GameObject.Find("Player");
        gameOverManager = GameObject.Find("GameOverManager");
        gameOverPanel = GameObject.Find("GameOverPanel");

        bIsMusicPlayed = false;

        gameOverManager.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        /* If player is dead, go to game over scene */
        if (player == null)
        {
            gameOverManager.SetActive(true);
            gameOverPanel.SetActive(true);
        }

        if (!bIsMusicPlayed && gameOverPanel.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
            bIsMusicPlayed = true;
        }
    }

}
