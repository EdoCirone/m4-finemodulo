using System.Collections;
using UnityEngine;

/// <summary>
///     Questa classe gestisce il movimento di un oggetto tramite traslazione lungo un asse specificato.
///   Il movimento può essere continuo o a step, e supporta l'attaccamento del player per seguire il movimento.
/// </summary>

public class TranslationMovement : AbstractObjectMovement
{
    [Header("Proprietà Movimento")]
    [SerializeField] private float _movementValue = 3f;
    [SerializeField] private Vector3 _movementAxis = Vector3.right;

    [Header("Attaccamento Player")]
    public bool EnableAttachment = true; //attiva l'attaccamento del player

    private Vector3 _basePosition;
    private Vector3 _lastPosition; // Posizione precedente per calcolare il delta
    public Vector3 DeltaPosition { get; private set; } // Delta tra la posizione attuale e quella precedente per attaccamento player

    protected override void Start()
    {
        _basePosition = transform.position;
        _lastPosition = _basePosition;
        DeltaPosition = Vector3.zero;

        base.Start();
    }

    public override IEnumerator DoComportamentSmooth() // Esegue il movimento verso la posizione target
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
        DeltaPosition = Vector3.zero;
        _lastPosition = transform.position;
    }

    public override IEnumerator ResetComportamentSmooth() // Esegue il movimento di ritorno alla posizione base
    {
        Vector3 start = transform.position;
        Vector3 target = _basePosition;
        float timer = 0f;

        while (timer < _comportamentTime) 
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;

            Vector3 newPos = Vector3.Lerp(start, target, t);// Calcola la nuova posizione interpolando tra start e target
           
            DeltaPosition = newPos - transform.position;
            transform.position = newPos;

            yield return null;
        }

        transform.position = target;
        DeltaPosition = Vector3.zero;
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
        Gizmos.DrawWireCube(basePos + _movementAxis * _movementValue, new Vector3(2,0.1f,2));
    }
}
