using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> unsavedMissionLogs = new List<GameObject>();
    [System.NonSerialized] public int unsavedOxygenContainers;
    [System.NonSerialized] public int currentPlayerHealth;
    private static LevelManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] ProgressBar oxygenBar;
    [SerializeField] private int totalPlayerHealth;
    [SerializeField] private Inventory inventory;

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
        inventory.CollectedMissionLog(missionLog.GetComponent<MissionLog>().missionLogNumber);
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
        GameState.GetInstance().savedOxygenContainers += unsavedOxygenContainers;
        unsavedOxygenContainers = 0;
    }

    public void LoadProgress()
    {
        for (int i = 0; i < unsavedMissionLogs.Count; i++)
        {
            unsavedMissionLogs[i].SetActive(true);
        }
        unsavedMissionLogs.Clear();
        inventory.InitInventory();
        for (int j = 0; j < GameState.GetInstance().savedMissionLogs.Count; j++)
        {
            GameObject missionLog = GameState.GetInstance().savedMissionLogs[j];
            missionLog.SetActive(false);
            inventory.CollectedMissionLog(missionLog.GetComponent<MissionLog>().missionLogNumber);
        }
        if(GameState.GetInstance().hasMiles)
        {
            inventory.AcquiredMiles();
        }
        if(GameState.GetInstance().gotOrchid)
        {
            inventory.AcquiredOrchid();
        }
        //place player at checkpoint position
        player.transform.position = GameState.GetInstance().checkpointPositions[GameState.GetInstance().lastCheckpoint];
        currentPlayerHealth = totalPlayerHealth;
        oxygenBar.BarValue = 75;
        GameState.GetInstance().gamePaused = false;
        //player.GetComponent<OxygenState>().StartOxygenLoss();
        unsavedOxygenContainers = 0;
        //Update OxygenContainerUI
    }

    public static LevelManager GetInstance()
    {
        return Instance;
    }

    public void StartWayBack()
    {
        SaveProgress(5); //TODO: replace with accurate value
        //TODO: make creatures aggressive -> change their path pattern and make them deal damage to the player
        //TODO: change oxygen depleting speed
        //TODO: make Richmond follow the player
    }
}
