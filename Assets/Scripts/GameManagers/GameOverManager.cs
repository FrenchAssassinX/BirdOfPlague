using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private int arrowPosition;

    public GameObject replayButton;
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
                ReloadGameplayScene();
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
                replayButton.GetComponent<RectTransform>().localPosition.x - replayButton.GetComponent<RectTransform>().sizeDelta.x / 2 - arrowCursor.GetComponent<RectTransform>().sizeDelta.x,
                replayButton.GetComponent<RectTransform>().localPosition.y - replayButton.GetComponent<RectTransform>().sizeDelta.y / 2 + arrowCursor.GetComponent<RectTransform>().sizeDelta.y,
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

    private void ReloadGameplayScene()
    {
        DontDestroyOnLoad(GameObject.Find("CurrentLevel"));
        GameObject.Find("CurrentLevel").GetComponent<CurrentLevel>().bLoadNextLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
