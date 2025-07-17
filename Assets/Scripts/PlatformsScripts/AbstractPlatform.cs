using System.Collections;
using UnityEngine;

/// <summary>
/// Classe base astratta per piattaforme mobili o trasformabili.
/// Gestisce due modalità: movimento continuo e movimento a step.
/// </summary>
public abstract class AbstractPlatform : MonoBehaviour
{
    [SerializeField] protected float _timeOffset; // Ritardo iniziale prima dell'avvio

    [Header("Comportamento a step")]
    [SerializeField] protected float _comportamentTime = 0.5f;
    [SerializeField] protected float _waitTime = 1f;

    [Header("Comportamento continuo")]
    [SerializeField] protected bool _continuousMovement = false;
    [SerializeField] protected float _frequency = 1f;

    protected float _startTime = 0f;

    public abstract IEnumerator DoComportamentSmooth();
    public abstract IEnumerator ResetComportamentSmooth();
    public abstract void ContinuousComportament();

    protected virtual void Start()
    {
        if (_timeOffset > 0f)
            StartCoroutine(DelayedStart());
        else
            StartPlatformLogic();
    }

   
    private void StartPlatformLogic()
    {
        _startTime = Time.time;

        if (_continuousMovement)
            StartCoroutine(ContinuousLoop());
        else
            StartCoroutine(HandleCycle());
    }

    private IEnumerator HandleCycle()
    {
        yield return ResetComportamentSmooth();

        while (true)
        {
            yield return new WaitForSeconds(_waitTime);
            yield return DoComportamentSmooth();
            yield return new WaitForSeconds(_waitTime);
            yield return ResetComportamentSmooth();
        }
    }

    /// <summary>
    /// Esegue il comportamento continuo frame-by-frame.
    /// </summary>
    private IEnumerator ContinuousLoop()
    {
        while (true)
        {
            ContinuousComportament();
            yield return null;
        }
    }

    /// <summary>
    /// Attende il ritardo iniziale prima di avviare la logica della piattaforma.
    /// </summary>
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(_timeOffset);
        StartPlatformLogic();
    }
}
