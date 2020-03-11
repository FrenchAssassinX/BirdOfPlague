using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public int currentLevel;
    public bool bLoadNextLevel;

    void Start()
    {
        currentLevel = 1;
        bLoadNextLevel = true;
    }
}
