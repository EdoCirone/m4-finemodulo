using UnityEngine;

/// <summary>
/// Gestisce il movimento, il tempo di vita e il ritorno al pool del proiettile.
/// </summary>
public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 2f;

    private Rigidbody _rb;
    private float _timer;
    public string PoolTag { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Inizializza il proiettile quando viene spawnato dal pool.
    /// </summary>
    public void OnObjectSpawn()
    {
        _timer = _lifeTime;
        _rb.velocity = Vector3.zero;

        // Si sottoscrive all'evento di danno per despawn
        MakeDamage dmg = GetComponent<MakeDamage>();
        if (dmg != null)
            dmg.OnHit += DeSpawn;
    }

    private void OnDisable()
    {
        // Rimuove la sottoscrizione all'evento per sicurezza
        MakeDamage dmg = GetComponent<MakeDamage>();
        if (dmg != null)
            dmg.OnHit -= DeSpawn;
    }

    /// <summary>
    /// Imposta la direzione e la velocità del proiettile.
    /// </summary>
    public void Shoot(Vector3 direction)
    {
        _rb.velocity = direction.normalized * _speed;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
            DeSpawn();
    }

    /// <summary>
    /// Disattiva e ritorna il proiettile al pool.
    /// </summary>
    private void DeSpawn()
    {
        gameObject.SetActive(false);
        PoolManager.Instance.ReturnToPool(gameObject, PoolTag);
    }

    public void SetPoolTag(string tag) => PoolTag = tag;
}