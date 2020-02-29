using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private GameObject player;                  // Player game object

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        /* If player is dead, go to game over scene */
        if (player == null)
        {
            GoToGameOverScene();
        }
    }

    public void GoToGameOverScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
