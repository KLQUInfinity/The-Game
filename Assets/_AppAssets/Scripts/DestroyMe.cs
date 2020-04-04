using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour
{
    [SerializeField] private float aliveTime;

    private void Start()
    {
        Destroy(gameObject, aliveTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<EnemyController>().type == EnemyType.Type1)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {
                DialogueManager.instance.StartDialogue(6);
            }
        }
    }
}
