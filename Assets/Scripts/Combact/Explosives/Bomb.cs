using UnityEngine;

/// <summary>
/// Componente che gestisce una bomba: si attiva se un player entra nel raggio,
/// avvia un countdown e poi esplode applicando danno.
/// </summary>
public class Bomb : MonoBehaviour
{
    [Header("Raggio di attivazione ed esplosione")]
    [SerializeField] private float _activationRadius = 3f;

    [Header("CountDown e sistema d'allarme")]
    [SerializeField] private float _explosionCountdown = 3f;
    [SerializeField] private float _allarmScaleTime = 5f;
    [SerializeField] private float _allarmScale = 0.3f;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _playerLayer;

    private Collider[] _players = new Collider[8];
    private int _playerCount = 0;

    private bool _isActivated = false;
    private float _countdown = 0f;
    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!_isActivated)
        {
            _playerCount = Physics.OverlapSphereNonAlloc(transform.position, _activationRadius, _players, _playerLayer);
            if (_playerCount > 0)
            {
                _isActivated = true;
                _countdown = _explosionCountdown;
            }
        }

        if (_isActivated)
        {
            _countdown -= Time.deltaTime;

            // Effetto visivo di allarme (pulsazione)
            float scaleFactor = 1f + Mathf.PingPong(Time.time * _allarmScaleTime, _allarmScale);
            transform.localScale = _originalScale * scaleFactor;

            if (_countdown <= 0f)
            {
                Explode();
            }
        }
    }

    /// <summary>
    /// Esegue l'esplosione: applica danno ai player nel raggio.
    /// </summary>
    private void Explode()
    {
        _playerCount = Physics.OverlapSphereNonAlloc(transform.position, _activationRadius, _players, _playerLayer);

        MakeDamage dmg = GetComponent<MakeDamage>();

        for (int i = 0; i < _playerCount; i++)
        {
            GameObject player = _players[i].gameObject;
            Debug.Log("Player colpito: " + player.name);

            // Solo danno, nessuna forza o spinta
            if (dmg != null)
                dmg.ApplyDamage(player);
        }

        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _activationRadius);
    }
}
