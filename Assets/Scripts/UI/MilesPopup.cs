using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilesPopup : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject richmondTrigger;
    [SerializeField] private Sprite deadRichmondSprite;
    [SerializeField] private Inventory inventory;
    private bool dialogueTriggered;

    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueDone;
    }
    public void StartNextDialogue()
    {
        GameState.GetInstance().gamePaused = true;
        inventory.AcquiredMiles();
        if (GameState.GetInstance().savedRichmond)
        {
            dialogueTriggered = true;
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT2");
            //TODO: AUDIO Start VO "Roanoke Finds Richmond - V1 Richmond Lives - PT2"            
        }
        else
        {
            dialogueTriggered = true;
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT2");
            //TODO: AUDIO Start VO "Roanoke Finds Richmond - V2 Richmond Dies - PT2"            
        }
    }

    private void DialogueDone()
    {
        if(dialogueTriggered)
        {
            GameState.GetInstance().hasMiles = true;
            if(GameState.GetInstance().savedRichmond)
            {
                richmondTrigger.SetActive(false);
            }
            else
            {
                richmondTrigger.GetComponent<SpriteRenderer>().sprite = deadRichmondSprite;
            }
            //change Richmond Sprite / start following
            cameraController.GetBackToPlayer();
            dialogueTriggered = false;
        }       
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueDone;
    }
}
