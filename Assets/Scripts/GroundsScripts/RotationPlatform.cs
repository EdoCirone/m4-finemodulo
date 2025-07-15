using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotationPlatform : AbstractPlatform
{
    [SerializeField] private float _rotationValue = 90f;

    private Vector3 _baseRotation;


    void Start()
    {
        _baseRotation = transform.localEulerAngles;
        base.Start();
    }


    public override void DoComportament()
    {
        Vector3 rotated = _baseRotation + new Vector3(_rotationValue, 0, 0);
        transform.localEulerAngles = rotated;
    }

    public override void ResetComportament()
    {

        transform.localEulerAngles = _baseRotation;
    }
}
