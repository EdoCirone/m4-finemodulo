using System.Collections;
using UnityEngine;


public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private string _paramNameForward = "forward";
    [SerializeField] private string _paramNameVerticalSpeed = "vSpeed";
    [SerializeField] private string _paramNameIsGrounded = "isGrounded";
    [SerializeField] private string _paramNameHit = "hit";
    [SerializeField] private string _paramNameFallDeath = "fallDeath";
    [SerializeField] private string _paramNameJump = "jump";
    [SerializeField] private float _paramRangeForward = 2f;

    private Animator _anim;
    private Rigidbody _rb;
    private PlayerController _playerController;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerController = GetComponent<PlayerController>();

    }
    private void Update()
    {
        Vector2 moveInput = _playerController.MoveInput;

        float movementMagnitude = moveInput.magnitude;
        _anim.SetFloat(_paramNameForward, movementMagnitude * _paramRangeForward);
        _anim.SetFloat(_paramNameVerticalSpeed, _rb.velocity.y);
        _anim.SetBool(_paramNameIsGrounded, _playerController.IsGroundedNow);
    }
    public void OnJump()
    {
        _anim.SetTrigger(_paramNameJump);
    }

    public void OnLand()
    {
        _anim.SetBool(_paramNameIsGrounded, true);
    }

    public void OnHit()
    {
        Debug.Log("Trigger Hit chiamato");
        _anim.SetTrigger(_paramNameHit);
    }

    public void OnFallDeath()
    {
        Debug.Log("Trigger fallDeath chiamato");
        _anim.SetTrigger(_paramNameFallDeath);
    }


    public void ForceGrounded()
    {
        _anim.SetBool(_paramNameIsGrounded, true);
    }


}

