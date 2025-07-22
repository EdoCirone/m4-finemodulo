using System.Collections;
using UnityEngine;

/// <summary>
/// Gestisce oggetti che traslano lungo un asse, sia con movimento continuo che a step.
/// Espone DeltaPosition per l’attaccamento preciso del player.
/// </summary>
public class TranslationMovement : AbstractObjectMovement
{
    [Header("Proprietà Movimento")]
    [SerializeField] private float _movementValue = 3f;
    [SerializeField] private Vector3 _movementAxis = Vector3.right;

    private Vector3 _basePosition;
    private Vector3 _lastPosition;
    public Vector3 DeltaPosition { get; private set; }

    protected override void Start()
    {
        _basePosition = transform.position;
        _lastPosition = _basePosition;
        DeltaPosition = Vector3.zero;

        base.Start();
    }

    public override IEnumerator DoComportamentSmooth()
    {
        Vector3 start = transform.position;
        Vector3 target = _basePosition + _movementAxis * _movementValue;
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;

            Vector3 newPos = Vector3.Lerp(start, target, t);
            DeltaPosition = newPos - transform.position;
            transform.position = newPos;

            yield return null;
        }

        transform.position = target;
        DeltaPosition = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }

    public override IEnumerator ResetComportamentSmooth()
    {
        Vector3 start = transform.position;
        Vector3 target = _basePosition;
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;

            Vector3 newPos = Vector3.Lerp(start, target, t);
            DeltaPosition = newPos - transform.position;
            transform.position = newPos;

            yield return null;
        }
        
        transform.position = target;
        DeltaPosition = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }

    public override void ContinuousComportament()
    {
        float pingPong = Mathf.PingPong((Time.time - _startTime) * _frequency, _movementValue);
        Vector3 newPos = _basePosition + _movementAxis * pingPong;

        DeltaPosition = newPos - transform.position;
        transform.position = newPos;

        _lastPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        Vector3 basePos = Application.isPlaying ? _basePosition : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(basePos + _movementAxis * _movementValue, 0.1f);
    }
}
