using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] CollectableManager _manager;
    [SerializeField] private LayerMask _playerLayer;

    private void Awake()
    {
        _manager = FindAnyObjectByType<CollectableManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _playerLayer) != 0)
        {
            _manager.TakeCollectable(this);
            Destroy(gameObject);
        }
    }
}
