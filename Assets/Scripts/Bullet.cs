using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;
   
    public ParticleSystem onHitEffect;

    [NonSerialized]
    public Quaternion playerRotation;
    [NonSerialized]
    public float damage;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    void Start()
    {
        _rigidbody.velocity = transform.up * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag) 
        {
            default: 
                break;
            
            case "Level":
            case "Door":
            case "Turret": 
                {
                    ContactPoint contact = collision.contacts[0];
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
                    Vector3 pos = contact.point;
                    var effect = Instantiate(onHitEffect, pos, rot);
                    Destroy(gameObject);
                    break;
                }

            case "Enemy":
            case "Boss":
                {
                    var p = collision.gameObject.GetComponent<BaseEnemy>();
                    p.OnHit(gameObject);
                    break;
                }
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") 
        {
           
            var p = other.gameObject.GetComponent<BaseEnemy>();
            p.OnHit(gameObject);
        }
      
    }*/

}
