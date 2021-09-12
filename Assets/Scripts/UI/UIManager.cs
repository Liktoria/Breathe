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
    private float alpha;
    private TMP_Text[] fadeTexts;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !GameState.GetInstance().gamePaused)
        {
            ShowMenu(pauseMenu);
        }
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
        StartCoroutine(fadeFunction(1f, 0f, fadeDuration));
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

            foreach(Image fadeImage in fadeImages)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            }

            foreach(TMP_Text text in fadeTexts)
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
    }

    IEnumerator fadeSwitchFunction(float startValue, float endValue, float duration, GameObject newMenu)
    {
        float time = 0;
        alpha = startValue;
        GameState.GetInstance().gamePaused = true;

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
        StartCoroutine(fadeFunction(0, 1, fadeDuration));
    }
}
