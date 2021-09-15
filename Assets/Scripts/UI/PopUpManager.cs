using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject oxygenPopup;
    [SerializeField] GameObject missionLogPopup;
    [SerializeField] GameObject orchidPopup;
    [SerializeField] GameObject orchid;
    [SerializeField] Light light;
    [SerializeField] Sprite newOrchidSprite;
    [SerializeField] OxygenState oxygenState;
    [SerializeField] Inventory inventory;
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

    public void ShowOrchidPopup()
    {
        orchidPopup.SetActive(true);
        GameState.GetInstance().gamePaused = true;
    }

    public void HideOrchidPopupWhole()
    {
        oxygenState.SetFinalOxygenValues();
        orchidPopup.SetActive(false);
        GameState.GetInstance().gamePaused = false;
        orchid.SetActive(false);
        light.color = new Color(0.9811321f, 0.5590602f, 0.5590602f, 1);
        inventory.AcquiredOrchid();
        //make enemies aggressive
    }

    public void HideOrchidPopupPart()
    {
        oxygenState.SetFinalOxygenValues();
        oxygenState.ResetOxygenToValue(100);
        orchidPopup.SetActive(false);
        GameState.GetInstance().gamePaused = false;
        light.color = new Color(0.9811321f, 0.5590602f, 0.5590602f, 1);
        orchid.GetComponent<SpriteRenderer>().sprite = newOrchidSprite;
        inventory.AcquiredOrchid();
        //make enemies aggressive
    }
}
