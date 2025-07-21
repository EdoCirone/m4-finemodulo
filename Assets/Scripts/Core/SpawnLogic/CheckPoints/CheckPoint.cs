using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject _respawnVisual; // ad esempio una bandierina o effetto

    private bool _activated = false;

    private void OnTriggerEnter(Collider other)
    {


        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

        if (respawn != null)
        {
            respawn.SetSpawnPoint(transform);
        }
        if (_activated) return;

        if (other.CompareTag("Player")) // o layer
        {
            _activated = true;
            CheckpointManager.Instance.SetCheckpoint(transform);

            if (_respawnVisual != null)
            {
                Debug.Log($"Sto spawnando la grafica{_respawnVisual}");
            }
                _respawnVisual.SetActive(true); // visual feedback
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}

