using System;
using UnityEngine;
public class GridMapPositionShow : MonoBehaviour
{
    [SerializeField] private Camera rayCastCamera;
    [SerializeField] private LayerMask placementlayermask;
    private Vector3 _lastPosition; 
    private Vector3 GetSelectedMapPosition()
    {
        Ray _ray;
        RaycastHit _hit;
        _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
        if (Physics.Raycast(_ray, out _hit, 3, placementlayermask))
        {
            Debug.Log(_hit.collider.gameObject.name);
            _lastPosition = _hit.point;
        }
        return _lastPosition;
    }

    private void Update()
    {
        GetSelectedMapPosition();
    }
}
