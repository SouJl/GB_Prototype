using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float speed = 1f;
    
    NavMeshAgent enemyAI;
    
    PlayerController player;

    [NonSerialized]
    public Vector3 statrPos;

    void Start()
    {
        enemyAI = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
        statrPos = transform.position;
    }

    
    void Update()
    {
        if (player == null) return;

        if (EnemySpawner.IsPlayerIn) 
        {
            enemyAI.SetDestination(player.transform.position);       
        }
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector3 targetDir = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(targetDir);
        transform.rotation = newDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
