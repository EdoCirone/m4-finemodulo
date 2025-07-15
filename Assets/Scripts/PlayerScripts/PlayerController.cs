using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJumpCount = 2;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1.1f;


    private float _horizontalInput;
    private float _verticalInput;
    private int _jumpCount = 0;
    private bool _jumpInput;

    private Ray _ray;
    private RaycastHit _hitInfo;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {

        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        _jumpInput = Input.GetButtonDown("Jump");
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");


    }
    public void FixedUpdate()
    {


        MovePlayer();

        if (GroundCheacker()) { _jumpCount = 0; }

        Jump();
    }


    public void MovePlayer()
    {
        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            Vector3 moveDirection = new Vector3(_horizontalInput, 0, _verticalInput).normalized;

            if (moveDirection.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                _rb.MovePosition(transform.position + moveDirection * _moveSpeed * Time.fixedDeltaTime);
            }
        }
    }


    public bool GroundCheacker()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, groundLayer);
        // Vector3.up * 0.1f mi serve per evitare che il raycast parta da dentro il terreno
    }

    public void Jump()
    {

        if (_rb != null && _jumpInput)
        {

            if (_jumpCount < _maxJumpCount)
            {
                _jumpCount++;
                _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z); // resetto la velocità verticale per evitare che la forza di salto si sommi alla velocità verticale corrente
                _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }


        }

    }


}
