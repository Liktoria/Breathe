
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject blackMenu;
    private float alpha;
    private void Start()
    {
        DialogueManager.OnDialogueEnded += DialogueEnded;
    }
    public void StartFirstCutscene()
    {
        GameState.GetInstance().gamePaused = true;
        DialogueManager.GetInstance().StartDialogue("Opening Scene");
        FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Scenes/Opening Scene", GetComponent<Transform>().position);
    }
    private void DialogueEnded()
    {
        //StartCoroutine(fadeOutBlack(1, 0, 1.5f));
        GetComponent<UIManager>().HideMenu(blackMenu);
    }

    /*IEnumerator fadeOutBlack(float startValue, float endValue, float duration)
    {
        float time = 0;
        alpha = startValue;

        while (time < duration)
        {
            alpha = Mathf.Lerp(startValue, endValue, time / duration);
            blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }
        blackImage.gameObject.SetActive(false);
        GameState.GetInstance().gamePaused = false;
    }*/
}
