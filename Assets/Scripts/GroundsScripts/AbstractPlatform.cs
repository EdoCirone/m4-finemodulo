using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlatform : MonoBehaviour
{
    [SerializeField] protected float _comportamentTime = 1.0f; // tempo per completare un ciclo del comportamento
    [SerializeField] protected float _waitTime = 0.5f; // tempo di attesa tra i comportamenti

    //gestiso i timer per il comportamento e l'attesa direttamente nella classe AbstractPlatform così da poterli riutilizzare in tutte le piattaforme derivate

    private float _waitTimer;// timer per il tempo di attesa
    private float _comportamentTimer;// timer per il tempo di comportamento
    private bool _isWaiting = false; //flag per verificare se la piattaforma sta aspettando
    public abstract void DoComportament();
    public abstract void ResetComportament();


    protected virtual void Start()
    {
        _waitTimer = _waitTime;
        _comportamentTimer = _comportamentTime;
        ResetComportament(); // Inizializza lo stato della piattaforma
    }

    protected virtual void Update()
    {
        if (_isWaiting)
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f)
            {
                DoComportament(); // Esegui il comportamento
                _isWaiting = false;
                _comportamentTimer = _comportamentTime; // resetta il timer del comportamento
            }
        }
        else
        {
            _comportamentTimer -= Time.deltaTime;
            if (_comportamentTimer <= 0f)
            {
                ResetComportament(); // Reset della piattaforma
                _isWaiting = true;
                _waitTimer = _waitTime; // resetta il timer di attesa
            }
        }
    }
}
