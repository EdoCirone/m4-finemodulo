using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Riferimenti")]
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private Transform _player;

    [Header("Sensibilità")]
    [SerializeField] private float _rotationSpeed = 3f;

    private Vector2 _mouseInput;
    private bool _cameraMovementRequested;

    private void Update()
    {
        ReadInput();
        CameraRotation();
        FollowPlayer();
    }

    private void ReadInput()
    {
        // Usa gli assi standard del mouse
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _cameraMovementRequested = Input.GetMouseButton(0); // tiene premuto, non solo click singolo
    }

    private void CameraRotation()
    {
        if (!_cameraMovementRequested) return;

        // Ruota attorno all'asse Y (orizzontale)
        _cameraPivot.Rotate(Vector3.up, _mouseInput.x * _rotationSpeed, Space.World);

        // Ruota attorno all'asse X (verticale, invertito)
        _cameraPivot.Rotate(Vector3.right, -_mouseInput.y * _rotationSpeed, Space.Self);
    }

    private void FollowPlayer() => _cameraPivot.position = _player.position;
}