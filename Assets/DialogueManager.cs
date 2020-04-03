using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject SkipButton;
    public Dialogue[] dialogues;
    public float delayBetweenSentences;
    private float currentDelay = 0;
    public float timeBetweenCharacters;
    [HideInInspector] public List<string> sentences;

    private bool isReadyToSkip = false;
    private bool alreadyPressed = false;

    public Animator anim;

    private void Start()
    {
        StartDialogue(0);
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
                if (Input.GetKeyDown(KeyCode.Return))
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
        nameText.text = dialogues[index].name;

        foreach (string sentence in dialogues[index].sentences)
        {
            sentences.Insert(0, sentence);
        }

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
        StartCoroutine(TypeSentence(sentence));
        sentences.RemoveAt(sentences.Count - 1);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }

        isReadyToSkip = true;
        currentDelay = delayBetweenSentences;
        //Invoke("DisplayNextSentence", delayBetweenSentences);
    }

    void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
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
