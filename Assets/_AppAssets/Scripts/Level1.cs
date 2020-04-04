using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject player;
    public GameObject movementInstruction;
    public GameObject scriptPrefab;
    public Transform scriptSpawnLocation;

    private void OnEnable()
    {
        player.GetComponent<PlayerController>().enabled = false;
        movementInstruction.SetActive(false);
        if (DialogueManager.instance != null)
        {
            DialogueManager.instance.StartDialogue(0);
            DialogueManager.instance.DialogueFinished.AddListener(HandleDialogueFinished);
        }
    }

    private void Update()
    {
        
    }

    public void HandleDialogueFinished(int i)
    {
        switch (i)
        {
            case 0:
                movementInstruction.SetActive(true);
                StartCoroutine(StartDialogue1AfterDelay(3));
                break;
            case 1:
                GameObject go = Instantiate(scriptPrefab, scriptSpawnLocation.position, scriptSpawnLocation.rotation);
                //player.GetComponent<PlayerController>().enabled = true;
                break;
        }
    }

    public void EnablePlayerMovement()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    IEnumerator StartDialogue1AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DialogueManager.instance.StartDialogue(1);
    }
}
