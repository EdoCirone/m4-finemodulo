using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlatform : AbstractPlatform
{
    [SerializeField] private float _scaleValue = 0.5f;

    private Vector3 _baseScale;


    void Start()
    {
        _baseScale = transform.localScale;
        base.Start();
    }


    public override void DoComportament()
    {
        transform.localScale = new Vector3(_scaleValue, _baseScale.y, _scaleValue);
    }

    public override void ResetComportament()
    {

        transform.localScale = new Vector3(_baseScale.x, _baseScale.y, _baseScale.z);
    }
}

