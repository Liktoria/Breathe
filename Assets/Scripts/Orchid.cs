using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchid : MonoBehaviour
{
    private bool collectedOrchid;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !collectedOrchid)
        {
            PopUpManager.GetInstance().ShowOrchidPopup();
            collectedOrchid = true;
        }
    }
}
