using System.Collections;
using UnityEngine;

/// <summary>
/// Tipo di rotazione continua da eseguire.
/// </summary>
public enum TypeOfContinuousMovement
{
    Sinusoid,
    Continuous
}

/// <summary>
/// Gestisce tutti gli oggetti che ruotano avanti e indietro lungo un asse specificato.
/// Supporta sia il comportamento a step che continuo.
/// </summary>
public class RotationObjectMovement : AbstractObjectMovement
{
    [Header("Proprietà Rotazione")]
    [SerializeField] private float _rotationValue = 90f;               // Angolo massimo di rotazione (gradi)
    [SerializeField] private Vector3 _rotationAxis = Vector3.right;   // Asse di rotazione
    [SerializeField] private TypeOfContinuousMovement _rotationType;  // Tipo di movimento continuo
    [SerializeField] private float _rotationSpeed = 90f;              // Velocità in gradi/sec per rotazione continua

    private Vector3 _baseRotation; // Rotazione iniziale

    protected override void Start()
    {
        _baseRotation = transform.localEulerAngles;
        base.Start();
    }

    public override IEnumerator DoComportamentSmooth()
    {
        Quaternion start = transform.rotation;
        Quaternion target = Quaternion.Euler(_baseRotation + _rotationAxis * _rotationValue);
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;
            transform.rotation = Quaternion.Lerp(start, target, t);
            yield return null;
        }

        transform.rotation = target;
    }

    public override IEnumerator ResetComportamentSmooth()
    {
        Quaternion start = transform.rotation;
        Quaternion target = Quaternion.Euler(_baseRotation);
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;
            transform.rotation = Quaternion.Lerp(start, target, t);
            yield return null;
        }

        transform.rotation = target;
    }

    public override void ContinuousComportament()
    {
        switch (_rotationType)
        {
            case TypeOfContinuousMovement.Sinusoid:
                float pingPong = Mathf.PingPong((Time.time - _startTime) * _frequency, _rotationValue);
                transform.localEulerAngles = _baseRotation + _rotationAxis * pingPong;
                break;

            case TypeOfContinuousMovement.Continuous:
                transform.Rotate(_rotationAxis.normalized * _rotationSpeed * Time.deltaTime, Space.Self);
                break;
        }
    }
}
