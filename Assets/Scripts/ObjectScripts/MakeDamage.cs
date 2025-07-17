using UnityEngine;
using System;

public class MakeDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private bool _deactivateOnHit = true;

    public event Action OnHit;

    private void OnTriggerEnter(Collider other)
    {
        LifeController life = other.GetComponent<LifeController>();
        if (life != null)
        {
            life.RemoveHp(_damage);
            OnHit?.Invoke(); // notifico
            if (_deactivateOnHit)
                gameObject.SetActive(false); // opzionale
        }
    }
}