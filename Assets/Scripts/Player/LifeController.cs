using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int _maxHp = 5;
    [SerializeField] private int _hp;
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private UnityEvent _onFallDeath;
    [SerializeField] private UnityEvent _onHit;

    [Header("Morte per caduta")]
    [SerializeField] private float _deathHeight = -10f;

    [Header("pausa per animazioni")]
    [SerializeField] private float _hitPauseDuration = 0.2f;
    [SerializeField] private float _fallDeathPauseDuration = 0.2f;

    private bool _isDead = false;

    private IRespawnable _respawnable;

    private void Start()
    {
        _hp = _maxHp;
        _respawnable = GetComponent<IRespawnable>();
        UIManager.Instance.UpdateLife(_hp, _maxHp);
    }

    private void Update()
    {
        if (!_isDead && transform.position.y < _deathHeight)
        {
            Debug.Log($"Y = {transform.position.y}, isDead = {_isDead}");
            DieForFalling();
        }
    }
    public bool IsAlive() => !_isDead;
    public int CurrentHp => _hp;
    public int MaxHp => _maxHp;
    public void AddHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Min(_hp + hp, _maxHp);
        UIManager.Instance.UpdateLife(_hp, _maxHp);
    }

    public void RemoveHp(int hp)
    {
        if (_isDead) return;

        Debug.Log($"RemoveHp({hp}) chiamato");

        _hp = Mathf.Max(_hp - hp, 0);
        UIManager.Instance.UpdateLife(_hp, _maxHp);
        _onHit?.Invoke();

        StartCoroutine(HitPauseAndHandleDamage());
    }

    private void Die()
    {
        if (_isDead) return;

        _isDead = true;
        _onDeath?.Invoke();
        UIManager.Instance.UpdateLife(_maxHp, _maxHp);
        Debug.Log("Sei morto!");
        Destroy(gameObject);
    }

    private void DieForFalling()
    {
        if (_isDead) return;

        _isDead = true;
        _onFallDeath?.Invoke();

        StartCoroutine(FallDeathPauseCoroutine());
    }

    private IEnumerator HitPauseAndHandleDamage()
    {


        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(_hitPauseDuration);

        Time.timeScale = originalTimeScale;


        if (_hp > 0)
        {
            if (CheckpointManager.Instance.HasCheckpoint())
                _respawnable?.RespawnHere(CheckpointManager.Instance.GetCurrentCheckpoint());
            else
                _respawnable?.RespawnHere(null);
        }
        else
        {
            Die();
        }
    }


    private IEnumerator FallDeathPauseCoroutine()
    {
        Debug.Log("FallDeathCoroutine: inizio animazione");

        _onFallDeath?.Invoke(); // parte la fallDeath animazione

        yield return new WaitForSeconds(_fallDeathPauseDuration); // tempo normale

        Debug.Log("FallDeathCoroutine: mostra Game Over");

        UIManager.Instance.UpdateLife(_maxHp, _maxHp); // opzionale reset
        MenuManager.Instance.ShowGameOverMenu(); // mostra UI
        Destroy(gameObject); // se vuoi rimuovere il player
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, transform.up * _deathHeight);
    }


}

