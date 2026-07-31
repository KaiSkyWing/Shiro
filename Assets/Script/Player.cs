using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8f;
    [SerializeField] private float _jumpPower = 16f;
    [SerializeField] private float _coyoteTime = 0.2f;
    [SerializeField] private float _jumpBufferTime = 0.2f;
    [SerializeField] private bool _isFacingRight = true;
    [SerializeField] private float _horizontal;
    [SerializeField] private float _coyoteTimeCounter;
    [SerializeField] private float _jumpBufferCounter;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _spriteMask;
    private bool _isAlive;
    private bool _isGrounded;
    private bool _isJumping;

    private Vector3 _spawnPosition;

    private void Awake()
    {
        _isAlive = true;
        _spawnPosition = transform.position;

        if (_rigidbody2D == null)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        if (_rigidbody2D == null)
        {
            _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 1f;
            _rigidbody2D.freezeRotation = true;
        }
    }

    private void Update()
    {
        Retry();

        if (_isAlive)
        {
            Light();
            Move();
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            //Dead
            Debug.Log("死んだよ！");
        }
    }

    private void Retry()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isAlive = true;
            transform.position = _spawnPosition;
        }
    }

    private void Light()
    {
        if (_spriteMask != null)
        {
            _spriteMask.SetActive(Input.GetKey(KeyCode.F));
        }
    }

    private void Move()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

        if (_isGrounded)
        {
            _coyoteTimeCounter = _coyoteTime;
            _isJumping = false;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (_jumpBufferCounter > 0f && (_coyoteTimeCounter > 0f || _isGrounded) && !_isJumping)
        {
            Jump();
            _jumpBufferCounter = 0f;
            _coyoteTimeCounter = 0f;
            _isJumping = true;
        }

        if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
        }


        Flip();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _moveSpeed, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpPower);
    }

    private void Flip()
    {
        if ((_isFacingRight && _horizontal < 0f) || (!_isFacingRight && _horizontal > 0f))
        {
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Water"))
        {
            _isAlive = false;
        }
        if (collider.CompareTag("Checkpoint"))
        {
            _spawnPosition = collider.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}