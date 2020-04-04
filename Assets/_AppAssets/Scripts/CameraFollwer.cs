using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollwer : MonoBehaviour
{
    public static CameraFollwer instance;

    [SerializeField] private Transform player;

    private Vector3 offset;

    public bool followPlayer;

    private void Awake()
    {
        instance = this;
    }

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
