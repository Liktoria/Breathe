using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OxygenStorage : MonoBehaviour
{
    [SerializeField] TMP_Text numberDisplayed;
    [System.NonSerialized] public int currentOxygenExtras;
    private void Start()
    {
        numberDisplayed.text = "0";
    }
    public void IncreaseNumber()
    {
        currentOxygenExtras++;
        numberDisplayed.text = currentOxygenExtras.ToString();
        LevelManager.GetInstance().unsavedOxygenContainers++;
    }

    public void UpdateNumber()
    {
        currentOxygenExtras = GameState.GetInstance().savedOxygenContainers;
        numberDisplayed.text = currentOxygenExtras.ToString();
    }
}
