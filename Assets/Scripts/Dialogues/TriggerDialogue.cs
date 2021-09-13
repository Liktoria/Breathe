using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] private string dialogueToTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.GetInstance().StartDialogue(dialogueToTrigger);
        GameState.GetInstance().gamePaused = true;
    }
}
