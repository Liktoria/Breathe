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
        DialogueManager.GetInstance().lastDialogue = true;
        if (GameState.GetInstance().savedRichmond)
        {
            dialogueTriggered = true;
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT2");     
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/R finds R V1 pt 2", GetComponent<Transform>().position);  
        }
        else
        {
            dialogueTriggered = true;
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT2");  
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/R finds R V2 pt 2", GetComponent<Transform>().position);       
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
            //cameraController.GetBackToPlayer();
            dialogueTriggered = false;
        }       
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueDone;
    }
}
