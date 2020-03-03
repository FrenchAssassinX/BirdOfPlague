using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private int arrowPosition;

    private GameObject playButton;
    private GameObject helpButton;
    private GameObject exitButton;
    private GameObject arrowCursor;         // Arrow cursor to select button

    void Start()
    {
        arrowPosition = 0;

        playButton = GameObject.Find("PlayButton");
        helpButton = GameObject.Find("HelpButton");
        exitButton = GameObject.Find("ExitButton");
        arrowCursor = GameObject.Find("ArrowCursor");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrowPosition++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrowPosition--;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (arrowPosition == 0)
            {
                GoToGameplayScene();
            }
            else if (arrowPosition == 1)
            {
                GoToHelpScene();
            }
            else if (arrowPosition == 2)
            {
                ExitGame();
            }
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
                playButton.GetComponent<RectTransform>().localPosition.x - playButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                playButton.GetComponent<RectTransform>().localPosition.y - playButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
                0
            );
        }
        else if (arrowPosition == 1)
        {
            arrowCursor.GetComponent<RectTransform>().localPosition = new Vector3(
                helpButton.GetComponent<RectTransform>().localPosition.x - helpButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                helpButton.GetComponent<RectTransform>().localPosition.y - helpButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
                0
            );
        }
        else if (arrowPosition == 2)
        {
            arrowCursor.GetComponent<RectTransform>().localPosition = new Vector3(
                exitButton.GetComponent<RectTransform>().localPosition.x - exitButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                exitButton.GetComponent<RectTransform>().localPosition.y - exitButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
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

    private void GoToGameplayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    private void GoToHelpScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
