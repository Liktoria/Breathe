using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShipDialogue : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Credits creditsScript;
    private bool savedRichmond;
    private bool dialogueTriggered;

    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueEnded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GameState.GetInstance().hasMiles)
        {
            //cameraController.DialogueTriggerReached(this.transform);//lerp camera
            //Lerp player position
            if (player.GetComponent<OxygenState>().extraOxygenContainers >= GameState.GetInstance().minExtraOxygenToSaveRichmond)
            {
                DialogueManager.GetInstance().StartDialogue("Final Scene - V1 Richmond Alive");
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/Final Scene V1", GetComponent<Transform>().position);
                dialogueTriggered = true;
            }
            else
            {
                DialogueManager.GetInstance().StartDialogue("Final Scene - V2 Richmond Dead");
                FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/Final Scene V2", GetComponent<Transform>().position);
                dialogueTriggered = true;
            }

            GameState.GetInstance().gamePaused = true;
        }
    }

    private void DialogueEnded()
    {
        if(dialogueTriggered)
        {
            dialogueTriggered = false;
            GameState.GetInstance().gamePaused = true;
            creditsScript.GetComponent<UIManager>().ShowMenu(mainMenu);
            creditsScript.StartCredits();
        }        
        //TODO: Open main menu, reset progress, change button to "play again"
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueEnded;
    }
}
