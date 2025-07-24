using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    [SerializeField] float _maxTime = 300f;
    [SerializeField] UnityEvent _onTimeExpire;

    private bool _expired = false;
    private void Update()
    {
        if (_expired) return;


        _maxTime -= Time.deltaTime;
        _maxTime = Mathf.Max(_maxTime, 0f);

        UIManager.Instance.UpdateTimer(_maxTime);
        if (_maxTime <= 0f && !_expired)
        {
            _expired = true;
            _onTimeExpire?.Invoke();
        }






    }

    public void TimeOver() { if (_maxTime == 0) _onTimeExpire?.Invoke(); }
}
