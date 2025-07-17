using System.Collections;
using UnityEngine;


public abstract class AbstractPlatform : MonoBehaviour
{
    [SerializeField] protected float _timeOffset; // Delay iniziale prima di iniziare la logica della piattaforma

    [Header("Comportamento a step")]
    [SerializeField] protected float _comportamentTime = 0.5f; // Durata dell'animazione di movimento/reset
    [SerializeField] protected float _waitTime = 1f;          // Tempo di pausa tra i movimenti

    [Header("Comportamento continuo")]
    [SerializeField] protected bool _continuosMoviment = false; // Se attivo, la piattaforma si muove in modo continuo
    [SerializeField] protected float _frequency = 1f;            // Frequenza del movimento continuo

    // Controlla se  esegue il movimento continuo
    private bool _canUpdateContinuously = false;

    // Time per sincronizzare i movimenti continui
    protected float _startTime = 0f;

    // Metodi astratti da implementare nelle sottoclassi
    public abstract IEnumerator DoComportamentSmooth();
    public abstract IEnumerator ResetComportamentSmooth();
    public abstract void ContinuousComportament();

    protected virtual void Start()
    {
        // Se è impostato un delay, aspetta prima di iniziare la logica
        if (_timeOffset > 0f)
        {
            StartCoroutine(DelayedStart());
        }
        else
        {
            StartPlatformLogic();
        }
    }

    protected virtual void Update()
    {
        // Se è attivo il movimento continuo e il delay è terminato, esegui il comportamento continuo
        if (_continuosMoviment && _canUpdateContinuously)
        {
            ContinuousComportament();
        }
    }

    // Avvia la logica della piattaforma dopo il delay (se presente).

    private void StartPlatformLogic()
    {
        _startTime = Time.time;

        if (_continuosMoviment)
        {
            _canUpdateContinuously = true;
        }
        else
        {
            StartCoroutine(HandleCycle());
        }
    }

    //Esegue il ciclo: Reset → Pausa → Movimento → Pausa → Reset...

    private IEnumerator HandleCycle()
    {
        yield return ResetComportamentSmooth(); // Stato iniziale

        while (true)
        {
            yield return new WaitForSeconds(_waitTime);          // Pausa prima del movimento
            yield return DoComportamentSmooth();                 // Movimento "attivo"
            yield return new WaitForSeconds(_waitTime);          // Pausa dopo il movimento
            yield return ResetComportamentSmooth();              // Rientro alla posizione iniziale
        }
    }

    /// <summary>
    /// Ritarda l'inizio del comportamento per supportare un offset temporale.
    /// </summary>
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(_timeOffset);
        StartPlatformLogic();
    }
}
