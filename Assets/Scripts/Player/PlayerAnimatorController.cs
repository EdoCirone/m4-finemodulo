using System.Collections;
using UnityEngine;


public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private string _paramNameForward = "forward";
    [SerializeField] private float _paramRangeForward = 2f;

    private Animator _anim;



    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float v = Input.GetAxis("Vertical");

        _anim.SetFloat(_paramNameForward, v * 2);
    }



}

