using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFXTriggerSpatial : MonoBehaviour
{
    [SerializeField] private string sfxName;
    [SerializeField] private SFXSpatial spatialSFX;
    [Range(0f, 1f)] public float volume = 1f;

    public void Play()
    {
        if (spatialSFX != null)
            spatialSFX.PlaySFXAt(sfxName, transform.position, volume);
        else
            Debug.LogWarning("SFXSpatial non assegnato.");
    }
}
