using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableManager : MonoBehaviour
{



    [SerializeField] private GameObject _finishPoint;



    [SerializeField] private UnityEvent _onCollectedall;
    [SerializeField] private UnityEvent _onCollected;

 
    public List<Collectable> _collectablesInScene { get; private set; }
    public List<Collectable> _playerCollectables { get; private set; } = new List<Collectable>();
    public int _totalCollectables { get; private set; }



    private void Start()
    {
        _collectablesInScene = new List<Collectable>(FindObjectsOfType<Collectable>());
        _totalCollectables = _collectablesInScene.Count;
        UIManager.Instance.UpdateCoins(CollectedCount, _totalCollectables);
    }


    /// <summary>
    /// Aggiungo alla lista dei collezionabili del Player li levo dalla lista dei Collezionabili in scena
    /// </summary>

    public void TakeCollectable(Collectable collectable)
    {
        if (_collectablesInScene.Contains(collectable))
        {
            _collectablesInScene.Remove(collectable);
            _playerCollectables.Add(collectable);
            CheckAllCollected();

            _onCollected?.Invoke();
            UIManager.Instance.UpdateCoins(CollectedCount, _totalCollectables); //Parlo all'interfaccia

        }
    }
    /// <summary>
    /// Controllo se ho preso tutti i collezionabili
    /// </summary>

    private void CheckAllCollected()
    {
        if (_playerCollectables.Count == _totalCollectables)
        {
            Debug.Log("Ho collezionato tutto quanto, spero di invocare il menu");
            _onCollectedall?.Invoke();
            _finishPoint.SetActive(true);

        }
    }

 
    public int CollectedCount => _playerCollectables.Count;

}
