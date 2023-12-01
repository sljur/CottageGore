using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject testGo;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private ObjectPooler _pooler;
    
    // Start is called before the first frame update; we want to make sure the objects are pulled before spawning
    void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
    }

    // Update is called once per frame; this function controls how often an enemy is spawned from the moment update is called
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            
            }
        
        }
    }

    //This function actually makes the enemies show up in the game.
    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.SetActive(true);
    
    }
}
