using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGrenade : MonoBehaviour
{
    public GameObject grenade;
    public float range;

    public void Launch() 
    {
        var go = Instantiate(grenade, transform.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * range, ForceMode.Impulse);
    }
}
