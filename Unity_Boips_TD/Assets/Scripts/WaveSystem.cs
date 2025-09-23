using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    //teld de waves en geef aan hoeveel punten de systeem heb om mee te spelen
    [SerializeField] int _waveCount = 1;
    [SerializeField] int _waveDuration = 60;
    float _waveTimer;
    //serializefield omdat ik will zien als de huidige value multiplier teveel is
    [SerializeField] int _waveValue;
    //
    [SerializeField] List<Enemy> _enemies = new List<Enemy>();
    //de lijst van enemies die klaar staan voor in gespawn te worden (serializefield zodat ik kan zien wat de lijst bevat voordat ze allemaal spawnen)
    [SerializeField] List<GameObject> _enemiesAtReady = new List<GameObject>();
    //locatie voor spawn(oorsprongelijk was van plan om een andere loactie te hebben)
    [SerializeField] Transform _spawnlacation;
    float _spawninterval;
    [SerializeField] int _spawnDuration = 30;
    float _spawntimer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_spawntimer <= 0)
        {
            if (_enemiesAtReady.Count > 0)
            {
                Instantiate(_enemiesAtReady[0], _spawnlacation.position, Quaternion.identity);
                _enemiesAtReady.RemoveAt(0);
                _spawntimer = _spawninterval;
            }
        }
        else if(_spawntimer < 0)
        {
            //weet effe noet wat ik anders kan doen
            _spawntimer++;
        }
        else
        {
            _spawntimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;

        }
        if (_waveTimer <= 0 && _enemiesAtReady.Count <= 0)
        {
            _waveCount++;
            GenWave();
        }
    }

    public void GenWave()
    {
        Debug.Log("GenWave opgeroept");
        _waveValue = _waveCount * 10;
        GenEnemies();
        //zorg voor dat er later in de spel niet een kwartier duur voordat alles gespawn is en dat begin rustiger is mer de dichtheid van de enemies
        _spawninterval = _spawnDuration / _enemiesAtReady.Count;
        _waveTimer = _waveDuration;
    }
    public void GenEnemies()
    {
        Debug.Log("GenEnemies op gereopen");
        List<GameObject> GeneratedEnemeies = new List<GameObject>();
        while (_waveValue > 0)
        {
            int RandEnemyID = Random.Range(0, _enemies.Count);
            int RandEnemyCost = _enemies[RandEnemyID].Cost;

            if (_waveValue - RandEnemyCost >= 0)
            {
                GeneratedEnemeies.Add(_enemies[RandEnemyID].EnemyPrefabs);
            }
            else if (_waveValue < 0)
            {
                break;
            }
        }
        

            _enemiesAtReady.Clear();
        _enemiesAtReady = GeneratedEnemeies;
    }
}
[System.Serializable]
public class Enemy
{
    public GameObject EnemyPrefabs;
    public int Cost;
}