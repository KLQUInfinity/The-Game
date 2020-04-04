using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject player;
    public GameObject trigger;
    public GameObject nextCliff;
    public GameObject jumpInst1;
    public GameObject jumpInst2;
    public bool waitingForDeath = false;

    private void OnEnable()
    {
        DialogueManager.instance.DialogueFinished.AddListener(HandleDialogueFinished);
        player.GetComponent<PlayerController>().playerDied.AddListener(HandlePlayerDeath);
        jumpInst1.SetActive(true);
        jumpInst2.SetActive(false);
    }

    public void HandleDialogueFinished(int i)
    {
        switch (i)
        {
            case 2:
                ChangeJumpButton();
                trigger.GetComponent<JumpTrigger>().enabled = false;
                trigger.GetComponent<Collider2D>().enabled = false;
                jumpInst2.SetActive(true);
                jumpInst1.SetActive(false);
                break;
            case 3:
                nextCliff.GetComponent<Collider2D>().isTrigger = false;
                nextCliff.GetComponent<Collider2D>().offset = new Vector2(0, 0);
                nextCliff.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
                nextCliff.GetComponent<FalseCliff>().enabled = false;
                GameMaster.instance.StartLevel(2);
                break;
        }
    }

    public void HandlePlayerDeath()
    {
        if (waitingForDeath)
        {
            waitingForDeath = false;
            DialogueManager.instance.StartDialogue(DialogueManager.instance.NextDialogueIndex);
        }
    }


    public void ChangeJumpButton()
    {
        player.GetComponent<PlayerController>().jumpButton = KeyCode.X;
    }
}
