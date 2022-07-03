using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunner : BaseEnemy
{
    [Header("Set in Inspector: EnemyGunner")]
    public Transform weaponPos;
    public float fireRate = 3f;
    public float chargeTime = 1f;
    public GameObject laserLine;

    [SerializeField] private LayerMask _mask;
    [SerializeField] private Transform _player;

    Animator animator;
    private bool _allowShoot;
    private bool isTargetFind;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        animator = GetComponent<Animator>();
        _allowShoot = true;
    }

    public override void MoveFixedUpdate()
    {
        if (_player == null) return;

        Vector3 targetDir = _player.position - transform.position;
        Quaternion newDir = Quaternion.LookRotation(targetDir);
        transform.rotation = newDir;

        RaycastHit hit;
        Color drawColor = Color.red;
        var startPos = weaponPos.position;
        isTargetFind = false;
        if (Physics.Raycast(startPos, transform.forward, out hit, Mathf.Infinity, _mask))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {            
                isTargetFind = true;
            }
        }
        if(isTargetFind && _allowShoot) 
        {
            drawColor = Color.green;
            StartCoroutine(Shoot());
        }

        Debug.DrawRay(startPos, transform.forward * 20, drawColor);
    }


    IEnumerator Shoot() 
    {
        _allowShoot = false;

        animator.SetTrigger("Charge");
        yield return new WaitForSeconds(chargeTime);

        Instantiate(laserLine, weaponPos.position, weaponPos.rotation);

        yield return new WaitForSeconds(fireRate);
        _allowShoot = true;
    }
}
