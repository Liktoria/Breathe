using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilesPopup : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    private bool dialogueTriggered;

    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueDone;
    }
    public void StartNextDialogue()
    {
        if(GameState.GetInstance().savedRichmond)
        {
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT2");
            //TODO: AUDIO Start VO "Roanoke Finds Richmond - V1 Richmond Lives - PT2"
            dialogueTriggered = true;
        }
        else
        {
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT2");
            //TODO: AUDIO Start VO "Roanoke Finds Richmond - V2 Richmond Dies - PT2"
            dialogueTriggered = true;
        }
    }

    private void DialogueDone()
    {
        if(dialogueTriggered)
        {
            cameraController.GetBackToPlayer();
            dialogueTriggered = false;
        }       
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueDone;
    }
}
