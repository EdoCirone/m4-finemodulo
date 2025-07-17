using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class MovePlatform : AbstractPlatform
{
    [Header("Proprietà Movimento")]
    [SerializeField] private float _movementValue = 3f;             // Ampiezza del movimento
    [SerializeField] private Vector3 _movementAxis = Vector3.right;  // Direzione del movimento

    private Vector3 _basePosition; // Posizione iniziale della piattaforma

    protected override void Start()
    {
        _basePosition = transform.position;
        base.Start();
    }

    // Movimento smooth dalla posizione attuale alla posizione "di punta"

    public override IEnumerator DoComportamentSmooth()
    {
        Vector3 start = transform.position;
        Vector3 target = _basePosition + _movementAxis * _movementValue;
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
    }


    // Movimento smooth dalla posizione attuale alla posizione di partenza

    public override IEnumerator ResetComportamentSmooth()
    {
        Vector3 start = transform.position;
        Vector3 target = _basePosition;
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
    }

    // Movimento continuo avanti e indietro (PingPong) lungo l'asse scelto

    public override void ContinuousComportament()
    {
        // Time.time - _startTime serve per rispettare l'eventuale offset iniziale
        float pingPong = Mathf.PingPong((Time.time - _startTime) * _frequency, _movementValue);
        transform.position = _basePosition + _movementAxis * pingPong;
    }


    private void OnDrawGizmos()
    {
        // Se siamo in play mode usa _basePosition, altrimenti prendi transform.position attuale

        Vector3 basePos = Application.isPlaying ? _basePosition : transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(basePos + _movementAxis * _movementValue, 0.1f);
    }
}
