using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Image> missionLogImages = new List<Image>();
    [SerializeField] private List<Button> missionLogButtons = new List<Button>();
    [SerializeField] private Image miles;
    [SerializeField] private Image orchid;
    [SerializeField] private GameObject inventoryPrefab;
    private List<int> collectedMissionLogs = new List<int>();
    private bool milesActivated = false;
    private bool orchidActivated = false;

    private void Start()
    {
        InitInventory();
    }

    public void InitInventory()
    {
        collectedMissionLogs.Clear();
        milesActivated = false;
        orchidActivated = false;
        
    }

    public void CollectedMissionLog(int number)
    {
        collectedMissionLogs.Add(number);
    }

    public void AcquiredMiles()
    {
        miles.color = new Color(miles.color.r, miles.color.g, miles.color.b, 1);
    }

    public void AcquiredOrchid()
    {
        orchid.color = new Color(orchid.color.r, orchid.color.g, orchid.color.b, 1);
    }

    public void OpenInventory()
    {
        inventoryPrefab.SetActive(true);

        miles.color = new Color(miles.color.r, miles.color.g, miles.color.b, 0.2f);
        orchid.color = new Color(orchid.color.r, orchid.color.g, orchid.color.b, 0.2f);

        foreach (Button missionLogButton in missionLogButtons)
        {
            missionLogButton.enabled = false;
        }

        foreach(Image missionLogImage in missionLogImages)
        {
            missionLogImage.color = new Color(missionLogImage.color.r, missionLogImage.color.g, missionLogImage.color.b, 0.2f);
        }

        for (int i = 0; i < collectedMissionLogs.Count; i++)
        {
            int missionLogNumber = collectedMissionLogs[i] - 1;
            missionLogImages[missionLogNumber].color = new Color(missionLogImages[missionLogNumber].color.r, missionLogImages[missionLogNumber].color.g, missionLogImages[missionLogNumber].color.b, 1);
            missionLogButtons[missionLogNumber].enabled = true;
        }
        
        if(milesActivated)
        {
            miles.color = new Color(miles.color.r, miles.color.g, miles.color.b, 1);
        }      
        
        if(orchidActivated)
        {
            orchid.color = new Color(orchid.color.r, orchid.color.g, orchid.color.b, 1);
        }
    }

    public void CloseInventory()
    {
        inventoryPrefab.SetActive(false);
        GameState.GetInstance().gamePaused = false;
    }
}
