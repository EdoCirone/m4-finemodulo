using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private enum MovementType { Normal, Tank }

    [Header("Movimento")]
    [SerializeField] private MovementType _movementType = MovementType.Normal;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 5f;

    [Header("Salto")]
    [SerializeField] private float _jumpHeight = 5f;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private float _jumpCooldownAfterLanding = 0.2f;

    [Header("Ground Checker")]
    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private UnityEvent _onJump;
    [SerializeField] private UnityEvent _onLand;

    private Rigidbody _rb;
    private Vector2 _moveInput;
    private bool _jumpRequested;
    private bool _wasGroundedLastFrame = false;
    private int _currentJumpCount;
    private float _lastGroundedTime = -Mathf.Infinity;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ReadInput();
    }

    private void FixedUpdate()
    {
        HandleJump();
        HandleMovement();
        CheckLanding();
    }

    private void ReadInput()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Jump"))
        {
            _jumpRequested = true;
        }
    }

    private void HandleMovement()
    {
        switch (_movementType)
        {
            case MovementType.Normal:
                MoveNormal();
                break;
            case MovementType.Tank:
                MoveTank();
                break;
        }
    }

    private void MoveNormal()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

        if (direction.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);

            Vector3 movement = direction * _moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + movement);
        }
    }

    private void MoveTank()
    {
        float moveAmount = _moveInput.y * _moveSpeed * Time.fixedDeltaTime;
        float rotationAmount = _moveInput.x * _rotationSpeed * 10f * Time.fixedDeltaTime;

        _rb.MovePosition(_rb.position + transform.forward * moveAmount);
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f));
    }

    private void HandleJump()
    {
        if (IsGrounded() && _rb.velocity.y <= 0.1f)
        {
            _currentJumpCount = 0;
        }

        bool canJump = _currentJumpCount < _maxJumpCount &&
                       (IsGrounded() ? Time.time - _lastGroundedTime >= _jumpCooldownAfterLanding : true);

        if (_jumpRequested && canJump)
        {
            _currentJumpCount++;
            _jumpRequested = false;

            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _rb.velocity = velocity;

            float jumpVelocity = Mathf.Sqrt(2f * _jumpHeight * -Physics.gravity.y);
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);

            _onJump?.Invoke();
        }
    }


    private void CheckLanding()
    {
        bool isGroundedNow = IsGrounded();

        if (!_wasGroundedLastFrame && isGroundedNow && _rb.velocity.y <= 0.1f)
        {
            _onLand?.Invoke(); // chiama l'animazione, particelle, suoni ecc.
            _lastGroundedTime = Time.time;
        }

        _wasGroundedLastFrame = isGroundedNow;
    }

    private bool IsGrounded()
    {
        // Centro sfera leggermente sotto il personaggio
        Vector3 origin = transform.position + Vector3.down * 0.1f;
        return Physics.CheckSphere(origin, _groundCheckRadius, _groundLayer);
    }

    //Resetto allo spawn per un bug con la bomba
    public void ForceReset()
    {
        _moveInput = Vector2.zero;
        _jumpRequested = false;
        _currentJumpCount = 0;

        _wasGroundedLastFrame = true;
    }

    public bool IsGroundedNow => IsGrounded();

    public Vector2 MoveInput => _moveInput;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.down * 0.1f;
        Gizmos.DrawWireSphere(origin, _groundCheckRadius);
    }

}
