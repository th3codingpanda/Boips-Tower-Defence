using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 MovementInputVector{ get; private set; }
    private void OnMove(InputValue inputValue)
    {
        MovementInputVector = inputValue.Get<Vector2>();
        Debug.Log(inputValue.Get<Vector2>());
    }
}
