using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyInputs : MonoBehaviour
{
    [SerializeField] UnityEvent _onEscape;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _onEscape?.Invoke();
        }
    }
}
