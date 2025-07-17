using System.Collections;
using UnityEngine;

/// <summary>
/// Gestisce una piattaforma che si muove avanti e indietro lungo un asse specificato.
/// Supporta sia movimento a step che continuo.
/// </summary>
public class MovePlatform : AbstractPlatform
{
    [Header("Proprietà Movimento")]
    [SerializeField] private float _movementValue = 3f;               // Ampiezza massima del movimento
    [SerializeField] private Vector3 _movementAxis = Vector3.right;  // Asse lungo cui muoversi

    private Vector3 _basePosition; // Posizione iniziale della piattaforma (salvata al runtime)

    protected override void Start()
    {
        _basePosition = transform.position; // Salviamo la posizione solo in runtime
        base.Start();
    }

    /// <summary>
    /// Movimento smooth verso la posizione finale (animazione di andata).
    /// </summary>
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

    /// <summary>
    /// Movimento smooth verso la posizione iniziale (animazione di ritorno).
    /// </summary>
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

    /// <summary>
    /// Movimento continuo avanti e indietro (PingPong) lungo l’asse specificato.
    /// </summary>
    public override void ContinuousComportament()
    {
        // Calcolo il valore PingPong in base al tempo, sincronizzato con lo start
        float pingPong = Mathf.PingPong((Time.time - _startTime) * _frequency, _movementValue);
        transform.position = _basePosition + _movementAxis * pingPong;
    }

    /// <summary>
    /// Disegna un gizmo che mostra la posizione massima raggiunta dalla piattaforma.
    /// </summary>
    private void OnDrawGizmos()
    {
        // In PlayMode usiamo la base registrata, altrimenti prendiamo la posizione attuale
        Vector3 basePos = Application.isPlaying ? _basePosition : transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(basePos + _movementAxis * _movementValue, 0.1f);
    }
}
