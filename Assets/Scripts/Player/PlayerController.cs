using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Tipo di movimento selezionabile
    private enum MovementType { Normal, Tank }

    [Header("Movimento")]
    [SerializeField] private MovementType _movementType = MovementType.Normal;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 5f;

    [Header("Salto")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJumpCount = 2;

    [Header("Ground Checker")]
    [SerializeField] private float _groundCheckDistance = 1.1f;
    [SerializeField] private LayerMask _groundLayer;

    // Input & stato
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
        ReadInput(); // Legge input ogni frame

    }

    private void FixedUpdate()
    {
        Movement(); // Movimento in base alla modalità selezionata
        Jump();     // Gestione salto
    }

    /// <summary>
    /// Legge gli input da tastiera
    /// </summary>
    private void ReadInput()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Richiede il salto solo se c'è ancora almeno un salto disponibile
        if (Input.GetButtonDown("Jump") && (_currentJumpCount < _maxJumpCount))
        {
            if (_currentJumpCount == 0 || !_jumpRequested) // Limita le richieste multiple in volo
                _jumpRequested = true;
        }
    }

    /// <summary>
    /// Seleziona il tipo di movimento (normale o tank).
    /// </summary>
    private void Movement()
    {
        switch (_movementType)
        {
            case MovementType.Normal:
                NormalMovement();
                break;

            case MovementType.Tank:
                TankMovement();
                break;
        }
    }

    /// <summary>
    /// Movimento "free" in tutte le direzioni con rotazione verso la direzione.
    /// </summary>
    private void NormalMovement()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;

        if (direction.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; // Il Rad2Deg trasforma i radianti in gradi, sono impazzito dietro sta roba...
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);

            Vector3 movement = direction * _moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + movement);
        }
    }

    /// <summary>
    /// Movimento tipo tank: avanti/indietro + rotazione orizzontale.
    /// </summary>
    private void TankMovement()
    {
        float moveAmount = _moveInput.y * _moveSpeed * Time.fixedDeltaTime;
        float rotationAmount = _moveInput.x * _rotationSpeed * 10f * Time.fixedDeltaTime;

        _rb.MovePosition(_rb.position + transform.forward * moveAmount);
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0f, rotationAmount, 0f));
    }

    /// <summary>
    /// Gestione del salto.
    /// </summary>
    private void Jump()
    {
        if (IsGrounded() && _rb.velocity.y <= 0.1f)
        {
            _currentJumpCount = 0;
        }

        if (_jumpRequested && _currentJumpCount < _maxJumpCount)
        {
            _currentJumpCount++;

            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            _jumpRequested = false;
        }

    }

    /// <summary>
    /// Controlla se il personaggio è a terra usando un raycast.
    /// </summary>
    private bool IsGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.2f;
        return Physics.Raycast(origin, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    /// <summary>
    /// Visualizza il raycast per il controllo del terreno nell'editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        Gizmos.DrawLine(origin, origin + Vector3.down * _groundCheckDistance);
    }
}
