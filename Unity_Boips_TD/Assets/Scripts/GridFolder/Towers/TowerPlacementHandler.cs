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
        [SerializeField] private GameObject grid;
        private PhaseHandler phaseHandler;
        private InputManager inputManager;
        private GridHandler2 gridhandler;
        private MoneyHandler moneyHandler;
        private Vector2 _lastPositionWallPlacementPoint;
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
            moneyHandler = MoneyHandler.Instance;
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
                    _lastPositionWallPlacementPoint = new Vector2(_hit.point.x, _hit.point.z);
                    _lastPositionWallPlacementPoint = new Vector2(
                        Mathf.Round(_lastPositionWallPlacementPoint.x),
                        Mathf.Round(_lastPositionWallPlacementPoint.y ));
                    wallPlacementIndicator.transform.position = new Vector3(_lastPositionWallPlacementPoint.x,grid.transform.localScale.y/2 + 0.01f, _lastPositionWallPlacementPoint.y);
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
            if (_lastPositionWallPlacementPoint != gridhandler.localstartpos)
            {
                if (moneyHandler.CheckMoneyAmount(towerPrefab.GetComponent<CostHandler>().cost))
                {
                    if (gridhandler.cells[_lastPositionWallPlacementPoint].Iswall != true)
                    {
                        gridhandler.cells[_lastPositionWallPlacementPoint].Iswall = true;
                        Debug.Log($"StartPos: {gridhandler.localstartpos} EndPos: {gridhandler.localendpos}");
                        if (gridhandler.FindPath(gridhandler.localstartpos, gridhandler.localendpos))
                        {

                            moneyHandler.ChangeMoney(-towerPrefab.GetComponent<CostHandler>().cost);
                            Debug.Log("Place wall" + _lastPositionWallPlacementPoint);
                            GameObject wall = Instantiate(towerPrefab,
                                new Vector3(_lastPositionWallPlacementPoint.x,
                                    grid.transform.localScale.y / 2 + towerPrefab.transform.localScale.y / 2,
                                    _lastPositionWallPlacementPoint.y), Quaternion.identity);
                            gridhandler.cells[_lastPositionWallPlacementPoint].Iswall = true;
                            gridhandler.cells[_lastPositionWallPlacementPoint].Wall = wall;




                        }
                        else
                        {
                            gridhandler.cells[_lastPositionWallPlacementPoint].Iswall = false;
                            Debug.Log("Can Not place wall there");
                        }
                    }
                }
                else
                {
                    Debug.Log("insufficient money");
                }
            }
        }

        private void PlaceTower()
        {
            TowerOnWallPlacement towerOnWallPlacement = _lastPositionTowerPlacementObject.GetComponent<TowerOnWallPlacement>();
            towerOnWallPlacement.PlaceTower(towerPrefab,towerPrefab.GetComponent<CostHandler>().cost);
        }
    }
}
