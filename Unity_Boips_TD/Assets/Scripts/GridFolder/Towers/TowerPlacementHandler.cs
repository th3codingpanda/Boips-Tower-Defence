using EnemyFolder;
using Grid;
using Grid.Towers;
using UnityEngine;

namespace GridFolder.Towers
{
    public class TowerPlacementHandler : MonoSingleton<TowerPlacementHandler>
    {
        private InputActionManager inputAction;
        [SerializeField] private Camera rayCastCamera;
        [SerializeField] private LayerMask wallPlacementlayermask;
        [SerializeField] private LayerMask towerPlacementlayermask;
        public GameObject wallPlacementIndicator;
        [SerializeField] private float raycastDistance = 5;
        [SerializeField] private GameObject grid;
        private PhaseHandler phaseHandler;
        private InputActionManager inputActionManager;
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
            inputActionManager = InputActionManager.Instance;
            gridhandler = GridHandler2.Instance;
            moneyHandler = MoneyHandler.Instance;
            phaseHandler.BuildModeRayCast.AddListener(GetSelectedMapPosition);
        }

        public void UpdateTowerSelected(GameObject tower)
        {
            towerPrefab = tower;
            Debug.Log(towerPrefab.name);
            if (towerPrefab.name == "Wall")
            {
                inputActionManager.PlaceTowerEvent.RemoveAllListeners();
                inputActionManager.PlaceTowerEvent.AddListener(PlaceWall);
            }
            else
            {
                inputActionManager.PlaceTowerEvent.RemoveAllListeners();
                inputActionManager.PlaceTowerEvent.AddListener(PlaceTower);
            }
        }
        private void GetSelectedMapPosition()
        {
            if (!phaseHandler.waveOnGoing)
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
            }
        }

        private void PlaceWall()
        {
            if (!phaseHandler.waveOnGoing)
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
        }

        private void PlaceTower()
        {
            if (_lastPositionTowerPlacementObject != null){
                TowerOnWallPlacement towerOnWallPlacement = _lastPositionTowerPlacementObject.GetComponent<TowerOnWallPlacement>();
                towerOnWallPlacement.PlaceTower(towerPrefab,towerPrefab.GetComponent<CostHandler>().cost);
            }
        }

        public void Turnoffindicator()
        {
            wallPlacementIndicator.SetActive(false);
        }
    }
}
