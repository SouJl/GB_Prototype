using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject bombPrefub;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(BombBarUIManager.instance.bombCount > 0) 
            {
                Instantiate(bombPrefub, transform.position - new Vector3(0,0.06f,0), transform.rotation);
                BombBarUIManager.instance.RemoveBomb();
            }
        }
    }

}
