using System.Collections;
using UnityEngine;

/// <summary>
/// Gestisce tutti gli oggetti cambiano scala nel tempo, con due modalità:
/// - Comportamento a step (riduzione/ripristino scala)
/// - Comportamento continuo (oscillazione sinusoidale)
/// </summary>
public class ScaleObjectMovement : AbstractObjectMovement
{
    [Header("Proprietà Scala")]
    [SerializeField] private float _scaleValue = 0.5f; // Fattore minimo di scala (moltiplicato alla scala base)

    private Vector3 _baseScale; // Scala originale

    protected override void Start()
    {
        _baseScale = transform.localScale; // Salva la scala iniziale
        base.Start();                      // Chiama la logica di avvio comune
    }

    /// <summary>
    /// Scala verso una scala ridotta in modo smooth.
    /// </summary>
    public override IEnumerator DoComportamentSmooth()
    {
        Vector3 start = transform.localScale;
        Vector3 target = _baseScale * _scaleValue;
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;
            transform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.localScale = target;
    }

    /// <summary>
    /// Riporta alla scala originale in modo smooth.
    /// </summary>
    public override IEnumerator ResetComportamentSmooth()
    {
        Vector3 start = transform.localScale;
        Vector3 target = _baseScale;
        float timer = 0f;

        while (timer < _comportamentTime)
        {
            timer += Time.deltaTime;
            float t = timer / _comportamentTime;
            transform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.localScale = target;
    }

    /// <summary>
    /// Scala continua tramite oscillazione sinusoidale tra 1 e _scaleValue.
    /// </summary>
    public override void ContinuousComportament()
    {
        // Normalizza il seno per ottenere valori tra 0 e 1
        float t = (Mathf.Sin((Time.time - _startTime) * _frequency) + 1f) * 0.5f;
        float scaleFactor = Mathf.Lerp(1f, _scaleValue, t);
        transform.localScale = _baseScale * scaleFactor;
    }
}
