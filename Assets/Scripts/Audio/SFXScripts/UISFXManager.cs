using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXManager : MonoBehaviour
{
    [Header("Lista SFX UI")]
    [SerializeField] private List<NamedSFX> sfxList;

    [Header("Inpostazioni Audio")]
    [SerializeField][Range(0f, 1f)] float defaultVolume;

    private Dictionary<string, AudioClip> sfxDict;

    private AudioSource audioSource;

    private void Awake()
    {
        //prendoL'audioSource o lo creo
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // forso il suono in 2D

        //creo e riempio il dizionario
        sfxDict = new Dictionary<string, AudioClip>();
        foreach (var sfx in sfxList)
        {

            if (!sfxDict.ContainsKey(sfx.name))
            {


                sfxDict.Add(sfx.name, sfx.clip);
            
            }
            else
            {
                Debug.LogWarning("doppio sfx" + sfx.name);
            }

        }

    }


    public void PlaySFX(string name, float volume = 1f)
    {
        if (sfxDict.TryGetValue(name, out AudioClip clip))
        {

            float v = (volume >= 0f) ? volume : defaultVolume;
            audioSource.PlayOneShot(clip, v);

        }
        else
        {
            Debug.LogWarning($"non hai l'SFX {name}");
        }

    }
}
