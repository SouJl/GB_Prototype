using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform firePoint;
    public GameObject bulletPrefub;
    public float fireRate = 0.5f;

    bool allowFire;

    private void Awake()
    {
        allowFire = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && allowFire) 
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire() 
    {
        allowFire = false;
        Instantiate(bulletPrefub, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }
}
