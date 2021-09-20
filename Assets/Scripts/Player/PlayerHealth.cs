using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TMP_Text deathQuoteTextField;
    [SerializeField] private string[] allDeathQuotes;
    [SerializeField] private SpriteRenderer minusOne;
    private List<string> currentDeathQuotes = new List<string>();

    // Start is called before the first frame update
    private void Start()
    {
        foreach(string quote in allDeathQuotes)
        {
            currentDeathQuotes.Add(quote);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && GameState.GetInstance().hasMiles)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player Character/Damage", GetComponent<Transform>().position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Roanoke Barks/Takes Damage Emote", GetComponent<Transform>().position);
            TakeHit();
        }
    }

    public void TakeHit()
    {
        if (LevelManager.GetInstance().currentPlayerHealth > 1)
        {
            LevelManager.GetInstance().currentPlayerHealth--;
            ShowText();
            Debug.Log("Got hit! " + LevelManager.GetInstance().currentPlayerHealth + "health left");
        }
        else
        {
            //die
            LevelManager.GetInstance().currentPlayerHealth = 0;
            ShowText();
            GameState.GetInstance().gamePaused = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/VO/Roanoke Barks/Death Emote", GetComponent<Transform>().position);
            if (currentDeathQuotes.Count <= 0)
            {
                foreach (string quote in allDeathQuotes)
                {
                    currentDeathQuotes.Add(quote);
                }
            }
            else
            {
                int randomIndex = Random.Range(0, currentDeathQuotes.Count);
                deathQuoteTextField.text = currentDeathQuotes[randomIndex];
                deathQuoteTextField.text = deathQuoteTextField.text.Replace("\\n", "\n");
                currentDeathQuotes.RemoveAt(randomIndex);
            }

            foreach (Image fadeImage in deathPanel.GetComponent<Menu>().fadeImages)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0.8f);
            }

            foreach (TMP_Text fadeText in deathPanel.GetComponent<Menu>().fadeTexts)
            {
                fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, 1);
            }
            deathPanel.SetActive(true);
        }
    }

    public void ShowText()
    {
        minusOne.color = new Color(minusOne.color.r, minusOne.color.g, minusOne.color.b, 1);
        StartCoroutine(WaitToHide(minusOne));
    }

    IEnumerator WaitToHide(SpriteRenderer minusOne)
    {
        yield return new WaitForSeconds(1.5f);
        minusOne.color = new Color(minusOne.color.r, minusOne.color.g, minusOne.color.b, 0);
    }
}
