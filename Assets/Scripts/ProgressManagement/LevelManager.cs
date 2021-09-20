using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] private List<GameObject> allEnemies = new List<GameObject>();
    [SerializeField] private Menu blackMenu;

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
        for(int i = 0; i < allEnemies.Count; i++)
        {
            allEnemies[i].SetActive(true);
            allEnemies[i].GetComponentInChildren<Enemy>().ResetEnemy();
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

    public void ContinueGame()
    {
        LoadProgress();
        GameState.GetInstance().gamePaused = false;
    }

    public void ResetProgressAndLoad()
    {
        //reset oxygen bubbles, mission logs & enemies
        for (int i = 0; i < oxygenBubbles.Count; i++)
        {
            oxygenBubbles[i].SetActive(true);
        }
        for (int i = 0; i < allMissionLogs.Count; i++)
        {
            allMissionLogs[i].SetActive(true);
        }
        for(int i = 0; i < allEnemies.Count; i++)
        {
            allEnemies[i].GetComponentInChildren<Enemy>().DeactivateEnemy();
        }

        //Reset game state
        GameState.GetInstance().savedMissionLogs.Clear();
        GameState.GetInstance().hasMiles = false;
        GameState.GetInstance().savedOxygenContainers = 0;
        GameState.GetInstance().gotOrchid = false;
        GameState.GetInstance().firstCollectMissionLog = true;
        GameState.GetInstance().firstCollectOxygen = true;
        GameState.GetInstance().lastCheckpoint = 0;
        GameState.GetInstance().savedRichmond = false;

        //Reset inventory
        inventory.InitInventory();

        //Reset oxygen values
        oxygenState.ResetOxygenToValue(100);
        oxygenState.SetFirstOxygenValues();

        //Reset player
        currentPlayerHealth = totalPlayerHealth;
        player.transform.position = GameState.GetInstance().checkpointPositions[GameState.GetInstance().lastCheckpoint];

        //Reset intro menu
        blackMenu.gameObject.SetActive(true);
        foreach (Image fadeImage in blackMenu.fadeImages)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        }
        foreach (TMP_Text fadeText in blackMenu.fadeTexts)
        {
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 1);
        }
    }

    public void StartWayBack()
    {
        for (int i = 0; i < oxygenBubbles.Count; i++)
        {
            oxygenBubbles[i].SetActive(false);
        } 
    }
}
