using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseCliff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameMaster.instance.levels[1].GetComponent<Level2>().waitingForDeath = true;
            collision.GetComponent<PlayerController>().Die();
        }
    }
}
