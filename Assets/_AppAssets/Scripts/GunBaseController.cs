using UnityEngine;

public class GunBaseController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {

        }
    }
}
