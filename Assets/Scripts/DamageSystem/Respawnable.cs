using UnityEngine;

public class Respawnable : MonoBehaviour
{
    public void RespawnHere()
    {
        Transform checkpoint = CheckpointManager.Instance.GetCurrentCheckpoint();

        if (checkpoint != null)
        {
            transform.position = checkpoint.position;
            Debug.Log("Respawnato al checkpoint.");
        }
        else
        {
            Debug.LogWarning("Nessun checkpoint attivo!");
        }
    }
}