using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveScript : MonoSingleton<EnemyWaveScript>
{
    EnemyPathGiver  pathGiver;

    [SerializeField]private List<GameObject> enemyPrefabs;
    [SerializeField]private GameObject bossPrefab; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathGiver = EnemyPathGiver.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnWave(int wave)
    {

    }
}
