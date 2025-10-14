using UnityEngine;

public class PhaseHandler : MonoBehaviour
{
    private InputPackageManagerScript inputScript;
    EnemyWaveScript enemywaveScript;
    private bool waveOnGoing = false;
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
            enemywaveScript.SpawnWave(1); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
