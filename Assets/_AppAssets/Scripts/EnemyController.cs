using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType type;
    [SerializeField] private Vector3[] points;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Color alertColor;

    private bool moving;
    private int currentPatrolIndex;
    private Color oldColor;
    private SpriteRenderer SpriteRenderer;
    private bool followPlayer = false;
    private Transform player;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        moving = true;
        oldColor = SpriteRenderer.color;
        currentPatrolIndex = 0;
        transform.position = points[currentPatrolIndex];
    }

    private void Update()
    {
        //if (moving)
        //{
            MoveBetweenPoints();
        //}
    }

    private void MoveBetweenPoints()
    {
        if (followPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        if (transform.position == points[currentPatrolIndex])
        {
            currentPatrolIndex++;
            currentPatrolIndex = currentPatrolIndex % points.Length;
        }
        transform.position = Vector3.MoveTowards(transform.position, points[currentPatrolIndex], moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Die();
            followPlayer = false;
        }

        //if(collision.gameObject.tag == "Bullet")
        //{
        //    Destroy(collision.gameObject);
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SpriteRenderer.color = alertColor;
            moving = false;
            followPlayer = true;
            player = collision.transform;
        }
        //else if (collision.tag.Equals("Bullet"))
        //{
        //    if (type == EnemyType.Type1)
        //    {
        //        Destroy(collision.gameObject);
        //        Destroy(gameObject);
        //    }
        //    else
        //    {

        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SpriteRenderer.color = oldColor;
            moving = true;
            followPlayer = false;
        }
    }
}

public enum EnemyType
{
    Type1,
    Type2
}
