using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float shootSpeed;

    private Rigidbody2D myRB;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        if (transform.localRotation.z > 0)
        {
            myRB.velocity = new Vector2(-1 * shootSpeed, 0);
        }
        else
        {
            myRB.velocity = new Vector2(1 * shootSpeed, 0);
        }
    }

    public void removeForce()
    {
        myRB.velocity = new Vector2(0, 0);
    }
}
