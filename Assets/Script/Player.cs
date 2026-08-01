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
    [SerializeField] private const float _waterToPlayerDistance = 30f;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private GameObject _spriteMask;
    [SerializeField] private FadeControl _fadeControl;
    [SerializeField] private GameObject _water;

    [HideInInspector] public bool IsPaused = false;

    private bool _isAlive;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isOnLadder;
    private float _vertical;
    private float _defaultGravityScale;

    // Ladder jump detachment cooldown
    private float _ladderJumpCooldown = 0f;
    private const float LADDER_JUMP_COOLDOWN_TIME = 0.4f;

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

        _defaultGravityScale = _rigidbody2D.gravityScale;
    }

    private void Update()
    {
        if (IsPaused)
            return;
        Retry();

        if (_isAlive)
        {
            Light();
            Move();
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
            // Dead
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

        // Handle ladder detachment timer
        if (_ladderJumpCooldown > 0f)
        {
            _ladderJumpCooldown -= Time.deltaTime;
        }

        if (_isOnLadder)
        {
            _vertical = Input.GetAxisRaw("Vertical");
            Flip();

            if (Input.GetButtonDown("Jump"))
            {
                DetachFromLadder();
                Jump();
            }

            return;
        }

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
        }

        if (Input.GetButtonUp("Jump") && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        float verticalVelocity = _isOnLadder && !_isJumping ? _vertical * _moveSpeed : _rigidbody2D.velocity.y;
        _rigidbody2D.velocity = new Vector2(_horizontal * _moveSpeed, verticalVelocity);
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpPower);
        _isJumping = true;
    }

    private void DetachFromLadder()
    {
        _isOnLadder = false;
        _rigidbody2D.gravityScale = _defaultGravityScale;
        _ladderJumpCooldown = LADDER_JUMP_COOLDOWN_TIME;
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
        if (collider.CompareTag("Water") && _isAlive)
        {
            _isAlive = false;

            if (_fadeControl != null)
                _fadeControl.FadeOut(SpawnAtCheckpoint);
            else
                SpawnAtCheckpoint();
        }
        if (collider.CompareTag("Checkpoint"))
        {
            _spawnPosition = collider.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder") && _ladderJumpCooldown <= 0f)
        {
            _isOnLadder = true;
            _rigidbody2D.gravityScale = 0f;
            _isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            _isOnLadder = false;
            _rigidbody2D.gravityScale = _defaultGravityScale;
            _isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Box"))
        {
            _isGrounded = true;
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Box"))
        {
            _isGrounded = false;
        }
    }

    public void SpawnAtCheckpoint()
    {
        transform.position = _spawnPosition;
        _water.transform.position = new Vector3(0, _spawnPosition.y - _waterToPlayerDistance, 0);
        _isAlive = true;
    }
}