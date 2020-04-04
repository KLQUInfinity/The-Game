using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public IntEvent DialogueFinished = new IntEvent();

    public static DialogueManager instance;

    public int NextDialogueIndex = 0;

    //public int testDialogueNumber;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject SkipButton;
    public Dialogue[] dialogues;
    public float delayBetweenSentences;
    public float soundDelay;
    private float currentDelay = 0;
    public float timeBetweenCharacters;
    [HideInInspector] public List<string> sentences;

    private bool isReadyToSkip = false;
    private bool alreadyPressed = false;

    private int currentDialogueIndex;

    public bool inDialogue;

    public Animator anim;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inDialogue = false;
        //StartDialogue(testDialogueNumber);
    }

    private void Update()
    {
        SkipButton.SetActive(isReadyToSkip);

        if (isReadyToSkip)
        {
            if (currentDelay > 0)
            {
                currentDelay -= Time.deltaTime;
            }
            else if (alreadyPressed)
            {
                alreadyPressed = false;
                currentDelay = 0;
                return;
            }
            else
            {
                currentDelay = 0;
                DisplayNextSentence();
            }
        }

        if (anim.GetBool("IsOpen"))
        {
            if (isReadyToSkip)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    alreadyPressed = true;
                    DisplayNextSentence();
                }
            }
        }
    }

    public void StartDialogue(int index)
    {
        anim.SetBool("IsOpen", true);
        inDialogue = true;
        currentDialogueIndex = index;
        nameText.text = dialogues[index].name;

        foreach (string sentence in dialogues[index].sentences)
        {
            sentences.Insert(0, sentence);
        }

        StartCoroutine(TypeSound(soundDelay));
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        isReadyToSkip = false;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences[sentences.Count - 1];
        StopAllCoroutines();
        StartCoroutine(TypeSound(soundDelay));
        StartCoroutine(TypeSentence(sentence));
        sentences.RemoveAt(sentences.Count - 1);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                break;
            }
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        isReadyToSkip = true;
        currentDelay = delayBetweenSentences;
        //Invoke("DisplayNextSentence", delayBetweenSentences);
    }

    IEnumerator TypeSound(float delay)
    {
        while (!isReadyToSkip)
        {
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            yield return new WaitForSeconds(delay);
        }
    }

    void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
        inDialogue = false;
        DialogueFinished.Invoke(currentDialogueIndex);
        NextDialogueIndex++;
        //MyGameManager.justEndedConversation = true;
        //MyGameManager.inConversation = false;
    }

    public void SkipSentence()
    {
        if (isReadyToSkip)
        {
            alreadyPressed = true;
            DisplayNextSentence();
        }
    }
}

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}

public class IntEvent : UnityEvent<int>
{

}

