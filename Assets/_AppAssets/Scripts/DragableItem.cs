using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableItem : MonoBehaviour
{
    private float distance;

    public string target;

    private void Start()
    {
        distance = Camera.main.transform.position.z;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos();
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnMouseUp()
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

    private Vector2 GetMousePos()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameMaster.instance.levels[0].GetComponent<Level1>().EnablePlayerMovement();
            Destroy(gameObject);
        }

        if(other.tag == "Gun")
        {
            GameMaster.instance.levels[2].GetComponent<Level3>().player.GetComponent<PlayerController>().canShoot = true;
        }
    }
}
