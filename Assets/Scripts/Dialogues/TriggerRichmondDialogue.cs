using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRichmondDialogue : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject milesPopup;
    [SerializeField] private CameraController cameraController;
    private bool savedRichmond;
    private bool dialogueTriggered;

    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueEnded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraController.DialogueTriggerReached(this.transform); //lerp camera
            //Lerp player position
            if (player.GetComponent<OxygenState>().extraOxygenContainers >= GameState.GetInstance().minExtraOxygenToSaveRichmond)
            {
                GameState.GetInstance().savedRichmond = true;
                DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT1");
                //TODO: AUDIO Start VO "Roanoke Finds Richmond - V1 Richmond Lives - PT1"
                dialogueTriggered = true;
            }
            else
            {
                GameState.GetInstance().savedRichmond = false;
                DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT1");
                dialogueTriggered = true;
                //TODO: AUDIO Start VO "Roanoke Finds Richmond - V2 Richmond Dies - PT1"
            }
            GameState.GetInstance().gamePaused = true;
        }
    }

    private void DialogueEnded()
    {
        if (dialogueTriggered)
        {
            milesPopup.SetActive(true);
            //TODO: AUDIO pop up sound (if exsiting)
            dialogueTriggered = false;
        }        
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueEnded;
    }
}
