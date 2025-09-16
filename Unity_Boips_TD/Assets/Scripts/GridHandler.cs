using System;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoSingleton<GridHandler>
{
    [SerializeField] private Camera rayCastCamera;
    [SerializeField] private LayerMask placementlayermask;
    [SerializeField] private GameObject gridPositionIndicator;
    [SerializeField] private Grid grid;
    [SerializeField] private Vector3 gridIndicatorOffset;
    private Vector3 _lastPosition;
    private Dictionary<Vector3Int, GameObject> placedTowers = new Dictionary<Vector3Int, GameObject>();
    Ray _ray;
    RaycastHit _hit;
    private void GetSelectedMapPosition()
    {

        _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
        if (Physics.Raycast(_ray, out _hit, 3, placementlayermask))
        {
            _lastPosition = _hit.point;
        }
        Vector3 cameraRayCastPosition = _lastPosition;
        Vector3Int gridposition = grid.WorldToCell(cameraRayCastPosition);
        gridPositionIndicator.transform.position = grid.CellToWorld(gridposition) + gridIndicatorOffset;

    }

    private void GetTowerPlacement()
    {
        
        _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
        if (Physics.Raycast(_ray, out _hit, 3, placementlayermask))
        {
            _lastPosition = _hit.point;
        }
        Vector3 cameraRayCastPosition = _lastPosition;
        Vector3Int gridposition = grid.WorldToCell(cameraRayCastPosition);
        gridPositionIndicator.transform.position = grid.CellToWorld(gridposition) + gridIndicatorOffset;
        Instantiate(gridPositionIndicator, gridPositionIndicator.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        GetSelectedMapPosition();
    }
}
