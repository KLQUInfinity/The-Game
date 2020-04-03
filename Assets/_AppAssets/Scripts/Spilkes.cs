using UnityEngine;

public class Spilkes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            collision.transform.GetComponentInParent<PlayerController>().Die();
        }
    }
}
