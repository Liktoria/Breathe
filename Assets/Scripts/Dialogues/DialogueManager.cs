using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    // Singleton
    private static DialogueManager instance;

    //private InteractionManager interactionManager;

    //private List<Interactable> subscribers = new List<Interactable>();

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    [SerializeField]
    private float timeBetweenCharacters = 0.1f;
    private float timer = 0.0f;
    private int characterIndex = 0;
    private bool isDoneShowingLine = true;
    public delegate void DialogueEndedAction();
    public static event DialogueEndedAction OnDialogueEnded;
    [System.NonSerialized] public bool lastDialogue = true;

    private bool IsDoneShowingLine
    {
        get { return isDoneShowingLine; }
        set
        {
            isDoneShowingLine = value;
            continueArrow.SetActive(value);
        }
    }

    // All fields for controlling the UI
    [SerializeField]
    private GameObject dialogueBox;
    private TMP_Text speaker;
    private TMP_Text content;
    private GameObject background;
    private GameObject continueArrow;
    private Image leftPerson;
    private Image rightPerson;

    // File with the Dialogues
    [SerializeField]
    private TextAsset jsonDialogues;
    [SerializeField]
    private Person[] persons;

    private Dialogue[] dialogues;
    public Dialogue currentDialogue;
    private int currentLine;
    private int currentSide;

    private DialogueManager() { }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        dialogueBox.SetActive(false);
        dialogues = JsonUtility.FromJson<Dialogues>(jsonDialogues.text).dialogues;


        speaker = dialogueBox.transform.Find("Name").GetComponent<TMP_Text>();
        content = dialogueBox.transform.Find("Content").GetComponent<TMP_Text>();
        background = dialogueBox.transform.Find("Background").gameObject;
        continueArrow = dialogueBox.transform.Find("ContinueArrow").gameObject;
        leftPerson = dialogueBox.transform.Find("LeftPerson").gameObject.GetComponent<Image>();
        rightPerson = dialogueBox.transform.Find("RightPerson").gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox.activeSelf && !IsDoneShowingLine)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenCharacters)
            {
                string textToDisplay = currentDialogue.lines[currentLine].text;
                characterIndex++;
                if (characterIndex < textToDisplay.Length + 1)
                {
                    content.text = textToDisplay.Substring(0, characterIndex);
                } else {
                    characterIndex = 0;
                    IsDoneShowingLine = true;
                }
                timer = 0.0f;
            }
        }
    }

    public void StartDialogue(string dialogueName)
    {
        GameState.GetInstance().gamePaused = true;
        Dialogue[] filteredDialogues = dialogues.Where(d => d.name == dialogueName).ToArray();

        if (filteredDialogues.Length > 0)
        {
            dialogueBox.SetActive(true);
            currentDialogue = filteredDialogues[0];
            currentLine = 0;
            ChangeLine(currentDialogue.lines[0]);
        }
    }

    public void NextLine()
    {
        if(!IsDoneShowingLine)
        {
            IsDoneShowingLine = true;
            characterIndex = 0;
            content.text = currentDialogue.lines[currentLine].text;
        }
        else if(dialogueBox.activeSelf)
        {
            currentLine += 1;
            //AUDIO: play corresponding VO file
            if(currentDialogue.lines.Length > currentLine)
            {
                ChangeLine(currentDialogue.lines[currentLine]);
            } else {
                currentDialogue = null;
                currentLine = 0;
                dialogueBox.SetActive(false);
                characterIndex = 0;
                IsDoneShowingLine = true;
                if(lastDialogue)
                {
                    GameState.GetInstance().gamePaused = false;
                }                
                OnDialogueEnded?.Invoke();
                //Dialogue ended
            }
        }
    }

    private void ChangeLine(DialogueLine line)
    {
        if (line.side == 0)
        {
            this.currentSide = this.currentSide == 1 ? 2 : 1;
        } else
        {
            this.currentSide = line.side;
        }
        background.SetActive(true);
        DisplayCharacterFor(line.name, side: this.currentSide);
        speaker.text = line.name;
        content.text = "";

        IsDoneShowingLine = false;
    }

    private void DisplayCharacterFor(string name, int side)
    {
        Person[] filteredPersons = persons.Where(person => person.personName == name).ToArray();
        if(filteredPersons.Length > 0) { 
            ShowPerson(filteredPersons[0], side);
        }
        else
        {
            leftPerson.gameObject.SetActive(false);
            rightPerson.gameObject.SetActive(false);
        }
    }

    private void ShowPerson(Person person, int side)
    {
        leftPerson.gameObject.SetActive(side == 1);
        rightPerson.gameObject.SetActive(side == 2);
        if (side == 1)
        {
            leftPerson.sprite = person.personImage;

        } else
        {
            rightPerson.sprite = person.personImage;
        }
    }
}