
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public void StartFirstCutscene()
    {
        GameState.GetInstance().gamePaused = true;
        DialogueManager.GetInstance().StartDialogue("Opening Scene");
    }

    public void StartEndlevelDialogue(string chosenDialogue)
    {
        GameState.GetInstance().gamePaused = true;
        DialogueManager.GetInstance().StartDialogue(chosenDialogue);
    }
}
