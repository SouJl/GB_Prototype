using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : BaseEnemy
{
    [Header("Set in Inspector")]
    public List<GameObject> raycastsView;
   
    [Header("Set Dynamically")]
    public float speedRot = 50f;
    

    public override void MoveUpdate()
    {
        RaycastPointOfView rayPoint = null;
        //if (!_isMoveEnd) return;
        foreach (var ray in raycastsView)
        {
            var rayScr = ray.GetComponent<RaycastPointOfView>();
            if (rayScr.isTargetFind)
            {
                rayPoint = rayScr;
                break;
            }
        }

        if (rayPoint != null)
        {
            StartCoroutine(MoveTowards(rayPoint.GetTargetPosition()));
        }
    }

    public override void MoveFixedUpdate()
    {
        transform.RotateAround(transform.position, Vector3.up, speedRot * Time.deltaTime);
    }


    public override void OnHitBomb(GameObject collideGo)
    {
        if (collideGo.tag == "Bomb")
        {
            health -= health / 2;
            if (health <= 0)
            {
                Main.instance.EnemyDefeat(this);
                Destroy(gameObject);
            }
            Destroy(collideGo);
        }
    }

    IEnumerator MoveTowards(Vector3 target) 
    {
        speedRot = Random.Range(50f, 101f);
        enemyAI.SetDestination(target);
        yield return new WaitForSeconds(1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (HealthBarUIManager.instance.healthCount > 0)
            {
                HealthBarUIManager.instance.MinusHealth();
            }
            else
            {
                Destroy(other.gameObject);
                Main.instance.GameOver(false);
            }
        }
    }
}
