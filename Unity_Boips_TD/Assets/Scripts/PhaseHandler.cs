using System;
using System.Collections.Generic;
using Grid;
using GridFolder.Towers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PhaseHandler : MonoSingleton<PhaseHandler>
{
    private InputManager input;
    EnemyWaveHandler enemywaveHandler;
    public bool waveOnGoing;
    private int wave;
    [NonSerialized]public int AmountOfWaves;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI phaseText;
    [SerializeField] public List<GameObject> enemiesOnScreen;
    private UIHandler uiHandler;
    private bool gameBeaten;
    private BaseHandler baseHandler;
    private TowerPlacementHandler  towerPlacementHandler;
    [NonSerialized]public readonly UnityEvent BuildModeRayCast = new UnityEvent();

    public WinLoseUi winUI;
    void Start()
    {
        input = InputManager.Instance;
        uiHandler = UIHandler.Instance;
        baseHandler = BaseHandler.Instance;
        enemywaveHandler = EnemyWaveHandler.Instance;
        towerPlacementHandler = TowerPlacementHandler.Instance;
        input.StartRound.AddListener(StartWaveSpawn);
        uiHandler.ChangeUIText(phaseText, $"Phase: Build phase");
        
    }

    private void StartWaveSpawn()
    {
        if (!waveOnGoing)
        {
            waveOnGoing = true;
            wave += 1;
            uiHandler.ChangeUIText(waveText, $"wave: {wave} / {AmountOfWaves}");
            uiHandler.ChangeUIText(phaseText, $"Phase: Wave phase");
            StartCoroutine(enemywaveHandler.SpawnWave(wave));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waveOnGoing && enemiesOnScreen.Count == 0)
        {
            waveOnGoing = false;
            uiHandler.ChangeUIText(phaseText, $"Phase: Build phase");
        }

        if (!waveOnGoing && wave == AmountOfWaves && !gameBeaten && baseHandler.baseHp >= 1 )
        {
            gameBeaten = true;
            Debug.Log("win");
            //something to show u won
            winUI.winGame();
        }

        if (!waveOnGoing)
        {
            BuildModeRayCast.Invoke();
        }
        else if (towerPlacementHandler.wallPlacementIndicator.activeInHierarchy)
        {
                towerPlacementHandler.Turnoffindicator();
        }
    }
}
