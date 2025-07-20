using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour, IRespawnable
{
    private Transform _startPosition;
    private Transform _spawnPoint;

    [SerializeField] private Transform _customStartPoint; //questa è una cazzatina per mettere un override nel caso volessi mettere uno startpoint

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
        if (_spawnPoint != null)
        {
            transform.position = _spawnPoint.position;
        }
        else
        {
            transform.position = _startPosition.position;
        }
    }
}
