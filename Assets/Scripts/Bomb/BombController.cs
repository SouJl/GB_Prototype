using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            var p = other.gameObject.GetComponent<BaseEnemy>();
            p.OnHitBomb(gameObject);
            Destroy(gameObject);
        }
    }
}
