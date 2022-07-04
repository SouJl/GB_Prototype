using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject spawnEffect;
    public float spawnDelay = 3f;

    [Space(15)]
    public bool UseStartDelay = false;
    public float startDelay = 0f;

    private void Awake()
    {
        StartCoroutine(Spawn());    
    }

    IEnumerator Spawn() 
    {
        Main.instance.EnemyCount++;
        
        if(UseStartDelay)
            yield return new WaitForSeconds(startDelay);

        var spwnEff = Instantiate(spawnEffect, transform.position + new Vector3(0, 0.01f, 0), Quaternion.identity);
        
        yield return new WaitForSeconds(spawnDelay);
       
        Destroy(spwnEff);
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
