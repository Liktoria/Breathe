using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> unsavedMissionLogs = new List<GameObject>();
    [SerializeField] List<GameObject> oxygenBubbles = new List<GameObject>();
    [System.NonSerialized] public int unsavedOxygenContainers;
    [System.NonSerialized] public int currentPlayerHealth;
    private static LevelManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] ProgressBar oxygenBar;
    [SerializeField] private int totalPlayerHealth;
    [SerializeField] private Inventory inventory;
    [SerializeField] private OxygenStorage oxygenStorage;
    [SerializeField] private OxygenState oxygenState;
    [SerializeField] private float oxygenReloadValue;
    [SerializeField] private List<GameObject> allMissionLogs = new List<GameObject>();

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

        for (int i = 0; i < oxygenBubbles.Count; i++)
        {
            oxygenBubbles[i].SetActive(true);
        }

        unsavedMissionLogs.Clear();
        inventory.InitInventory();
        for (int j = 0; j < GameState.GetInstance().savedMissionLogs.Count; j++)
        {
            GameObject missionLog = GameState.GetInstance().savedMissionLogs[j];
            missionLog.SetActive(false);
            inventory.CollectedMissionLog(missionLog.GetComponent<MissionLog>().missionLogNumber);
        }

        if (GameState.GetInstance().hasMiles)
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
        oxygenState.ResetOxygenToValue(oxygenReloadValue);
        GameState.GetInstance().gamePaused = false;
        unsavedOxygenContainers = 0;
        oxygenStorage.UpdateNumber();
    }

    public static LevelManager GetInstance()
    {
        return Instance;
    }

    public void ResetProgressAndLoad()
    {
        for (int i = 0; i < oxygenBubbles.Count; i++)
        {
            oxygenBubbles[i].SetActive(true);
        }
        for (int i = 0; i < allMissionLogs.Count; i++)
        {
            allMissionLogs[i].SetActive(true);
        }
        GameState.GetInstance().savedMissionLogs.Clear();
        GameState.GetInstance().hasMiles = false;
        GameState.GetInstance().savedOxygenContainers = 0;
        GameState.GetInstance().gotOrchid = false;
        GameState.GetInstance().firstCollectMissionLog = true;
        GameState.GetInstance().firstCollectOxygen = true;
        GameState.GetInstance().lastCheckpoint = 0;
        GameState.GetInstance().savedRichmond = false;
        inventory.InitInventory();
        oxygenState.ResetOxygenToValue(100);
        oxygenState.SetFirstOxygenValues();
        currentPlayerHealth = totalPlayerHealth;
    }

    public void StartWayBack()
    {
        for (int i = 0; i < oxygenBubbles.Count; i++)
        {
            oxygenBubbles[i].SetActive(false);
        } //TODO: replace with accurate value
        //TODO: make creatures aggressive -> change their path pattern and make them deal damage to the player
        //TODO: change oxygen depleting speed
        //TODO: make Richmond follow the player
    }
}
