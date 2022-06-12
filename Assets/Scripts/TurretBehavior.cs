using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretType 
{
    none,
    forward,
    circle
}

public class TurretBehavior : MonoBehaviour
{
    [Header("Set in Inspector")]
    public List<Transform> firePoints;
    public GameObject bulletPrefub;
    public float fireRate = 0.5f;
    public TurretType turretType;
    public float rotateSpeed = 1f;

    bool allowFire;

    private void Start()
    {
        allowFire = true;
    }

    void Update()
    {
        if (turretType == TurretType.none) return;

        if(turretType == TurretType.circle) 
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }

        if (allowFire)
            StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        allowFire = false;
        foreach(var firePoint in firePoints) 
        {
            Instantiate(bulletPrefub, firePoint.position, firePoint.rotation);
        }
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }
}
