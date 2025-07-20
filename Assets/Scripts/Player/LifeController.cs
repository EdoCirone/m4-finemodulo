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
    [SerializeField] private float _deathHeight = -10f; // Altezza oltre la quale l'entità muore

    private bool _isDead = false; // Flag per evitare chiamate multiple a Die()

    private void Start()
    {
        _hp = _maxHp;
        UIManager.Instance.UpdateLife(_hp, _maxHp);

    }

    private void Update()
    {
        // Morte automatica se si cade troppo in basso
        if (!_isDead && transform.position.y < _deathHeight)
        {
            Die();
        }
    }

    public void AddHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Min(_hp + hp, _maxHp);

    }

    public void RemoveHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Max(_hp - hp, 0);

        _onHit?.Invoke();


        if (_hp == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead) return; // se è già morto non morire!

        _isDead = true;
        _onDeath?.Invoke();

        _isDead = true;
        _hp = 0; // Utile per la UI 

        Debug.Log("Sei morto!");
        Destroy(gameObject);
    }


    public bool IsAlive() => !_isDead;
    public int CurrentHp => _hp;
    public int MaxHp => _maxHp;

    public void OnDrawGizmos() // disegno qualcosa all'altezza massima
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, transform.up * _deathHeight);
    }
}
