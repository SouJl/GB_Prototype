using System;
using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemy : BaseEnemy
{
    [NonSerialized]
    public Vector3 statrPos;

    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        statrPos = transform.position;
    }
 
    public override void MoveUpdate()
    {
        if (player == null) return;

        if (EnemySpawner.IsPlayerIn && enemyAI.enabled)
        {
            enemyAI.SetDestination(player.transform.position);
        }
    }

    public override void MoveFixedUpdate()
    {
        if (player == null) return;

        Vector3 targetDir = player.transform.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(targetDir);
        transform.rotation = newDir;
    }
}
