using UnityEngine;
using System;

/// <summary>
/// Gestisce l'applicazione di danni su impatto (trigger o collisione).
/// Comunica l'impatto tramite evento e può disattivarsi.
/// </summary>
public class MakeDamage : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private bool _deactivateOnHit = true;

    /// <summary>
    /// Evento invocato quando viene inflitto un danno.
    /// </summary>
    public event Action OnHit;

    /// <summary>
    /// Applica danno a un oggetto e gestisce eventuale disattivazione.
    /// </summary>
    public void ApplyDamage(GameObject target)
    {
        var life = target.GetComponent<LifeController>();
        if (life != null)
        {
            life.RemoveHp(_damage);
            OnHit?.Invoke();

            if (_deactivateOnHit)
                gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ApplyDamage(collision.gameObject);
    }
}
