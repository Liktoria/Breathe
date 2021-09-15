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
    private List<string> currentDeathQuotes = new List<string>();

    // Start is called before the first frame update
    private void Start()
    {
        foreach(string quote in allDeathQuotes)
        {
            currentDeathQuotes.Add(quote);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && GameState.GetInstance().hasMiles)
        {
            //TODO: AUDIO Got hit by enemy sound
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
        }
        else
        {
            //die
            LevelManager.GetInstance().currentPlayerHealth = 0;
            GameState.GetInstance().gamePaused = true;
            //TODO: AUDIO Suffocated to death
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
}
