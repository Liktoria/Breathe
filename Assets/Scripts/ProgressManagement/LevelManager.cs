using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> unsavedMissionLogs = new List<GameObject>();
    [System.NonSerialized] public int currentPlayerHealth;
    private static LevelManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] ProgressBar oxygenBar;
    [SerializeField] private int totalPlayerHealth;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentPlayerHealth = totalPlayerHealth;
    }

    public void AddMissionLog(GameObject missionLog)
    {
        //add to inventory
        unsavedMissionLogs.Add(missionLog);
        //display in inventory
    }

    public void SaveProgress(int checkPointIndex)
    {
        foreach(GameObject missionLog in unsavedMissionLogs)
        {
            GameState.GetInstance().savedMissionLogs.Add(missionLog);
        }
        unsavedMissionLogs.Clear();
        GameState.GetInstance().lastCheckpoint = checkPointIndex;
    }

    public void LoadProgress()
    {
        for (int i = 0; i < unsavedMissionLogs.Count; i++)
        {
            unsavedMissionLogs[i].SetActive(true);
        }
        unsavedMissionLogs.Clear();
        //TODO: update inventory UI
        for (int j = 0; j < GameState.GetInstance().savedMissionLogs.Count; j++)
        {
            GameState.GetInstance().savedMissionLogs[j].SetActive(false);
        }
        //place player at checkpoint position
        player.transform.position = GameState.GetInstance().checkpointPositions[GameState.GetInstance().lastCheckpoint];
        currentPlayerHealth = totalPlayerHealth;
        oxygenBar.BarValue = 75;
        GameState.GetInstance().gamePaused = false;
        player.GetComponent<OxygenState>().StartOxygenLoss();
    }

    public static LevelManager GetInstance()
    {
        return Instance;
    }
}
