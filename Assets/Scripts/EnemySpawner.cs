using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
   
    public List<Transform> spawnPositions;

    bool isTriggered;

    private List<GameObject> enemyGO;

    public static bool IsPlayerIn;

    private void Awake()
    {
        IsPlayerIn = false;
        isTriggered = false;
        enemyGO = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player") 
        {
            if (!isTriggered)
            {
                foreach (var spawnPostion in spawnPositions)
                {
                    var go = Instantiate(enemyPrefab, spawnPostion.position, Quaternion.identity);
                    enemyGO.Add(go);
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
