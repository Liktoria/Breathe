using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [System.NonSerialized] public int lastCheckpoint;
    [System.NonSerialized] public List<GameObject> savedMissionLogs = new List<GameObject>();
    [System.NonSerialized] public bool gamePaused = true; 
    [System.NonSerialized] public bool hasMiles = false; 
    [System.NonSerialized] public int savedOxygenContainers;
    [System.NonSerialized] public bool savedRichmond;
    [System.NonSerialized] public bool gotOrchid;
    [System.NonSerialized] public bool firstCollectOxygen = true;
    [System.NonSerialized] public bool firstCollectMissionLog = true;
    public Vector3[] checkpointPositions;
    public int minExtraOxygenToSaveRichmond;
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
