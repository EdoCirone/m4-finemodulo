using UnityEngine;
using UnityEngine.Events;


public class BasicShooter : MonoBehaviour
{
    [Header("Componenti")]
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] protected float _activationRadius = 5f;
    [SerializeField] private LayerMask _playerLayer; // Serve per trovare il player

    [Header("Proiettili")]
    [SerializeField] string _bulletTag = "basic"; // Scegli il tipo di proiettile dal PoolManager
    [SerializeField] private float _coolDown = 1f;

    [SerializeField] private UnityEvent _onShoot;

    private float _lastShootTime;
    protected Collider[] hits = new Collider[1];

    protected virtual void Update()
    {

        if (CanShoot()) Shoot();
    }

    /// <summary>
    /// Controlla se è passato abbastanza tempo per sparare.
    /// </summary>
    protected bool CanShoot()
    {
        if ((Time.time - _lastShootTime >= _coolDown) && IsPlayerInRange())
        {
            _lastShootTime = Time.time;
            return true;
        }
        return false;
    }

    protected bool IsPlayerInRange()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _activationRadius, hits, _playerLayer);
        return hitCount > 0;
    }


    /// <summary>
    /// Spara un proiettile nella direzione attuale dello spawner.
    /// </summary>
    protected void Shoot()
    {
        Vector3 shootDirection = _bulletSpawner.forward;

        IPooledObject pooled = PoolManager.Instance.SpawnFromPool(_bulletTag, _bulletSpawner.position, Quaternion.LookRotation(shootDirection));
        Bullet bullet = pooled as Bullet;

        if (bullet != null)
        {
            bullet.Shoot(shootDirection);
            _onShoot?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Mostra raggio di mira in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _activationRadius);
    }

}
