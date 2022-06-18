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
            foreach(var enemy in enemys.ToList()) 
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
                    FightCountUI.instance.StartCount(spawnDelay);
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
