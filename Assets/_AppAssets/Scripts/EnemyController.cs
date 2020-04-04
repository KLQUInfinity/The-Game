using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyType type;
    [SerializeField] private Vector3[] points;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Color alertColor;

    private bool moving;
    private int currentPatrolIndex;
    private Color oldColor;
    private SpriteRenderer SpriteRenderer;

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
        if (moving)
        {
            MoveBetweenPoints();
        }
    }

    private void MoveBetweenPoints()
    {
        if (transform.position == points[currentPatrolIndex])
        {
            currentPatrolIndex++;
            currentPatrolIndex = currentPatrolIndex % points.Length;
        }
        transform.position = Vector3.MoveTowards(transform.position, points[currentPatrolIndex], moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SpriteRenderer.color = alertColor;
            moving = false;
        }
        else if (collision.tag.Equals("Bullet"))
        {
            if (type == EnemyType.Type1)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            else
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SpriteRenderer.color = oldColor;
            moving = true;
        }
    }
}

public enum EnemyType
{
    Type1,
    Type2
}
