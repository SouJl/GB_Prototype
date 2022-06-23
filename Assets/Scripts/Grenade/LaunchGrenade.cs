using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGrenade : MonoBehaviour
{
    public GameObject grenade;
    public float minRange;
    public float maxRange;

    public void Launch() 
    {
        var go = Instantiate(grenade, transform.position, Quaternion.identity);
        float resultRange = Random.Range(minRange, maxRange); 
        go.GetComponent<Rigidbody>().AddForce(transform.forward * resultRange, ForceMode.Impulse);
    }
}
