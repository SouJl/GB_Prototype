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
        if (Input.GetKey(KeyCode.Mouse0) && allowFire) 
        {
            SoundManager.instance.Play("Bullet");
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire() 
    {
        allowFire = false;
        var go = Instantiate(bulletPrefub, firePoint.position, firePoint.rotation);
        go.GetComponent<Bullet>().playerRotation = transform.rotation;
        go.GetComponent<Bullet>().damage = FindObjectOfType<PlayerController>().damage;
        
        yield return new WaitForSeconds(fireRate);
        allowFire = true;
    }
}
