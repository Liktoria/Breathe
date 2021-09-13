using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private string dialogueToTrigger;
    [SerializeField] private GameObject player;
    private bool savedRichmond;
    private bool dialogueTriggered;

    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueEnded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Lerp Camera
        *Lerp player position
        *if(enough oxygen)
        *{
        *   DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT1");
        *}
        *else
        *{
        *   DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT1");        *   
        *}
        *
        */

        DialogueManager.GetInstance().StartDialogue(dialogueToTrigger);
        GameState.GetInstance().gamePaused = true;
    }

    private void DialogueEnded()
    {
        /*display Miles pop up
         * 
        *In Miles pop up:
        *On Clicked away pop up: 
        *GameState.hasMiles = true;
        *if(savedRichmond)
        *{
        *   DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V1 Richmond Lives - PT2");
        *}
        *else
        *{
        *   DialogueManager.GetInstance().StartDialogue("Roanoke Finds Richmond - V2 Richmond Dies - PT2");
        *}
        */
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= DialogueEnded;
    }
}
