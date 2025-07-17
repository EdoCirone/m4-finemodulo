using System.Collections;
using UnityEngine;

public class RotationPlatform : AbstractPlatform
{
    [Header("Proprietà Rotazione")]
    [SerializeField] private float _rotationValue = 90f;           // Valore massimo di rotazione in gradi
    [SerializeField] private Vector3 _rotationAxis = Vector3.right; // Asse di rotazione (es. X, Y o Z)

    private Vector3 _baseRotation; // Rotazione iniziale della piattaforma

    protected override void Start()
    {
        // Salva la rotazione iniziale in euleri
        _baseRotation = transform.localEulerAngles;

        // Chiama la logica della classe base (gestione offset, ciclo, ecc.)
        base.Start();
    }

    // Ruota verso l'angolo massimo in modo smooth

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


    // Riporta la rotazione allo stato iniziale in modo smooth

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


    // Rotazione continua avanti-indietro basata su PingPong e asse definito

    public override void ContinuousComportament()
    {
        float pingPong = Mathf.PingPong((Time.time - _startTime) * _frequency, _rotationValue);
        transform.localEulerAngles = _baseRotation + _rotationAxis * pingPong;
    }
}
