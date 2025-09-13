using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;

    private GameObject currentGameObject;
    //private float alpha = 0.5f;

    private GameObject CurrentPlacingTower;
    //id system
    void Start()
    {
        CurrentPlacingTower = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentPlacingTower != null)
        {
            Ray camRay = PlayerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out RaycastHit hitInfo, 100f))
            {
                if (CurrentPlacingTower.TryGetComponent<Collider>(out Collider towerCollider))
                {
                    Vector3 offset = new Vector3(0, towerCollider.bounds.extents.y, 0);
                    CurrentPlacingTower.transform.position = hitInfo.point + offset;

                    
                }
                else
                {
                    CurrentPlacingTower.transform.position = hitInfo.point;
                    
                }
            }

            // Only place tower if NOT clicking on UI
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                CurrentPlacingTower = null;
            }
        }
    }

    // Optionally enable collider here if disabled during placement
    //void ChangeAlpha(Material mat, float alphaVal)
    //{
    //    Color oldColor = mat.color;
    //    Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
    //    mat.SetColor("_Color", newColor);
    //}

    public void SetTowerToPlace(GameObject Cube)
    {
        CurrentPlacingTower = Instantiate(Cube, Vector3.zero, Quaternion.identity);
    }
}


//public void SetTowerToPlace(GameObject tower)
//    {
//        CurrentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
//    }
//}
    /*
    //pseudoCode

    if left click on button 
    (
        - Tower prefab will spawn on mouse location and move around, corresponding with mouse movement.
        - place left click again to place tower
    )
    */


