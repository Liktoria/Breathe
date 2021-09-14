
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public void StartFirstCutscene()
    {
        GameState.GetInstance().gamePaused = true;
        DialogueManager.GetInstance().StartDialogue("Opening Scene");
        //TODO: AUDIO Start VO "Opening Scene"
    }
}
