using UnityEngine;

public class Respawnable : MonoBehaviour, IRespawnable
{
    private Rigidbody _rb;
    private PlayerController _controller;
    private PlayerAnimatorController _animatorController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _controller = GetComponent<PlayerController>();
        _animatorController = GetComponent<PlayerAnimatorController>();
    }

    public void RespawnHere(Transform newSpawnPoint)
    {
        Transform checkpoint = newSpawnPoint ?? CheckpointManager.Instance.GetCurrentCheckpoint();

        if (checkpoint != null)
        {
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;

            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            transform.position += Vector3.down * 0.1f;

            _animatorController?.ForceGrounded();
            _controller.ForceReset();

            gameObject.SetActive(true);

            GetComponent<LifeController>()?.Revive();

            Debug.Log("Respawnato al checkpoint.");
        }
        else
        {
            Debug.LogWarning("Nessun checkpoint attivo!");
        }
    }
}

