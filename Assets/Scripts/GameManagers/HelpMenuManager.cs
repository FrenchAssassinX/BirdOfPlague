using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenuManager : MonoBehaviour
{
    private GameObject returnText;
    private int TIMER_VALUE = 40;
    public int timer;

    void Start()
    {
        returnText = GameObject.Find("ReturnText");     // Find return text in hierarchy
        timer = TIMER_VALUE;                            // Assign default value to timer
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GoToMenuScene();
            FindObjectOfType<AudioManager>().Play("Validate");
        }

        AnimateReturnText();
    }

    /* Function to animate return text */
    void AnimateReturnText()
    {
        /* If text is active then decrease timer */
        if (returnText.activeInHierarchy)
        {
            if (timer > 0)
            {
                timer--;

                if (timer <= 0)
                {
                    returnText.SetActive(false);        // Disable text
                }
            }
        }
        /* If text is disable then increase timer */
        else
        {
            if (timer < TIMER_VALUE)
            {
                timer++;

                if (timer >= TIMER_VALUE)
                {
                    returnText.SetActive(true);         // Able text
                }
            }
        }
    }

    /* Function to return to main menu scene */
    private void GoToMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
