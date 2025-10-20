using System.Collections.Generic;
using Grid;
using GridFolder;
using UnityEngine;

public class EnemyPathGiver : MonoSingleton<EnemyPathGiver>
{
    //private GridHandler2[] grids;
    [SerializeField]private GridHandler2 gridHandler;
    private List<Vector2> enemyPath = new List<Vector2>();
    public List<Vector2> GetEnemyPath()
    {
        if (enemyPath.Count == 0)
        {
            CreateNewPath();
        }
        return enemyPath;
    }
    public void CreateNewPath()
    {
        enemyPath = new List<Vector2>();
        for (int i  = gridHandler.finalPath.Count - 1;  i >= 0;  i--)
        {
            if (i != 0 && i != gridHandler.finalPath.Count - 1)
            {
                if ((gridHandler.finalPath[i + 1].x - gridHandler.finalPath[i].x != gridHandler.finalPath[i - 1].x - gridHandler.finalPath[i].x) &&
                    (gridHandler.finalPath[i + 1].y - gridHandler.finalPath[i].y != gridHandler.finalPath[i - 1].y - gridHandler.finalPath[i].y))
                {
                    
                    enemyPath.Add(gridHandler.finalPath[i]);
                }

            }
            else
            {
                enemyPath.Add(gridHandler.finalPath[i]);
            }
        }
        
    } 
}

