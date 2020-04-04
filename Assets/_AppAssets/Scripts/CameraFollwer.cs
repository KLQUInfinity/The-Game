using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollwer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;

    private void Start()
    {
        if (player)
        {
            offset = transform.position - player.position;
        }
    }

    private void LateUpdate()
    {
        if (player)
        {
            transform.position = player.position + offset;
        }
    }
}
