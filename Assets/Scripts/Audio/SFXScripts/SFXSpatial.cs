using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SFXSpatial : MonoBehaviour
{
    [SerializeField] private List<NamedSFX> sfxList;
    [SerializeField] private string poolTag = "PooledAudio3D"; // prefab registrato in PoolManager

    private Dictionary<string, AudioClip> sfxDict;

    public static SFXSpatial Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Evita duplicati
        }

        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var sfx in sfxList)
        {
            if (!sfxDict.ContainsKey(sfx.name))
                sfxDict.Add(sfx.name, sfx.clip);
        }
    }

    public void PlaySFXAt(string sfxName, Vector3 position, float volume = 1f)
    {
        if (!sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            Debug.LogWarning($"SFX '{sfxName}' non trovato.");
            return;
        }

        IPooledObject pooled = PoolManager.Instance.SpawnFromPool(poolTag, position, Quaternion.identity);
        if (pooled is PooledAudioSource audio)
        {
            audio.PlayClip(clip, volume);
        }
    }
}

