using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    private Rigidbody _rb;
    private float _timer;
    public string PoolTag { get; set; }

    private void Awake() => _rb = GetComponent<Rigidbody>();

   

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

    public void OnObjectSpawn()
    {
        _timer = _lifeTime;
        _rb.velocity = Vector3.zero;

        MakeDamage md = GetComponent<MakeDamage>();
        if (md != null)
            md.OnHit += DeSpawn;
    }

    private void OnDisable()
    {
        // Rimuovi subscription
        MakeDamage md = GetComponent<MakeDamage>();
        if (md != null)
            md.OnHit -= DeSpawn;
    }

    private void DeSpawn()
    {
        gameObject.SetActive(false);
        PoolManager.Instance.ReturnToPool(gameObject, PoolTag);
    }

    public void SetPoolTag(string tag)
    {
        PoolTag = tag;
    }
}
