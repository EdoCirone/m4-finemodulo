using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 1.0f;
    [SerializeField] float _bulletLifeTime = 2.0f;
    [SerializeField] int _bulletDamage = 1;

    private Rigidbody _rb;
    private float _timer;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _timer = _bulletLifeTime; 

    }
    

    private void Update()
    {
       
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            DeSpawn();
                        
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life != null)
        {
            life.RemoveHp(_bulletDamage);
        }

        DeSpawn();
    }

    public void Shoot(Vector3 direction)
    {
        if (_rb != null)
        {
        
        _rb.velocity = direction * _bulletSpeed;

        }

    }

    private void DeSpawn()
    {
        gameObject.SetActive(false);
    }
}
