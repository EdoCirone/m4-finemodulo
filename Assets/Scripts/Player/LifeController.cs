using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int _maxHp = 5;
    [SerializeField] private int _hp;
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private UnityEvent _onHit;

    [Header("Morte per caduta")]
    [SerializeField] private float _deathHeight = -10f;

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
            Die();
        }
    }

    public void AddHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Min(_hp + hp, _maxHp);
        UIManager.Instance.UpdateLife(_hp, _maxHp);
    }

    public void RemoveHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Max(_hp - hp, 0);
        UIManager.Instance.UpdateLife(_hp, _maxHp);
        _onHit?.Invoke();

        if (_hp > 0)
        {
            if (CheckpointManager.Instance.HasCheckpoint())
                _respawnable?.RespawnHere(CheckpointManager.Instance.GetCurrentCheckpoint());
            else
                _respawnable?.RespawnHere(null); // userà _startPosition
        }
        else
        {
            Die();
        }

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

    public bool IsAlive() => !_isDead;
    public int CurrentHp => _hp;
    public int MaxHp => _maxHp;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, transform.up * _deathHeight);
    }
}

