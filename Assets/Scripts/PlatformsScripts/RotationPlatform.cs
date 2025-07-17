using System.Collections;
using UnityEngine;

/// <summary>
/// Piattaforma che ruota avanti e indietro lungo un asse specificato.
/// Supporta sia il comportamento a step che continuo.
/// </summary>
public class RotationPlatform : AbstractPlatform
{
    [Header("Proprietà Rotazione")]
    [SerializeField] private float _rotationValue = 90f;              // Angolo massimo di rotazione (gradi)
    [SerializeField] private Vector3 _rotationAxis = Vector3.right;  // Asse di rotazione (X, Y o Z)

    private Vector3 _baseRotation; // Rotazione iniziale della piattaforma (euler angles)

    protected override void Start()
    {
        _baseRotation = transform.localEulerAngles; // Salva l'orientamento di partenza
        base.Start();                               // Avvia logica gestita dalla classe base
    }

    /// <summary>
    /// Ruota la piattaforma in modo smooth verso la rotazione massima.
    /// </summary>
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

    /// <summary>
    /// Riporta la rotazione alla posizione originale in modo smooth.
    /// </summary>
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

    /// <summary>
    /// Rotazione continua usando PingPong per oscillare tra base e rotazione massima.
    /// </summary>
    public override void ContinuousComportament()
    {
        float pingPong = Mathf.PingPong((Time.time - _startTime) * _frequency, _rotationValue);
        transform.localEulerAngles = _baseRotation + _rotationAxis * pingPong;
    }
}
