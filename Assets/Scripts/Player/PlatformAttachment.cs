using UnityEngine;

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
            // Se la piattaforma è disattivata o ha perso il flag, smetti di attaccarti
            if (!_currentPlatform.EnableAttachment)
            {
                Debug.Log("PlatformAttachmentHandler: Piattaforma non più attaccabile.");
                _currentPlatform = null;
                return;
            }

            Vector3 delta = _currentPlatform.DeltaPosition;
            _rb.MovePosition(_rb.position + delta);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var platform = collision.collider.GetComponentInParent<TranslationMovement>();
        if (platform != null && platform.EnableAttachment)
        {
            Debug.Log("PlatformAttachmentHandler: Trovata piattaforma valida: " + platform.name);
            _currentPlatform = platform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_currentPlatform != null && collision.collider.gameObject == _currentPlatform.gameObject)
        {
            //Debug.Log("PlatformAttachmentHandler: Uscito dalla piattaforma " + _currentPlatform.name);
            _currentPlatform = null;
        }
    }
}
