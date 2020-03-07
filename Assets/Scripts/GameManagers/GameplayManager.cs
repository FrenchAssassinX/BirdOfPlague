using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private GameObject player;                  // Player game object

    private GameObject gameVictoryManager;
    private GameObject gameVictoryPanel;
    private GameObject gameOverManager;
    private GameObject gameOverPanel;

    private GameObject[] villagers;
    private List<GameObject> listVillagers;
    private List<GameObject> villagerToRemove;

    private bool bIsMusicGameOverPlayed;
    private bool bIsMusicVictoryPlayed;

    void Start()
    {
        player = GameObject.Find("Player");

        gameVictoryManager = GameObject.Find("GameVictoryManager");
        gameVictoryPanel = GameObject.Find("GameVictoryPanel");
        gameOverManager = GameObject.Find("GameOverManager");
        gameOverPanel = GameObject.Find("GameOverPanel");

        villagers = GameObject.FindGameObjectsWithTag("Villager");
        listVillagers = villagers.ToList();
        villagerToRemove = new List<GameObject>();

        bIsMusicGameOverPlayed = false;
        bIsMusicVictoryPlayed = false;

        gameVictoryManager.SetActive(false);
        gameVictoryPanel.SetActive(false);
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

        /* Playing game over music when game over panel is displayed */
        if (!bIsMusicGameOverPlayed && gameOverPanel.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
            bIsMusicGameOverPlayed = true;
        }
        
        /* Verify wich villagers are infected */
        foreach (GameObject villager in listVillagers)
        {
            if (villager.GetComponent<MachineStateVillager>().bIsContamined)
            {
                villagerToRemove.Add(villager);
            }
        }

        /* Remove villagers infected from the list */
        foreach (GameObject villager in villagerToRemove)
        {
            listVillagers.Remove(villager);
        }

        if (listVillagers.Count <= 0)
        {
            player.GetComponent<MovePlayer>().bCanMove = false;
            gameVictoryManager.SetActive(true);
            gameVictoryPanel.SetActive(true);
        }

        /* Playing game over music when game over panel is displayed */
        if (!bIsMusicVictoryPlayed && gameVictoryPanel.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().Play("Victory");
            bIsMusicVictoryPlayed = true;
        }
    }

}
