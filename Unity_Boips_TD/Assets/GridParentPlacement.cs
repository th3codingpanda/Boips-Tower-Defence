using UnityEngine;

public class GridParentPlacement : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    [SerializeField]private GameObject gridParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridParent.transform.localScale = new Vector3(1,0,1);
        gridParent.transform.localScale = new Vector3( gridParent.transform.localScale.x/grid.transform.localScale.x, gridParent.transform.localScale.y, gridParent.transform.localScale.z/grid.transform.localScale.z);
        if (grid.transform.localScale.x % 2 != 0)
        {
            gridParent.transform.position = new Vector3(gridParent.transform.position.x + 1, gridParent.transform.position.y, gridParent.transform.position.z);
        }
        if (grid.transform.localScale.z % 2 != 0)
        {
            gridParent.transform.position = new Vector3(gridParent.transform.position.x , gridParent.transform.position.y, gridParent.transform.position.z + 1);
        }
        


    }

}
