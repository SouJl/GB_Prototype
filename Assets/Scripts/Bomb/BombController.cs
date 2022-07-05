using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float lifeTime = 10f;
    public GameObject explosion;


    private void Awake()
    {
        Invoke("BombDestroy", lifeTime);
    }

    public void BombDestroy()
    {
        Instantiate(explosion, transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            /*var p = other.gameObject.GetComponent<BaseEnemy>();
            p.OnHitBomb(gameObject);*/
            Instantiate(explosion, transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
