using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject enemyPrefab;
    public List<Transform> spawnPositions;
    public int spawnDelay = 3;
    public GameObject onSpawnEffect;
    public bool IsActiveTimer = true;

    [NonSerialized]
    public List<GameObject> enemys;
    [NonSerialized]
    public bool IsActive;
    [NonSerialized]
    public bool isTriggered;
    [NonSerialized]
    public bool isEnemySpawn;


    private List<GameObject> enemyGO;

    public static bool IsPlayerIn;

    private void Awake()
    {
        IsPlayerIn = false;
        isTriggered = false;
        IsActive = true;
        enemyGO = new List<GameObject>();
        enemys = new List<GameObject>();
    }

    private void Update()
    {
        if (enemys == null) return;

        if (enemys.Any())
        {
            foreach (var enemy in enemys.ToList())
            {
                if (enemy == null) enemys.Remove(enemy);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActive)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!isTriggered)
                {
                    Invoke("Spawn", spawnDelay);
                    if(IsActiveTimer)
                        FightCountUI.instance.StartCount(spawnDelay);
                    SpawnEffect();
                    isTriggered = true;
                    IsPlayerIn = true;
                }
                else
                {
                    foreach (var go in enemyGO)
                    {
                        Destroy(go);
                    }
                    isTriggered = false;
                    IsPlayerIn = false;
                }
            }
        }
    }

    private void SpawnEffect() 
    {
        foreach (var spawnPostion in spawnPositions)
        {
            var spwnEffect = Instantiate(onSpawnEffect, spawnPostion.position + new Vector3(0, 0.01f, 0), Quaternion.identity);
            Destroy(spwnEffect, spawnDelay);
        }
    }

    private void Spawn()
    {
        foreach (var spawnPostion in spawnPositions)
        {
            var go = Instantiate(enemyPrefab, spawnPostion.position, Quaternion.identity);
            enemyGO.Add(go);
            enemys.Add(go);
        }
        isEnemySpawn = true;
    }

}
