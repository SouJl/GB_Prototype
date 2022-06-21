using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public bool IsDetonate = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            var p = other.gameObject.GetComponent<BaseEnemy>();
            p.OnHitBomb(gameObject);
            IsDetonate = true;
        }
    }
}
