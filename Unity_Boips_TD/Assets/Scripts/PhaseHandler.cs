using System.Collections.Generic;
using UnityEngine;

public class PhaseHandler : MonoSingleton<PhaseHandler>
{
    private InputPackageManagerScript inputScript;
    EnemyWaveScript enemywaveScript;
    private bool waveOnGoing;
    private int wave;
    [SerializeField] public List<GameObject> enemiesOnScreen;
    void Start()
    {
        inputScript = InputPackageManagerScript.Instance;
        enemywaveScript = EnemyWaveScript.Instance;
        inputScript.StartRound.AddListener(StartWaveSpawn);
    }

    private void StartWaveSpawn()
    {
        if (!waveOnGoing)
        {
            waveOnGoing = true;
            wave += 1;
            StartCoroutine(enemywaveScript.SpawnWave(wave));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveOnGoing && enemiesOnScreen.Count == 0)
        {
            waveOnGoing = false;
        }
    }
}
