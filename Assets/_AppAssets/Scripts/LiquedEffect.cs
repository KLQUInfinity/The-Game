using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquedEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().Die();
        }
    }
}
