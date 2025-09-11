using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GridMapPositionShow : MonoBehaviour
{
    [SerializeField] private Camera rayCastCamera;
    [SerializeField] private LayerMask placementlayermask;
    private Vector3 _lastPosition;
    [SerializeField] private GameObject rayCastObjectInidcator;
    Ray _ray;
    RaycastHit _hit;
    private Vector3 GetSelectedMapPosition()
    {

        _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
        if (Physics.Raycast(_ray, out _hit, 3, placementlayermask))
        {
            _lastPosition = _hit.point;
        }
        return _lastPosition;
    }
    
    private void Update()
    {
        Vector3 cameraRayCastPosition = GetSelectedMapPosition();
        rayCastObjectInidcator.transform.position = cameraRayCastPosition;
    }
}
