using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInputController _playerMovement;

    [SerializeField] private int _speed = 5;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerInputController>();
    }

    private void Update()
    {
        Vector3 positionChange = new Vector3(_playerMovement.MovementInputVector.x, 0 , _playerMovement.MovementInputVector.y);

        transform.position += positionChange * Time.deltaTime * _speed;
    }
}
    
