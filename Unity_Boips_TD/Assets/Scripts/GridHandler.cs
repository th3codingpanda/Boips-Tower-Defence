using System;
using System.Collections.Generic;
using UnityEngine;

public class GridHandler : MonoSingleton<GridHandler>
{   
    private InputPackageManagerScript _inputScript; 
    [SerializeField] private Camera rayCastCamera;
    [SerializeField] private LayerMask placementlayermask;
    [SerializeField] private GameObject gridPositionIndicator;
    [SerializeField] private Grid grid;
    [SerializeField] private Vector3 gridIndicatorOffset;
    [SerializeField] private GameObject gridDisplay;
    [SerializeField] private List<GameObject> towers;
    private Vector3 _lastPosition;
    private Dictionary<Vector3Int, GameObject> _placedWalls = new Dictionary<Vector3Int, GameObject>();
    Ray _ray;
    RaycastHit _hit;
    
    private void Start()
    {
        _inputScript = InputPackageManagerScript.Instance;
        _inputScript.BuildMenuEvent.AddListener(ToggleBuildMode);
        _inputScript.PlaceTowerEvent.AddListener(PlaceTower);
    }
    private void Update()
    {
        GetSelectedMapPosition();
    }


    private void ToggleBuildMode()
    {
        gridDisplay.SetActive(!gridDisplay.activeSelf);
        gridPositionIndicator.SetActive(!gridPositionIndicator.activeSelf);
        
    }
    private void PlaceTower()
    {
        if (gridDisplay.activeSelf)
        {
            Instantiate(towers[0], gridPositionIndicator.transform.position , towers[0].transform.rotation);
        }
    }
    
    
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
}
