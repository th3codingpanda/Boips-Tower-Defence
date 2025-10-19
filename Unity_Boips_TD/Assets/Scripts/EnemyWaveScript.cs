using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveScript : MonoSingleton<EnemyWaveScript>
{
    [SerializeField]GridHandler2 gridHandler;

    [SerializeField]private List<GameObject> enemyPrefabs;
    [SerializeField]private GameObject bossPrefab;
    private PhaseHandler phaseHandler;

    private void Start()
    {
        phaseHandler = PhaseHandler.Instance;
    }

    public IEnumerator SpawnWave(int difficulty)
    {

        for (int i = 0; i < difficulty * 2; i++)
        {
                int enemyprefab = Random.Range(0, enemyPrefabs.Count);
                Debug.Log(enemyprefab);
                GameObject enemy = Instantiate(enemyPrefabs[enemyprefab],
                    new Vector3(gridHandler.localstartpos.x,
                        0 + gridHandler.grid.transform.position.y + gridHandler.grid.transform.localScale.y / 2 +
                        enemyPrefabs[enemyprefab].transform.localScale.y, gridHandler.localstartpos.y),
                    Quaternion.identity);
                phaseHandler.enemiesOnScreen.Add(enemy);
                yield return new WaitForSeconds(2);
                
        }
        if (difficulty % 5 == 0)
        {
            GameObject boss = Instantiate(bossPrefab,
                new Vector3(gridHandler.localstartpos.x,
                    0 + gridHandler.grid.transform.position.y + gridHandler.grid.transform.localScale.y / 2 +
                    bossPrefab.transform.localScale.y, gridHandler.localstartpos.y),
                Quaternion.identity);
            phaseHandler.enemiesOnScreen.Add(boss);
        }
    }
}
