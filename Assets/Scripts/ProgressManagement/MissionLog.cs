using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLog : MonoBehaviour
{
    [SerializeField] public int missionLogNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            LevelManager.GetInstance().AddMissionLog(this.gameObject);            
            if (GameState.GetInstance().firstCollectMissionLog)
            {
                PopUpManager.GetInstance().ShowMissionLogPopup(missionLogNumber);
            }
            else
            {
                DialogueManager.GetInstance().StartDialogue("Mission Log " + missionLogNumber);
                GameState.GetInstance().gamePaused = false;
            }
            //TODO: AUDIO beginning mission log sound
            //TODO: AUDIO Play VO for mission log with number missionLogNumber
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Mission Logs/ML" + missionLogNumber, GetComponent<Transform>().position);
        }
    }
}
