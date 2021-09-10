using UnityEngine;
using System.Collections;


public class Quit : MonoBehaviour
{
   public void Exitgame ()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }
}
