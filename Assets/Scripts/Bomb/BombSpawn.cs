using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject bombPrefub;
    public float lifeTime = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(BombBarUIManager.instance.bombCount > 0) 
            {
                var go = Instantiate(bombPrefub, transform.position, transform.rotation);
                Destroy(go, lifeTime);
                BombBarUIManager.instance.RemoveBomb();
            }
        }
    }
}