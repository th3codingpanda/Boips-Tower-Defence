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
        [SerializeField]private List<Vector2> cellsToSearch;
        [SerializeField]private List<Vector2> searchedCells;
        [SerializeField]private List<Vector2> finalPath;
        
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
        FindPath(localstartpos, localendpos);
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
                else if (finalPath.Contains(cell.Key))
                {
                    Gizmos.color = Color.yellow;
                }
                else if (cell.Value.Iswall)
                {
                    Gizmos.color = Color.black;
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

        private void FindPath(Vector2 startPos,  Vector2 endPos)
        {
            cellsToSearch = new List<Vector2>() {localstartpos};
            searchedCells = new List<Vector2>();
            finalPath = new List<Vector2>();
            Cell startcell = cells[localstartpos];
            startcell.gcost = 0;
            startcell.hcost = GetDistance(startPos, endPos);
            startcell.fcost = GetDistance(startPos, endPos);
            while (cellsToSearch.Count > 0)
            {
                Vector2 celltobesearched = cellsToSearch[0];
                foreach (Vector2 pos in cellsToSearch)
                {
                    Cell c = cells[pos];
                    if (c.fcost < cells[celltobesearched].fcost || 
                        c.fcost == cells[celltobesearched].fcost && c.hcost == cells[celltobesearched].hcost)
                    {
                        celltobesearched = pos;
                    }
                }
                cellsToSearch.Remove(celltobesearched);
                searchedCells.Add(celltobesearched);
                if (celltobesearched == endPos)
                {
                    Cell pathcell = cells[endPos];
                    while (pathcell.Position != startPos)
                    {
                        finalPath.Add(pathcell.Position);
                        pathcell = cells[pathcell.Connection];
                    }

                    return;
                }
                SearchCellNeighbors(celltobesearched, endPos);
            }
        }

        private void SearchCellNeighbors(Vector2 cellpos, Vector2 endpos)
        {
            for (float x = cellpos.x - 1; x <= 1 + cellpos.x; x++)
            {
                for (float y = cellpos.y - 1; y <= 1 + cellpos.y; y++)
                { 
                    if ((x == cellpos.x && y != cellpos.y) || (x != cellpos.x && y == cellpos.y))
                    {
                        Vector2 neighborPos = new Vector2(x, y);
                    
                    
                    Debug.Log(neighborPos);
                    if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) &&
                        !cells[neighborPos].Iswall)
                    {
                        int GCostToNeighbor = cells[cellpos].gcost + GetDistance(cellpos, neighborPos);
                        Debug.Log(GCostToNeighbor);
                        if (GCostToNeighbor < cells[neighborPos].gcost)
                        {
                            Cell neighborNode = cells[neighborPos];
                            neighborNode.Connection = cellpos;
                            neighborNode.gcost = GCostToNeighbor;
                            neighborNode.hcost = GetDistance(neighborPos, endpos);
                            neighborNode.fcost = neighborNode.gcost + neighborNode.hcost;
                            if (!cellsToSearch.Contains(neighborPos))
                            {

                                cellsToSearch.Add(neighborPos);
                            }
                        }
                    }
                    }
                }
            }
        }

        private int GetDistance(Vector2 pos1, Vector2 pos2)
        {
            Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));
            int lowest = Mathf.Min(dist.x, dist.y);
            return lowest;
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
