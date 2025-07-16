using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum MovementType { Normal, Tank }

    [SerializeField] private MovementType _movementType;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJumpCount = 2;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 1.1f;

    private float _horizontalInput;
    private float _verticalInput;
    private int _jumpCount = 0;
    private bool _jumpInput;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _jumpInput = Input.GetButtonDown("Jump");
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        switch (_movementType)
        {
            case MovementType.Normal:
                MovePlayer();
                break;
            case MovementType.Tank:
                TankMovement();
                break;
        }

        if (IsGrounded()) _jumpCount = 0;
        Jump();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(_horizontalInput, 0, _verticalInput).normalized;
        if (moveDirection.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + moveDirection * _moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void TankMovement()
    {
        _rb.MovePosition(_rb.position + transform.forward * _verticalInput * _moveSpeed * Time.fixedDeltaTime);
        float tankRotationSpeed = _rotationSpeed * 10;
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0, _horizontalInput * tankRotationSpeed * Time.fixedDeltaTime, 0));
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    private void Jump()
    {
        if (_jumpInput && _jumpCount < _maxJumpCount)
        {
            _jumpCount++;
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * _groundCheckDistance);
    }
}
