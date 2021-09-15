using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int checkpointIndex;
    private bool hasSaved = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.GetInstance().SaveProgress(checkpointIndex);
        Debug.Log("Progress saved!");
    }
}
