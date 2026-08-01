using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CurlingStone : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private bool startMovingRight = true;

    private Rigidbody2D rb;
    private bool rightTleftF;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rightTleftF = startMovingRight;
        UpdateDirection();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ground"))
            return;

        Debug.Log("CurlingStone collided with Ground");
        rightTleftF = !rightTleftF;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        if (rightTleftF)
        {
            moveDirection = Vector2.right;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            moveDirection = Vector2.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
