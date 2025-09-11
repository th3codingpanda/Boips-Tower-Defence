using UnityEngine;

public class GridTestInput : MonoBehaviour
{
    public Camera mainCamera;
    private Vector3 _lastPosition;
    public LayerMask groundLayerMask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0.1f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, groundLayerMask))
        {
            _lastPosition = hitInfo.point;
            return hitInfo.point;
        }
        return _lastPosition;
    }

    public bool GetPlacementInput()
    {
        return Input.GetMouseButtonDown(0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
