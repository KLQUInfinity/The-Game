using UnityEngine;

public class GunBaseController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.transform.GetChild(2).gameObject.SetActive(true);
            GameMaster.instance.levels[2].GetComponent<Level3>().waitingForDeath = true;
            Destroy(gameObject);
        }
    }
}
