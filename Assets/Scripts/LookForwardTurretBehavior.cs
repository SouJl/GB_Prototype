using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForwardTurretBehavior : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform leftFirePoint;
    public Transform rightFirePoint;
    public GameObject bulletPrefub;
    public float fireRate = 0.5f;

    bool allowFire;

    private void Awake()
    {
        allowFire = true;
    }

    void Update()
    {
        if(allowFire)
            StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        allowFire = false;
        Instantiate(bulletPrefub, leftFirePoint.position, leftFirePoint.rotation);
        Instantiate(bulletPrefub, rightFirePoint.position, rightFirePoint.rotation);
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }
}
