using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Transform _cameraTransform;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(float x, float z)
    {
        var cameraRotationY = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0);
        var offset = cameraRotationY * new Vector3(x, 0, z) * _speed;
        _rigidbody.velocity = offset;
        if (offset != Vector3.zero)
            _rigidbody.rotation = Quaternion.LookRotation(offset, Vector3.up);
    }
}
