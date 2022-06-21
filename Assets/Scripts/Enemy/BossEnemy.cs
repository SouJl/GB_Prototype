using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    [Header("Set in Inspector: BossEnemy")]
    public string Name;
    public List<GameObject> raycastsView;
    public List<GameObject> Launchers;
    public float maxSpeed = 15f;

    [Header("Set Dynamically: BossEnemy")]
    public float speedRot = 50f;
    public int grenadeDropChance = 10;


    private void Start() 
    {
        Main.instance.bossName.text = Name;
        Main.instance.InitSliderHealthBar((int)health);
        InvokeRepeating("OnLaunch", 0, 1);
    }

    public override void MoveUpdate()
    {
        RaycastPointOfView rayPoint = null;
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
            enemyAI.speed = Random.Range(speed, maxSpeed);
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
            health -= 20;
            Main.instance.SetValueInHealthBar((int)health);
            if (health <= 0)
            {
                Main.instance.EnemyDefeat(this);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator MoveTowards(Vector3 target) 
    {
        //speedRot = Random.Range(50f, 101f);
        enemyAI.SetDestination(target);
        yield return new WaitForSeconds(1.5f);
    }

    void OnLaunch() 
    {
        int rnd = Random.Range(0, 100);
        if (grenadeDropChance > rnd)
        {
            int ndx = Random.Range(0, Launchers.Count);
            Launchers[ndx].GetComponent<LaunchGrenade>().Launch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var p = other.gameObject.GetComponent<PlayerController>();
            if (!p.IsForceAdded) 
            {
                _rigidBody.Sleep();
                p.AddImpact(transform.forward, 30);
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
}
