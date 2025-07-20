using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    [Header("Riferimenti")]
    [SerializeField] private Transform _cameraPivot;     // Nodo che ruota sull'asse Y (orizzontale)
    [SerializeField] private Transform _cameraPitch;    // Nodo figlio che ruota sull'asse X (verticale)
    [SerializeField] private Transform _player;

    [Header("Sensibilità")]
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _minPitch = -30f;
    [SerializeField] private float _maxPitch = 60f;

    private Vector2 _mouseInput;
    private bool _cameraMovementRequested;
    private float _currentPitch = 0f;

    private void Update()
    {
        ReadInput();
        CameraRotation();
        FollowPlayer();
    }

    public void SetPlayer(Transform newPlayer)
    {
        _player = newPlayer;
    }
    private void ReadInput()
    {
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _cameraMovementRequested = Input.GetMouseButton(0);
    }

    private void CameraRotation()
    {
        if (!_cameraMovementRequested) return;

        // Rotazione orizzontale del pivot (yaw)
        _cameraPivot.Rotate(Vector3.up * _mouseInput.x * _rotationSpeed, Space.Self);

        // Gestione del pitch (rotazione verticale)
        _currentPitch = Mathf.Clamp(_currentPitch - _mouseInput.y * _rotationSpeed, _minPitch, _maxPitch);
        _cameraPitch.localEulerAngles = new Vector3(_currentPitch, 0f, 0f);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
            SetPlayer(playerGO.transform);
    }
    private void FollowPlayer()
    {
        if (_player == null) return;
        _cameraPivot.position = _player.position;
    }
}
