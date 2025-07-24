using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource _emitter;

    [SerializeField] private List<NamedSFX> _sfxList;

    private Dictionary<string, AudioClip> _sfxDict;

    private void Awake()
    {
        _emitter = GetComponent<AudioSource>();
        if (_emitter == null)
            _emitter = GetComponentInChildren<AudioSource>();

        if (_emitter == null)
        {
            Debug.LogError($"Nessun AudioSource trovato nei figli di {gameObject.name}");
            return;
        }

        _emitter.playOnAwake = false;
        _emitter.spatialBlend = 1f;// lo forzo in 3d per stare parato

        //faccio il dizionario di SFX

        _sfxDict = new Dictionary<string, AudioClip>();
        foreach (var sfx in _sfxList)
        {
            if (!_sfxDict.ContainsKey(sfx.name))
            {
                _sfxDict.Add(sfx.name, sfx.clip);
            }

        }

    }

    public void PlaySFX(string sfxName, float volume = 1f)
    {
        if (_sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            Debug.Log($"Riproduco SFX '{sfxName}' a volume {volume}");
            _emitter.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"Clip '{sfxName}' non trovato nel dizionario SFX di {gameObject.name}");
        }
    }


}
