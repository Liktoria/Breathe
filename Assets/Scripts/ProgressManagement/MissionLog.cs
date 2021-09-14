using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLog : MonoBehaviour
{
    [SerializeField] private int missionLogNumber;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            LevelManager.GetInstance().AddMissionLog(this.gameObject);
            DialogueManager.GetInstance().StartDialogue("Mission Log " + missionLogNumber);
            //TODO: AUDIO Play VO for mission log with number missionLogNumber
        }
    }
}
