using System.Collections.Generic;
using Grid;
using UnityEngine;

public class EnemyWaveScript : MonoSingleton<EnemyWaveScript>
{
    [SerializeField]GridHandler2 gridHandler;

    [SerializeField]private List<GameObject> enemyPrefabs;
    [SerializeField]private GameObject bossPrefab; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave(int difficulty)
    {
        for (int i = 0; i < difficulty; i++)
        {
            int enemyprefab = Random.Range(0, enemyPrefabs.Count);
            Debug.Log(enemyprefab);
            Instantiate(enemyPrefabs[enemyprefab], new Vector3(gridHandler.localstartpos.x,0 + gridHandler.grid.transform.position.y + gridHandler.grid.transform.localScale.y /2 + enemyPrefabs[enemyprefab].transform.localScale.y , gridHandler.localstartpos.y), Quaternion.identity );
        }
    }
}
