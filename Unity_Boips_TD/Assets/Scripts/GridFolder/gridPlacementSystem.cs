using UnityEngine;
using UnityEngine.InputSystem;

public class gridPlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GridTestInput inputManager;

    private void update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;
    }
}
