using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerShooter : MonoBehaviour
{
    [Header("componenti da settare")]
    [SerializeField] Transform _bulletSpawner;


    [Header("Gestione dei Proiettili")]
    [SerializeField] Bullet _bullet;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _coolDown;

    [Header("Gestione dell'Aim")]
    [SerializeField] bool _canAim;
    [SerializeField] float _aimRadius = 5f;
    [SerializeField] Transform _head;
    [SerializeField] LayerMask _playerLayer;

    private float _lastShootTime;
    private Transform _currentTarget; //Mi serve per tenere a mente l'ultima posizione del target ed evitare scatti nella rotazione della testa
    public Queue<Bullet> _bulletPool;

    private void Start()
    {
        _bulletPool = new Queue<Bullet>();
        for (int i = 0; i < 5; i++)
        {
           Bullet bullet =  Instantiate(_bullet);
           bullet.gameObject.SetActive(false);
           _bulletPool.Enqueue(bullet);
        }
        _lastShootTime = 0;
    }

    private void Update()
    {
        if (_canAim) { Aim(); }
        if (CanShoot()) { Shoot() ; }
    }
    private void Shoot()
    {
        Vector3 shootDirection = _bulletSpawner.forward;
        Bullet b = Instantiate(_bullet, _bulletSpawner.position, Quaternion.LookRotation(shootDirection));
        b.Shoot(shootDirection);

    }

    private bool CanShoot()
    {
        if (Time.time - _lastShootTime > _coolDown)
        {
            _lastShootTime = Time.time;
            return true;
        }
        else { return false; }

    }

    private void Aim()
    {
        if (_currentTarget == null)
        {
            Collider[] players = Physics.OverlapSphere(transform.position, _aimRadius, _playerLayer);
            if (players.Length > 0)
            {
                _currentTarget = players[0].transform;
            }
        }

        if (_currentTarget != null)
        {
            Vector3 targetPos = _currentTarget.position;
            targetPos.y = _head.position.y;

            Quaternion targetRotation = Quaternion.LookRotation(targetPos - _head.position);
            _head.rotation = Quaternion.Slerp(_head.rotation, targetRotation, Time.deltaTime * 5f);

            // Se il bersaglio è troppo lontano, resetta
            float distance = Vector3.Distance(transform.position, _currentTarget.position);
            if (distance > _aimRadius * 1.2f) // con margine
            {
                _currentTarget = null;
            }
        }
    }


    public Bullet GetBullet()
    {

        if(_bulletPool.Count > 0)
        {
            Bullet bullet = _bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            _bullet = bullet;
        }
        else { Instantiate(_bullet); }

            return _bullet;
    }

    public void RelaseBullet(Bullet bullet)
    {

        bullet.gameObject.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }

}
