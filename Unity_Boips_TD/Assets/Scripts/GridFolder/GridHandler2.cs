using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GridFolder
{
    public class GridHandler2 : MonoSingleton<GridHandler2>
    {
        public GameObject grid;
        [SerializeField]private  GameObject wallPrefab;
        [SerializeField] private bool randomwalls;
        private Vector2 gridSize;
        private bool _baseSpawned;
        public bool showGrid;
        [field: Tooltip("This is the start pos based on the object its on.")]
         public Vector2 localstartpos;
        [field: Tooltip("This is the end pos based on the object its on.")]
        public Vector2 localendpos;
        public Dictionary<Vector2, Cell> cells = new Dictionary<Vector2, Cell>();
        [SerializeField]private List<Vector2> cellsToSearch;
        [SerializeField]private List<Vector2> searchedCells;
        [SerializeField]public List<Vector2> finalPath;
        private bool isStartUp = true;

        private void GenerateGrid()
        {
            DestroyWalls();
            cells = new Dictionary<Vector2, Cell>();
            for (float x = 0;x < gridSize.x; x++)
            {
                for (float z = 0; z < gridSize.y; z++)
                {
                    Vector2 pos = new Vector2(x - grid.transform.localScale.x/2 + grid.transform.position.x + wallPrefab.transform.localScale.x/2, z - grid.transform.localScale.z/2 + grid.transform.position.z + wallPrefab.transform.localScale.z/2);
                    
                    cells.Add(pos, new Cell(pos));
                    if (randomwalls)
                    { 
                        int number = Random.Range(0, 10);
                        if (number == 0 && pos != localstartpos && pos != localendpos)
                        {
                            GameObject wall = Instantiate(wallPrefab , new Vector3(pos.x, wallPrefab.transform.localScale.y/2 + grid.transform.localScale.y/2 +grid.transform.position.y, pos.y), Quaternion.identity);
                            wall.transform.parent = grid.transform;
                            cells[pos].Iswall = true;
                            cells[pos].Wall = wall;
                            Debug.Log(cells[pos].Position);
                        } }
                
                }
            } FindPath(localstartpos, localendpos);
        }

        void Start()
        {
            gridSize = new Vector2(grid.transform.localScale.x, grid.transform.localScale.z);
            localstartpos = new Vector2(localstartpos.x + grid.transform.position.x, localstartpos.y + grid.transform.position.z);
            localendpos = new Vector2(localendpos.x +  grid.transform.position.x, localendpos.y + grid.transform.position.z);
            GenerateGrid();
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

        public bool FindPath(Vector2 startPos,  Vector2 endPos)
        {
            cellsToSearch = new List<Vector2>() {startPos};
            searchedCells = new List<Vector2>();
            finalPath = new List<Vector2>();
            foreach (var cell in cells.Values)
            {   
                cell.fcost = int.MaxValue;
                cell.gcost= int.MaxValue;
                cell.hcost= int.MaxValue;
                if (cell.Connection != new Vector2
                    {
                        x = 0,
                        y = 0
                    })
                {
                    cell.Connection = new Vector2();
                }
            }
            Cell startcell = cells[startPos];
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
                    isStartUp = false;
                    Debug.Log("Do i get here?");
                    return true;
                    
                }
                SearchCellNeighbors(celltobesearched, endPos);
            }
            if (isStartUp)
            {
                Debug.Log("ReRollingWalls");
                GenerateGrid();
                return false;
            } 
            Debug.Log("Couldn't find path");
            return false;
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
                    if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) &&
                        !cells[neighborPos].Iswall)
                    {
                        int GCostToNeighbor = cells[cellpos].gcost + GetDistance(cellpos, neighborPos);
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
            int highest = Mathf.Max(dist.x, dist.y);
            int horizontalmovesrequired = highest - lowest;
            return lowest * 14 + horizontalmovesrequired * 10;
        }

        public class Cell
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

        private void DestroyWalls()
        {
            foreach (KeyValuePair<Vector2, Cell> cell in cells)
            {
                if (cell.Value.Iswall)
                {
                    Destroy(cell.Value.Wall); 
                }
            }
            
        }
    }
}
