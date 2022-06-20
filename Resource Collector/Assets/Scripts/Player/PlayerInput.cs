using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] Joystick _joystick;

    private PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        _playerMovement.Move(_joystick.Horizontal, _joystick.Vertical);
    }
}
