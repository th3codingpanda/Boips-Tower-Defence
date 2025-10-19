using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridHandler : MonoSingleton<GridHandler>
    {   
        private InputPackageManagerScript _inputScript; 
        [SerializeField] private Camera rayCastCamera;
        [SerializeField] private LayerMask placementlayermask;
        [SerializeField] private GameObject gridPositionIndicator;
        [SerializeField] private UnityEngine.Grid grid;
        [SerializeField] private Vector3 gridIndicatorOffset;
        [SerializeField] private GameObject gridDisplay;
        [SerializeField] private List<GameObject> towers;
        private GameObject towerSelected;
        private Vector3 _lastPosition;
        private readonly Dictionary<Vector3, GameObject> _placedWalls = new Dictionary<Vector3, GameObject>();
        Ray _ray;
        RaycastHit _hit;
    
        private void Start()
        {
            _inputScript = InputPackageManagerScript.Instance;
            _inputScript.BuildMenuEvent.AddListener(ToggleBuildMode);
            _inputScript.PlaceTowerEvent.AddListener(PlaceTower);
            towerSelected = towers[0];
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
            if (gridDisplay.activeSelf && !_placedWalls.ContainsKey(gridPositionIndicator.transform.position))
            { 
                Instantiate(towerSelected, gridPositionIndicator.transform.position + new Vector3(0,towerSelected.transform.localScale.y/2,0) , towers[0].transform.rotation);
                _placedWalls.Add(gridPositionIndicator.transform.position, gridDisplay.gameObject);
                foreach (var wall in _placedWalls) Debug.Log(wall.Key);
            
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
}
