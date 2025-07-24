using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledAudioSource : MonoBehaviour, IPooledObject
{
    private string _poolTag;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public void SetPoolTag(string tag)
    {
        _poolTag = tag;
    }

    public void OnObjectSpawn()
    {
        // devo implementarlo per forza perchè fà parte dell'interfacciax
    }

    public void PlayClip(AudioClip clip, float volume = 1f)
    {
        _audioSource.clip = clip;
        _audioSource.volume = volume;
        _audioSource.Play();
        StartCoroutine(ReturnToPoolWhenDone(clip.length));
    }

    private IEnumerator ReturnToPoolWhenDone(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        PoolManager.Instance.ReturnToPool(gameObject, _poolTag);
    }
}
