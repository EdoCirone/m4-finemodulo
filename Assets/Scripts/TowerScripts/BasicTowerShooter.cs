using UnityEngine;

public class BasicTowerShooter : MonoBehaviour
{
    [Header("Componenti")]
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private float _activationRadius = 5f;

    [Header("Proiettili")]
    [SerializeField] string _bulletTag = "basic"; // Scegli il tipo di proiettile dal PoolManager
    [SerializeField] private float _coolDown = 1f;

    [Header("Sistema di mira")]
    [SerializeField] private bool _canAim = true;
    [SerializeField] private float _aimSpeed = 5f;
    [SerializeField] private Transform _head;        // La parte che ruota verso il bersaglio
    [SerializeField] private LayerMask _playerLayer; // Serve per trovare il player

    private float _lastShootTime;
    private Transform _currentTarget; // Per mantenere la rotazione fluida
    Collider[] hits = new Collider[1];

    private void Update()
    {
        if (_canAim) Aim();
        if (CanShoot()) Shoot();
    }

    /// <summary>
    /// Controlla se è passato abbastanza tempo per sparare.
    /// </summary>
    private bool CanShoot()
    {
        if ((Time.time - _lastShootTime >= _coolDown) && IsPlayerInRange())
        {
            _lastShootTime = Time.time;
            return true;
        }
        return false;
    }

    private bool IsPlayerInRange()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _activationRadius, hits, _playerLayer);
        return hitCount > 0;
    }


    /// <summary>
    /// Spara un proiettile nella direzione attuale dello spawner.
    /// </summary>
    private void Shoot()
    {
        Vector3 shootDirection = _bulletSpawner.forward;

        IPooledObject pooled = PoolManager.Instance.SpawnFromPool(_bulletTag, _bulletSpawner.position, Quaternion.LookRotation(shootDirection));
        Bullet bullet = pooled as Bullet;

        if (bullet != null)
        {
            bullet.Shoot(shootDirection);
        }
    }

    /// <summary>
    /// Trova un target e ruota la testa verso di esso.
    /// </summary>
    private void Aim()
    {
        if (_currentTarget == null && IsPlayerInRange())
        {
            _currentTarget = hits[0].transform;
        }

        if (_currentTarget != null)
        {
            Vector3 targetPos = _currentTarget.position;
            targetPos.y = _head.position.y; // Ignora la differenza in altezza

            Quaternion lookRotation = Quaternion.LookRotation(targetPos - _head.position);
            _head.rotation = Quaternion.Slerp(_head.rotation, lookRotation, Time.deltaTime * _aimSpeed);

            float distance = Vector3.Distance(transform.position, _currentTarget.position);
            if (distance > _activationRadius * 1.2f) // Se esce dal raggio, resetta
                _currentTarget = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        // Mostra raggio di mira in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _activationRadius);
    }

}
