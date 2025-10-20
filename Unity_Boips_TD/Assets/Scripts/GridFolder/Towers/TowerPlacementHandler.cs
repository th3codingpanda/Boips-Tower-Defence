using EnemyFolder;
using Grid;
using Grid.Towers;
using UnityEngine;

namespace GridFolder.Towers
{
    public class TowerPlacementHandler : MonoSingleton<TowerPlacementHandler>
    {
        private InputManager input;
        [SerializeField] private Camera rayCastCamera;
        [SerializeField] private LayerMask wallPlacementlayermask;
        [SerializeField] private LayerMask towerPlacementlayermask;
        [SerializeField] private GameObject wallPlacementIndicator;
        [SerializeField] private float raycastDistance = 5;
        private PhaseHandler phaseHandler;
        private InputManager inputManager;
        private GridHandler2 gridhandler;
        private Vector3 _lastPositionWallPlacementPoint;
        private GameObject _lastPositionTowerPlacementObject;
        private Ray _ray;
        private RaycastHit _hit;
        private GameObject towerPrefab;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            phaseHandler = PhaseHandler.Instance;
            inputManager = InputManager.Instance;
            gridhandler = GridHandler2.Instance;
            phaseHandler.buildModeRayCast.AddListener(GetSelectedMapPosition);
        }

        public void UpdateTowerSelected(GameObject tower)
        {
            towerPrefab = tower;
            Debug.Log(towerPrefab.name);
            if (towerPrefab.name == "Wall")
            {
                inputManager.PlaceTowerEvent.RemoveAllListeners();
                inputManager.PlaceTowerEvent.AddListener(PlaceWall);
            }
            else
            {
                inputManager.PlaceTowerEvent.RemoveAllListeners();
                inputManager.PlaceTowerEvent.AddListener(PlaceTower);
            }
        }
        private void GetSelectedMapPosition()
        {

            if (towerPrefab.name == "Wall")
            {
                wallPlacementIndicator.SetActive(true);
                _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
                if (Physics.Raycast(_ray, out _hit, raycastDistance, wallPlacementlayermask))
                {
                    _lastPositionWallPlacementPoint = _hit.point;
                    //Debug.Log("hit wallPlacementlayermask: " + _lastPosition);
                }
                
            }
            else
            {
                wallPlacementIndicator.SetActive(false);
                _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
                if (Physics.Raycast(_ray, out _hit, raycastDistance, towerPlacementlayermask))
                {
                    _lastPositionTowerPlacementObject = _hit.collider.gameObject;
                    //Debug.Log("hit towerPlacementlayermask: " + _lastPosition + _hit.transform.name);
                }
            }
            //Vector3 cameraRayCastPosition = _lastPosition;
            //Vector3Int gridposition = .WorldToCell(cameraRayCastPosition);
            //gridPositionIndicator.transform.position = grid.CellToWorld(gridposition) + gridIndicatorOffset;
        }

        private void PlaceWall()
        {
            // TODO Make it translate to grid position so the check will actually check it correclty
            Debug.Log("Place wall" + _lastPositionWallPlacementPoint);
            gridhandler.cells[_lastPositionWallPlacementPoint].Iswall = true;
            if (gridhandler.FindPath(gridhandler.localstartpos, gridhandler.localendpos))
            {
                GameObject wall = Instantiate(towerPrefab, _lastPositionWallPlacementPoint , Quaternion.identity);
                gridhandler.cells[_lastPositionWallPlacementPoint].Iswall = true;
                gridhandler.cells[_lastPositionWallPlacementPoint].Wall = wall;
            }
            else
            {
                gridhandler.cells[_lastPositionWallPlacementPoint].Iswall = false;
                Debug.Log("Can Not place wall there");
            }

        }

        private void PlaceTower()
        {
            TowerOnWallPlacement towerOnWallPlacement = _lastPositionTowerPlacementObject.GetComponent<TowerOnWallPlacement>();
            towerOnWallPlacement.PlaceTower(towerPrefab,towerPrefab.GetComponent<CostHandler>().cost);
        }
    }
}
