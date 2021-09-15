using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRichmondDialogue : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject milesPopup;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject saveRichmondPopup;
    private bool savedRichmond;
    private bool dialogueTriggered;
    private bool popUpTriggered;

    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueEnded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !popUpTriggered)
        {
            popUpTriggered = true;
            //cameraController.DialogueTriggerReached(this.transform); //lerp camera
            //Lerp player position            
            GameState.GetInstance().gamePaused = true;
            saveRichmondPopup.SetActive(true);
        }
    }

    public void TriggerFirstDialogue()
    {
        saveRichmondPopup.SetActive(false);
        GameState.GetInstance().gamePaused = true;
        DialogueManager.GetInstance().lastDialogue = false;
        if (GameState.GetInstance().savedOxygenContainers + LevelManager.GetInstance().unsavedOxygenContainers >= GameState.GetInstance().minExtraOxygenToSaveRichmond)
        {
            dialogueTriggered = true;
            GameState.GetInstance().savedRichmond = true;
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT1");
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/R finds R V1 pt 1", GetComponent<Transform>().position);
        }
        else
        {
            dialogueTriggered = true;
            GameState.GetInstance().savedRichmond = false;
            DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT1");
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/R finds R V2 pt 1", GetComponent<Transform>().position);
        }        
    }

    private void DialogueEnded()
    {
        if (dialogueTriggered)
        {
            GameState.GetInstance().gamePaused = true;
            milesPopup.SetActive(true);            
            dialogueTriggered = false;
        }        
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueEnded;
    }
}
