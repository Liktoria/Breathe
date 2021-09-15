using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject oxygenPopup;
    [SerializeField] GameObject missionLogPopup;
    private static PopUpManager instance;
    private int missionLogNumber;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void ShowOxygenPopup()
    {
        oxygenPopup.SetActive(true);
        GameState.GetInstance().gamePaused = true;
        GameState.GetInstance().firstCollectOxygen = false;
    }

    public void HideOxygenPopup()
    {
        oxygenPopup.SetActive(false);
        GameState.GetInstance().gamePaused = false;
    }

    public void ShowMissionLogPopup(int firstMissionLogNumber)
    {
        missionLogPopup.SetActive(true);
        GameState.GetInstance().gamePaused = true;
        missionLogNumber = firstMissionLogNumber;
        GameState.GetInstance().firstCollectMissionLog = false;
    }

    public void HideMissionLogPopup()
    {
        missionLogPopup.SetActive(false);
        GameState.GetInstance().gamePaused = false;
        DialogueManager.GetInstance().StartDialogue("Mission Log " + missionLogNumber);
    }

    public static PopUpManager GetInstance()
    {
        return instance;
    }
}
