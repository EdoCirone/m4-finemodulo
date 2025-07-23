using UnityEngine;

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

    [Header("Ground Checker")]
    [SerializeField] private float _groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private Vector2 _moveInput;
    private bool _jumpRequested;
    private int _currentJumpCount;

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
        // Reset salti se a terra
        if (IsGrounded() && _rb.velocity.y <= 0.1f)
        {
            _currentJumpCount = 0;
        }

        if (_jumpRequested && _currentJumpCount < _maxJumpCount)
        {
            _currentJumpCount++;
            _jumpRequested = false;

            Vector3 velocity = _rb.velocity;
            velocity.y = 0f;
            _rb.velocity = velocity;

            float jumpVelocity = Mathf.Sqrt(2f * _jumpHeight * -Physics.gravity.y);
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);
        }

        // Se non posso saltare, mantengo la richiesta (non azzero)
    }

    private bool IsGrounded()
    {
        // Centro sfera leggermente sotto il personaggio
        Vector3 origin = transform.position + Vector3.down * 0.1f;
        return Physics.CheckSphere(origin, _groundCheckRadius, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.down * 0.1f;
        Gizmos.DrawWireSphere(origin, _groundCheckRadius);
    }

}
