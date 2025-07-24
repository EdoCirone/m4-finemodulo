using UnityEngine;

public class SFXBridgePlayer : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private SFXPlayer _sfxPlayer;
    [Range(0f, 1f)][SerializeField] private float _defaultVolume = 1f;
    [SerializeField] private string _defaultSfxName = "jump";

    /// <summary>
    /// Mi serve da ponte tra PlayerSFX e Gli eventi di unity
    /// </summary>
    public void PlayByName(string sfxName)
    {
        if (_sfxPlayer != null)
        {
            _sfxPlayer.PlaySFX(sfxName, _defaultVolume);
        }
        else
        {
            Debug.LogWarning($"SFXBridge su {gameObject.name} non ha un SFXPlayer assegnato!");
        }
    }

    /// <summary>
    /// Per UnityEvent senza parametri, es. PlayDefault
    /// </summary>
    public void PlayDefault()
    {
        PlayByName(_defaultSfxName);
    }
}
