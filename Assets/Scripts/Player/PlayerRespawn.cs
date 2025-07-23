using UnityEngine;

public class PlayerRespawn : MonoBehaviour, IRespawnable
{
    [SerializeField] private Transform _customStartPoint;

    private Transform _startPosition;
    private Transform _spawnPoint;

    private void Start()
    {
        _startPosition = _customStartPoint != null ? _customStartPoint : transform;
    }

    public void SetSpawnPoint(Transform point)
    {
        _spawnPoint = point;
    }

    public void RespawnHere(Transform _)
    {
        Vector3 targetPos = _spawnPoint != null ? _spawnPoint.position : _startPosition.position;
        transform.position = targetPos;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();// forza il reset completo del rigidbody
        }
    }
}
