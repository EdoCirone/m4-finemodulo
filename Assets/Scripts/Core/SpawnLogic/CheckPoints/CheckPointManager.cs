using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private Transform _currentCheckpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        _currentCheckpoint = checkpoint;
        Debug.Log("Checkpoint aggiornato: " + checkpoint.name);
    }

    public Transform GetCurrentCheckpoint()
    {
        return _currentCheckpoint;
    }

    //Metodo necessario per il controllo in LifeController
    public bool HasCheckpoint() => _currentCheckpoint != null;
}
