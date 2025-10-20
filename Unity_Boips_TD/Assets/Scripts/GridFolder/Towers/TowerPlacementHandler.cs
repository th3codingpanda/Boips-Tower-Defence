using System;
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
        private Vector3 _lastPosition;
        private Ray _ray;
        private RaycastHit _hit;
        [NonSerialized] private GameObject towerPrefab;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            phaseHandler = PhaseHandler.Instance;
            phaseHandler.buildModeRayCast.AddListener(GetSelectedMapPosition);
        }

        public void UpdateTowerSelected(GameObject tower)
        {
            towerPrefab = tower;
            Debug.Log(towerPrefab.name);
        }
        private void GetSelectedMapPosition()
        {

            if (towerPrefab.name == "Wall")
            {
                wallPlacementIndicator.SetActive(true);
                _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
                if (Physics.Raycast(_ray, out _hit, raycastDistance, wallPlacementlayermask))
                {
                    _lastPosition = _hit.point;
                    Debug.Log("hit wallPlacementlayermask");
                }
                
            }
            else
            {
                wallPlacementIndicator.SetActive(false);
                _ray = new Ray(rayCastCamera.transform.position, rayCastCamera.transform.forward);
                if (Physics.Raycast(_ray, out _hit, raycastDistance, towerPlacementlayermask))
                {
                    _lastPosition = _hit.point;
                    Debug.Log("hit towerPlacementlayermask");
                }
            }

     
            //Vector3 cameraRayCastPosition = _lastPosition;
            //Vector3Int gridposition = .WorldToCell(cameraRayCastPosition);
            //gridPositionIndicator.transform.position = grid.CellToWorld(gridposition) + gridIndicatorOffset;
        }
    }
}
