using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollwer : MonoBehaviour
{
    [SerializeField] private Transform player;

    public float lerpSpeed;

    private Vector3 offset;

    private bool followPlayer;

    private Vector3 vel;

    private void Start()
    {
        SetOffset();
    }

    private void SetOffset()
    {
        if (player)
        {
            offset = transform.position - player.position;
            followPlayer = true;
        }
    }

    private void LateUpdate()
    {
        if (player && followPlayer)
        {
            //transform.position = player.position + offset;
            transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
            //transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref vel , Time.deltaTime * lerpSpeed);
        }
    }
}
