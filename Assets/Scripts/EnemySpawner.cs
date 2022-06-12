using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject enemyPrefab;
    public List<Transform> spawnPositions;
    
    [NonSerialized]
    public List<GameObject> enemys;
    [NonSerialized]
    public bool IsActive;
    [NonSerialized]
    public bool isTriggered;
    
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
        if(enemys.Count > 0) 
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
            if (other.gameObject.name == "Player")
            {
                if (!isTriggered)
                {
                    foreach (var spawnPostion in spawnPositions)
                    {
                        var go = Instantiate(enemyPrefab, spawnPostion.position, Quaternion.identity);
                        enemyGO.Add(go);
                        enemys.Add(go);
                    }
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
}
