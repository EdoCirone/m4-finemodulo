using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private int _maxHp = 100; // Vita massima
    [SerializeField] private int _hp;          // Vita attuale

    [Header("Morte per caduta")]
    [SerializeField] private float _deathHeight = -10f; // Altezza oltre la quale l'entità muore

    private bool _isDead = false; // Flag per evitare chiamate multiple a Die()

    private void Start()
    {
        _hp = _maxHp;
    }

    private void Update()
    {
        // Morte automatica se si cade troppo in basso
        if (!_isDead && transform.position.y < _deathHeight)
        {
            Die();
        }
    }

    /// <summary>
    /// Aggiunge HP senza superare il massimo consentito.
    /// </summary>
    public void AddHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Min(_hp + hp, _maxHp);
    }

    /// <summary>
    /// Sottrae HP, e se raggiunge 0, attiva la morte.
    /// </summary>
    public void RemoveHp(int hp)
    {
        if (_isDead) return;

        _hp = Mathf.Max(_hp - hp, 0);
        if (_hp == 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Logica di morte, chiamata solo una volta.
    /// </summary>
    private void Die()
    {
        if (_isDead) return;

        _isDead = true;
        _hp = 0; // Utile anche per aggiornare la UI correttamente

        Debug.Log("Sei morto!");
        Destroy(gameObject);
    }

    /// <summary>
    /// Ritorna true se l'entità è ancora viva.
    /// </summary>
    public bool IsAlive() => !_isDead;
}
