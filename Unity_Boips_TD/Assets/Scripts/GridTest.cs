using UnityEngine;

public class GridTest : MonoBehaviour
{
    public GameObject cube, blockPrefab;
    public Grid Grid;
    public GridTestInput gridInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 selectedPosition = gridInput.GetSelectedMapPosition();
        Vector3Int cellPosition = Grid.WorldToCell(selectedPosition);
        cube.transform.position = Grid.GetCellCenterWorld(cellPosition);

        if (gridInput.GetPlacementInput()) Instantiate(blockPrefab, cube.transform.position, Quaternion.identity);
    }
}
