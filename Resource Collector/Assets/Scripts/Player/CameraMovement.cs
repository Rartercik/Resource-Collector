using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;

    private Transform _transform;
    private Vector3 _offset;

    private void Start()
    {
        _transform = transform;
        _offset = _transform.position - _playerTransform.position;
    }

    private void Update()
    {
        _transform.position = _playerTransform.position + _offset;
    }
}
