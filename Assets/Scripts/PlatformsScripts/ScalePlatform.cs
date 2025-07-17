using System.Collections;
using UnityEngine;

public class ScalePlatform : AbstractPlatform
{
    [Header("Proprietà Scala")]
    [SerializeField] private float _scaleValue = 0.5f; // Fattore di scala minima (relativo alla scala originale)

    private Vector3 _baseScale; // Scala originale della piattaforma

    protected override void Start()
    {
        _baseScale = transform.localScale; // Salva la scala di partenza
        base.Start(); // Avvia la logica definita nella AbstractPlatform
    }


    //Riduce la scala verso il valore minimo con interpolazione smooth

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

    // Ripristina la scala originale con interpolazione smooth
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


    // Scala continua sinusoidale tra la scala originale e quella minima

    public override void ContinuousComportament()
    {
        // Oscillazione tra 1 e _scaleValue con sinusoide normalizzata [0, 1]
        float t = (Mathf.Sin((Time.time - _startTime) * _frequency) + 1f) * 0.5f;
        float scaleFactor = Mathf.Lerp(1f, _scaleValue, t);

        transform.localScale = _baseScale * scaleFactor;
    }
}
