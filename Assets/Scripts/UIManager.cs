using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ShowMenu(GameObject menuObject)
    {
        //Do some fading in action before enabling the menu
        menuObject.SetActive(true);
    }

    public void HideMenu(GameObject menuObject)
    {
        //Do some fading out action before disabling the menu
        menuObject.SetActive(false);
    }
}
