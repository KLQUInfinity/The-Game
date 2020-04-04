using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public bool waitingForDeath = false;
    public GameObject player;
    public GameObject chest;

    private void OnEnable()
    {
        DialogueManager.instance.DialogueFinished.AddListener(HandleDialogueFinished);
        player.GetComponent<PlayerController>().playerDied.AddListener(HandlePlayerDeath);
    }

    public void HandlePlayerDeath()
    {
        if (waitingForDeath)
        {
            waitingForDeath = false;
            DialogueManager.instance.StartDialogue(DialogueManager.instance.NextDialogueIndex);
        }
    }

    public void HandleDialogueFinished(int i)
    {
        switch (i)
        {
            case 4:
                chest.GetComponent<ChestController>().ToggleChest(true);
                GameMaster.instance.StartLevel(3);
                break;
        }
    }
}
