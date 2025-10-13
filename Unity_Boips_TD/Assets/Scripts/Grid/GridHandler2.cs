using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grid
{
    public class GridHandler2 : MonoBehaviour
    {
        [SerializeField]private GameObject grid;
        [SerializeField]private  GameObject wallPrefab;
        [SerializeField] private bool randomwalls;
        private Vector2 gridSize;
        private bool _baseSpawned;
        public bool showGrid;
        [field: Tooltip("This is the start pos based on the object its on.")]
        [SerializeField] private Vector2 localstartpos;
        [field: Tooltip("This is the end pos based on the object its on.")]
        [SerializeField]private Vector2 localendpos;
   
    
        private Dictionary<Vector2, Cell> cells;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gridSize = new Vector2(grid.transform.localScale.x, grid.transform.localScale.z);
            cells = new Dictionary<Vector2, Cell>();
            for (float x = 0;x < gridSize.x; x++)
            {
                for (float z = 0; z < gridSize.y; z++)
                {
                    Vector2 pos = new Vector2(x - grid.transform.localScale.x/2 + grid.transform.position.x + wallPrefab.transform.localScale.x/2, z - grid.transform.localScale.z/2 + grid.transform.position.z + wallPrefab.transform.localScale.z/2);
                    cells.Add(pos,new Cell(pos));
                    Debug.Log(cells[pos].Position);
                    if (randomwalls)
                    { 
                        int number = Random.Range(0, 10);
                        if (number == 0 && pos != localstartpos && pos != localendpos)
                        {
                            GameObject wall = Instantiate(wallPrefab , new Vector3(pos.x, wallPrefab.transform.localScale.y/2 + grid.transform.localScale.y/2 +grid.transform.position.y, pos.y), Quaternion.identity);
                            wall.transform.parent = grid.transform;
                            cells[pos].Iswall = true;
                        } }
                
                }
            }
        
        
        }

        private void OnDrawGizmos()
        {
            if (!showGrid || cells == null)
            {
                return;
            }

            foreach (KeyValuePair<Vector2, Cell> cell in cells)
            {
                if (cell.Value.Position == localstartpos)
                {
                    Gizmos.color = Color.purple;
                }
                else if (cell.Value.Position == localendpos)
                {
                    Gizmos.color = Color.red;
                }
                else if (cell.Value.Iswall)
                {
                    Gizmos.color = Color.greenYellow;
                }
                else
                {
                    Gizmos.color = Color.gray2;
                }
                Gizmos.DrawCube(new Vector3(
                        (cell.Key + (Vector2)transform.position).x,
                        5,
                        (cell.Key + (Vector2)transform.position).y),
                    new Vector3(1,0,1));
            
            }
        }

        private class Cell
        {
            public Vector2 Position;
            public int fcost = int.MaxValue;
            public int gcost= int.MaxValue;
            public int hcost= int.MaxValue;
            public Vector2 Connection;
            public bool Iswall;
            public GameObject Wall;
            public Cell(Vector2 posi)
            {
                Position = posi;
            }
        }
    }
}
