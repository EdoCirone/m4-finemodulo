using UnityEngine;

/// <summary>
/// Attacca il player solo a piattaforme che si muovono (TranslationMovement).
/// </summary>
public class PlatformAttachmentHandler : MonoBehaviour
{
    private TranslationMovement _currentPlatform;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Debug.Log("PlatformAttachmentHandler: Awake chiamato. Rigidbody trovato? " + (_rb != null));
    }

    private void LateUpdate()
    {
        if (_currentPlatform != null)
        {
            Vector3 delta = _currentPlatform.DeltaPosition;
            Debug.Log("PlatformAttachmentHandler: DeltaPlatform = " + delta);
            _rb.MovePosition(_rb.position + delta);
        }
        else
        {
            Debug.Log("PlatformAttachmentHandler: Nessuna piattaforma attiva.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PlatformAttachmentHandler: Collisione con: " + collision.collider.name);

        var platform = collision.collider.GetComponentInParent<TranslationMovement>();
        if (platform != null)
        {
            Debug.Log("PlatformAttachmentHandler: È una piattaforma valida! Nome: " + platform.name);
            _currentPlatform = platform;
        }
        else
        {
            Debug.Log("PlatformAttachmentHandler: Non è una piattaforma. Collider: " + collision.collider.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_currentPlatform != null && collision.collider.gameObject == _currentPlatform.gameObject)
        {
            Debug.Log("PlatformAttachmentHandler: Uscito dalla piattaforma " + _currentPlatform.name);
            _currentPlatform = null;
        }
    }
}