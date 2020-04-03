using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableItem : MonoBehaviour
{
    private float distance;

    private void Start()
    {
        distance = Camera.main.transform.position.z;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos();
    }

    private void OnMouseUp()
    {

    }

    private Vector2 GetMousePos()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePos;
    }
}
