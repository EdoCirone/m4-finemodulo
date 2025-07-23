using System.Collections;
using UnityEngine;


public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private string _paramNameForward = "forward";
    [SerializeField] private string _paramNameVerticalSpeed = "vSpeed";
    [SerializeField] private string _paramNameIsGrounded = "isGrounded";
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

        foreach (AnimatorControllerParameter param in _anim.parameters)
        {
            Debug.Log("Animator param: " + param.name);
        }
    }

    public void OnJump()
    {
        _anim.SetTrigger(_paramNameJump);
    }

    public void OnLand()
    {
        _anim.SetBool(_paramNameIsGrounded, true);
    }

    private void Update()
    {
        float v = _playerController.MoveInput.y;

        _anim.SetFloat(_paramNameForward, v * _paramRangeForward);
        _anim.SetFloat(_paramNameVerticalSpeed, _rb.velocity.y);
        _anim.SetBool(_paramNameIsGrounded, _playerController.IsGroundedNow);
    }



}

