using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public List<GameObject> spawners;
    public int spawnDelay = 3;
    public bool IsActiveTimer = true;

    [NonSerialized]
    public List<GameObject> enemys;
    [NonSerialized]
    public bool IsActive;
    [NonSerialized]
    public bool isTriggered;
    [NonSerialized]
    public bool isEnemySpawn;

    private void Awake()
    {
        isTriggered = false;
        IsActive = true;
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

    public void StartFight()
    {
        foreach (var spawn in spawners)
        {
            spawn.SetActive(true);
        }
        isEnemySpawn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActive)
        {
            if (other.gameObject.tag == "Player")
            {
                if (!isTriggered)
                {
                    if (IsActiveTimer)
                        FightCountUI.instance.StartCount(spawnDelay);
                    StartFight();
                    isTriggered = true;
                }
            }
        }
    }
}
