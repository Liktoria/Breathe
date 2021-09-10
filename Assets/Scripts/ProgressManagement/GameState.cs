using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [System.NonSerialized] public int lastCheckpoint;
    [System.NonSerialized] public List<GameObject> savedMissionLogs = new List<GameObject>();
    public Vector3[] checkpointPositions;
    private static GameState Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public static GameState GetInstance()
    {
        return Instance;
    }
}
