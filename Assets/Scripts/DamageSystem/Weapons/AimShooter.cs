using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimShooter : BasicShooter
{

    [Header("Sistema di mira")]
    [SerializeField] private float _aimSpeed = 5f;
    [SerializeField] private Transform _head;        // La parte che ruota verso il bersaglio


    private Transform _currentTarget; // Per mantenere la rotazione fluida

    protected override void Update()
    {
        Aim();
        base.Update();
    }

    /// <summary>
    /// Trova un target e ruota la testa verso di esso.
    /// </summary>
    private void Aim()
    {
        if (_currentTarget == null && IsPlayerInRange())
        {
            _currentTarget = hits[0].transform;
        }

        if (_currentTarget != null)
        {
            Vector3 targetPos = _currentTarget.position;
            targetPos.y = _head.position.y; // Ignora la differenza in altezza

            Quaternion lookRotation = Quaternion.LookRotation(targetPos - _head.position);
            _head.rotation = Quaternion.Slerp(_head.rotation, lookRotation, Time.deltaTime * _aimSpeed);

            float distance = Vector3.Distance(transform.position, _currentTarget.position);
            if (distance > _activationRadius * 1.2f) // Se esce dal raggio, resetta
                _currentTarget = null;
        }
    }
}


