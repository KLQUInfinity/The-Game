using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour
{
    [SerializeField] private float aliveTime;

    private void Start()
    {
        Destroy(gameObject, aliveTime);
    }
}
