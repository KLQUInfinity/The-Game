using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public GameObject LevelsHolder;

    [HideInInspector] public MonoBehaviour[] levels;

    public int currentLevelIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levels = LevelsHolder.GetComponents<MonoBehaviour>();
        currentLevelIndex = 0;
        StartLevel(currentLevelIndex);
    }

    public void StartLevel(int index)
    {
        foreach(MonoBehaviour level in levels)
        {
            level.enabled = false;
        }

        levels[index].enabled = true;
    }
}
