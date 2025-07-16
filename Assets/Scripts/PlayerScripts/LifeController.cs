using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    [SerializeField]private int _hp;

    private void Start()
    {
        _hp = _maxHp;
    }

    private void Update()
    {
      
    }
    public void AddHp(int hp) { _hp = Mathf.Min(_hp + hp,_maxHp); }
    public void RemoveHp(int hp) { _hp = Mathf.Max(_hp - hp, 0);
    
        if (-hp == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("sei morto!");
        GameObject.Destroy(gameObject);
    }
    public bool IsAlive()
    {
        return _hp > 0;
    }

}
