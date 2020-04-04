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
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
