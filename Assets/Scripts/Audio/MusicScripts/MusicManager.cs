using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;

    [Header("Music Parameters")]
    [Range(0f, 1f)][SerializeField] private float _defaultVolume = 1f;

    [Header("Musics List")]
    public List<SceneMusic> musicPerScene;



    //singelton

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.spatialBlend = 0f; // forzo il suono in 2D
        audioSource.volume = _defaultVolume;

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.name);
    }

    private void PlaySceneMusic(string sceneName)
    {
        AudioClip newMusic = musicPerScene.Find(m => m.sceneName == sceneName)?.musicClip;

        if (newMusic != null && audioSource.clip != newMusic)
        {
            audioSource.clip = newMusic;
            audioSource.Play();
        }
    }

}

