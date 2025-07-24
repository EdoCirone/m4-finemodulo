using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource emitter;

    [SerializeField] private List<NamedSFX> sfxList;

    private Dictionary<string, AudioClip> sfxDict;

    private void Awake()
    {
        emitter = GetComponentInChildren<AudioSource>();

        if (emitter == null)
        {
            Debug.LogError($"Nessun AudioSource trovato nei figli di {gameObject.name}");
            return;
        }

        emitter.playOnAwake = false;
        emitter.spatialBlend = 1f;// lo forzo in 3d per stare parato

        //faccio il dizionario di SFX

        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var sfx in sfxList)
        {
            if (!sfxDict.ContainsKey(sfx.name))
            {
                sfxDict.Add(sfx.name, sfx.clip);
            }

        }

    }

    public void PlaySFX(string sfxName, float volume = 1f)
    {
        if (sfxDict.TryGetValue(sfxName, out AudioClip clip))
        {
            emitter.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning($"non c'è il suono {sfxName}");
        }
    }


}
