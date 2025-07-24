using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        if (transform.parent == null)
            DontDestroyOnLoad(gameObject);
        else
            Debug.LogWarning($"{name} è figlio di {transform.parent.name} e non verrà conservato.");
    }
}