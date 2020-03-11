using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject player;               // Initialize in Inspector
    public GameObject prefabVillager;       // Initialize in Inspector
    public GameObject prefabKnight;         // Initialize in Inspector
    public List<GameObject> listLevels;     // All levels existing on GameplayScene

    public Dictionary<int, Vector3> villagersPosition;
    public Dictionary<int, Vector3> knightsPosition;

    public bool bLevelReady;

    void Awake()
    {
        listLevels = FindLevelsOnScene();
        bLevelReady = false;

        villagersPosition = new Dictionary<int, Vector3>();
        knightsPosition = new Dictionary<int, Vector3>();
    }

    void Update()
    {
        if (GameObject.Find("CurrentLevel").GetComponent<CurrentLevel>().bLoadNextLevel)
        {
            LoadLevel(GameObject.Find("CurrentLevel").GetComponent<CurrentLevel>().currentLevel, villagersPosition, knightsPosition);
        }
    }

    private List<GameObject> FindLevelsOnScene()
    {
        List<GameObject> localList = new List<GameObject>();

        foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (gameObject.name.Contains("GridLevel"))
            {
                localList.Add(gameObject);
            }
        }

        return localList;
    }

    private void LoadLevel(int pCurrentLevel, Dictionary<int, Vector3> pVillagersPos, Dictionary<int, Vector3> pKnightsPos)
    {
        pVillagersPos.Clear();
        pKnightsPos.Clear();

        OnChangeLevel(pCurrentLevel);

        switch (pCurrentLevel)
        {
            case 1:
                pVillagersPos.Add(1, new Vector3(8f, -5.5f, 0f));
                pVillagersPos.Add(2, new Vector3(7f, -1f, 0f));
                pVillagersPos.Add(3, new Vector3(9.5f, 6f, 0f));
                pVillagersPos.Add(4, new Vector3(-8.5f, 3f, 0f));

                pKnightsPos.Add(1, new Vector3(-2f, -5f, 0f));
                pKnightsPos.Add(2, new Vector3(-4f, -1.5f, 0f));
                pKnightsPos.Add(3, new Vector3(1.5f, 4.5f, 0f));
                pKnightsPos.Add(4, new Vector3(0f, 5.5f, 0f));

                player.transform.position = new Vector3(-12f, -6f, 0f);    // Starting postion on player

                CreateVillagers(pVillagersPos);
                CreateKnights(pKnightsPos);

                break;

            case 2:
                pVillagersPos.Add(1, new Vector3(12f, 3.5f, 0f));
                pVillagersPos.Add(2, new Vector3(22f, 2.5f, 0f));
                pVillagersPos.Add(3, new Vector3(9.5f, -1f, 0f));
                pVillagersPos.Add(4, new Vector3(13f, -9.5f, 0f));
                pVillagersPos.Add(5, new Vector3(57f, -19f, 0f));
                pVillagersPos.Add(6, new Vector3(67f, -19f, 0f));

                pKnightsPos.Add(1, new Vector3(14f, -5f, 0f));
                pKnightsPos.Add(2, new Vector3(25f, -2f, 0f));
                pKnightsPos.Add(3, new Vector3(23f, -8f, 0f));
                pKnightsPos.Add(4, new Vector3(55f, -36f, 0f));
                pKnightsPos.Add(5, new Vector3(63f, -36f, 0f));

                player.transform.position = new Vector3(-12f, -6f, 0f);    // Starting postion on player

                CreateVillagers(pVillagersPos);
                CreateKnights(pKnightsPos);

                break;

            default:
                break;
        }

        GameObject.Find("CurrentLevel").GetComponent<CurrentLevel>().bLoadNextLevel = false;
    }

    private void OnChangeLevel(int pCurrentLevel)
    {
        foreach (GameObject level in listLevels)
        {
            if (level.name.Equals("GridLevel" + pCurrentLevel))
            {
                level.SetActive(true);
            }
            else
            {
                level.SetActive(false);
            }
        }
    }

    private void CreateVillagers(Dictionary<int, Vector3> pVillagersPos)
    {
        foreach (KeyValuePair<int, Vector3> pair in pVillagersPos)
        {
            Instantiate(prefabVillager, pair.Value, Quaternion.identity);
        }
    }

    private void CreateKnights(Dictionary<int, Vector3> pKnightsPos)
    {
        foreach (KeyValuePair<int, Vector3> pair in pKnightsPos)
        {
            Instantiate(prefabKnight, pair.Value, Quaternion.identity);
        }
    }
}
