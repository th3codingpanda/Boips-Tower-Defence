using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class GridHandler2 : MonoBehaviour
{
    [SerializeField]private GameObject grid;
    [SerializeField]private  GameObject _wall;
    private Vector2 gridSize;
    private bool _baseSpawned;
    [field: Tooltip("This is the start pos based on the object its on.")]
    [SerializeField] private Vector2 localstartpos;
    [field: Tooltip("This is the end pos based on the object its on.")]
    [SerializeField]private Vector2 localendpos;
   
    
    private Dictionary<Vector2, Cell> Cells;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridSize = new Vector2(grid.transform.localScale.x, grid.transform.localScale.z);
        Cells = new Dictionary<Vector2, Cell>();
        for (float x = 0;x < gridSize.x; x++)
        {
            for (float z = 0; z < gridSize.y; z++)
            {
                Vector2 pos = new Vector2(x - grid.transform.localScale.x/2 + grid.transform.position.x + _wall.transform.localScale.x/2, z - grid.transform.localScale.z/2 + grid.transform.position.z + _wall.transform.localScale.z/2);
                Cells.Add(pos,new Cell(pos));
                Debug.Log(Cells[pos].Position);
                int number = Random.Range(0, 10);
                if (number == 0 && pos != localstartpos && pos != localendpos)
                {
                    GameObject Wall = Instantiate(_wall , new Vector3(pos.x, _wall.transform.localScale.y/2 + grid.transform.localScale.y/2 +grid.transform.position.y, pos.y), Quaternion.identity);
                    Wall.transform.parent = grid.transform;
                    Cells[pos].iswall = true;
                }
                
            }
        }
        
        
    }

    private class Cell
    {
        public Vector2 Position;
        public int fcost;
        public int gcost;
        public int hcost;
        public Vector2 Connection;
        public bool iswall;
        public Cell(Vector2 posi)
        {
            Position = posi;
        }
    }
}
