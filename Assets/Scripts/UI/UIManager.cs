using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Image[] fadeImages;
    private GameObject menuParent;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Inventory inventory;
    private float alpha;
    private TMP_Text[] fadeTexts;
    private bool inventoryActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameState.GetInstance().gamePaused)
        {
            ShowMenu(pauseMenu);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (GameState.GetInstance().gamePaused && inventoryActive)
            {
                inventory.CloseInventory();
                inventoryActive = false;
                //TODO: AUDIO UI pop up noise (if existing)
            }
            else if(!GameState.GetInstance().gamePaused && !inventoryActive)
            {
                inventory.OpenInventory();
                inventoryActive = true;
                GameState.GetInstance().gamePaused = true;
                //TODO: AUDIO UI pop up noise (if existing)
            }            
        }      
    }

    public void SetInventoryActive(bool newValue)
    {
        inventoryActive = newValue;
    }

    public void ShowMenu(GameObject menuObject)
    {
        //Do some fading in action before enabling the menu
        menuParent = menuObject;
        fadeImages = menuObject.GetComponent<Menu>().fadeImages;
        fadeTexts = menuObject.GetComponent<Menu>().fadeTexts;
        StartCoroutine(fadeFunction(0f, 1f, fadeDuration));
    }

    public void HideMenu(GameObject menuObject)
    {
        //Do some fading out action before disabling the menu
        menuParent = menuObject;
        fadeImages = menuObject.GetComponent<Menu>().fadeImages;
        fadeTexts = menuObject.GetComponent<Menu>().fadeTexts;
        StartCoroutine(fadeFunction(1f, 0f, fadeDuration));
    }

    public void SwitchMenu(GameObject newMenu)
    {
        StartCoroutine(fadeSwitchFunction(1f, 0f, fadeDuration, newMenu));
    }

    IEnumerator fadeFunction(float startValue, float endValue, float duration)
    {
        float time = 0;
        alpha = startValue;
        if (endValue == 1)
        {
            menuParent.SetActive(true);
            GameState.GetInstance().gamePaused = true;
        }

        while (time < duration)
        {
            alpha = Mathf.Lerp(startValue, endValue, time / duration);

            foreach (Image fadeImage in fadeImages)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            }

            foreach (TMP_Text text in fadeTexts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (endValue == 0)
        {
            menuParent.SetActive(false);
            GameState.GetInstance().gamePaused = false;
        }
        if(menuParent.name == "Main Menu")
        {
            GameState.GetInstance().gamePaused = true;
        }
    }

    IEnumerator fadeSwitchFunction(float startValue, float endValue, float duration, GameObject newMenu)
    {
        float time = 0;
        alpha = startValue;
        GameState.GetInstance().gamePaused = true;
        newMenu.SetActive(true);
        foreach(Image fadeImage in newMenu.GetComponent<Menu>().fadeImages)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        }

        foreach (TMP_Text fadeText in newMenu.GetComponent<Menu>().fadeTexts)
        {
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 1);
        }


        while (time < duration)
        {
            alpha = Mathf.Lerp(startValue, endValue, time / duration);

            foreach (Image fadeImage in fadeImages)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            }

            foreach (TMP_Text text in fadeTexts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            }

            time += Time.deltaTime;
            yield return null;
        }
        menuParent.SetActive(false);

        menuParent = newMenu;
        fadeImages = newMenu.GetComponent<Menu>().fadeImages;
        fadeTexts = newMenu.GetComponent<Menu>().fadeTexts;
        //StartCoroutine(fadeFunction(0, 1, fadeDuration));
    }
}
