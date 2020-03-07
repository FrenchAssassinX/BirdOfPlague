using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameVictoryManager : MonoBehaviour
{
    private int arrowPosition;

    public GameObject nextButton;
    public GameObject menuButton;
    public GameObject quitButton;
    public GameObject arrowCursor;

    void Start()
    {
        arrowPosition = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrowPosition++;
            FindObjectOfType<AudioManager>().Play("Cursor");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrowPosition--;
            FindObjectOfType<AudioManager>().Play("Cursor");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (arrowPosition == 0)
            {
                //ReloadGameplayScene();
            }
            else if (arrowPosition == 1)
            {
                GoToMenuScene();
            }
            else if (arrowPosition == 2)
            {
                ExitGame();
            }

            FindObjectOfType<AudioManager>().Play("Validate");
        }

        HandlingCursorPosition();
        LimitCursor();
    }

    /* Function to change position of the cursor */
    private void HandlingCursorPosition()
    {
        if (arrowPosition == 0)
        {
            arrowCursor.GetComponent<RectTransform>().localPosition = new Vector3(
                nextButton.GetComponent<RectTransform>().localPosition.x - nextButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                nextButton.GetComponent<RectTransform>().localPosition.y - nextButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
                0
            );
        }
        else if (arrowPosition == 1)
        {
            arrowCursor.GetComponent<RectTransform>().localPosition = new Vector3(
                menuButton.GetComponent<RectTransform>().localPosition.x - menuButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                menuButton.GetComponent<RectTransform>().localPosition.y - menuButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
                0
            );
        }
        else if (arrowPosition == 2)
        {
            arrowCursor.GetComponent<RectTransform>().localPosition = new Vector3(
                quitButton.GetComponent<RectTransform>().localPosition.x - quitButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                quitButton.GetComponent<RectTransform>().localPosition.y - quitButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
                0
            );
        }
    }

    /* Function to limit cursor position */
    private void LimitCursor()
    {
        if (arrowPosition < 0)
        {
            arrowPosition = 2;
        }

        if (arrowPosition > 2)
        {
            arrowPosition = 0;
        }
    }

    //private void ReloadGameplayScene()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
