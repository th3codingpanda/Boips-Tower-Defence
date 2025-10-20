using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using GridFolder;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveHandler : MonoSingleton<EnemyWaveHandler>
{
    [SerializeField]GridHandler2 gridHandler;

    [SerializeField]private List<GameObject> enemyPrefabs;
    [SerializeField]private GameObject bossPrefab;
    private PhaseHandler phaseHandler;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private int waveAmount = 0;
    private UIHandler uiHandler;
    [SerializeField] private float waveDelay;
    private void Start()
    {
        phaseHandler = PhaseHandler.Instance;
        phaseHandler.AmountOfWaves = waveAmount;
        uiHandler = UIHandler.Instance;
        uiHandler.ChangeUIText(waveText, $"wave: 0 / {waveAmount}");
    }

    public IEnumerator SpawnWave(int difficulty)
    {
        for (int i = 0; i < difficulty * 2; i++)
        {
                int enemyprefab = Random.Range(0, enemyPrefabs.Count);
                GameObject enemy = Instantiate(enemyPrefabs[enemyprefab],
                    new Vector3(gridHandler.localstartpos.x,
                        0 + gridHandler.grid.transform.position.y + gridHandler.grid.transform.localScale.y / 2 +
                        enemyPrefabs[enemyprefab].transform.localScale.y, gridHandler.localstartpos.y),
                    Quaternion.identity);
                phaseHandler.enemiesOnScreen.Add(enemy);
                yield return new WaitForSeconds(waveDelay);
                
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
